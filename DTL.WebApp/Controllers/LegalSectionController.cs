using DTL.Business.Common;
using DTL.Business.LegalSection;
using DTL.Business.UserManagement;
using DTL.Model.CommonModels;
using DTL.Model.Models;
using DTL.WebApp.Common.CommonClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{

    [Authorize(Roles = DTL.WebApp.Common.Constants.DVB_PENSION_TRUST)]
    public class LegalSectionController : Controller
    {
        public const string DATE_FORMAT = "dd-MM-yyyy";
        public const string ASSIGN_DATE_FORMAT = "yyyy-MM-dd";
        public const string TIME_FORMAT = "hh:mm tt";
        public const string ASSIGN_TIME_FORMAT = "HH\\:mm";

        private static ILegalSectionForm legalSectionForm;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;
        IDVBUser _dVBUser;
        public LegalSectionController(ILegalSectionForm _legalSectionForm, IAssignPermission assignPermission,IDVBUser dVBUser)
        {
            legalSectionForm = _legalSectionForm;
            _assignPermission = assignPermission;
            _dVBUser = dVBUser;
        }
        public IActionResult Index(bool isArchive = false)
        {
            var data = legalSectionForm.GetAllCases();
            IEnumerable<LegalSectionModel> cases = new List<LegalSectionModel>();

            cases = data.Where(x => x.IsDeleted == isArchive);

            GetPermissions("Legal Section", "NA");
            if (_view == false) return Json("No View permission");

            ViewBag.Create = _create;
            ViewBag.View = _view;
            ViewBag.Edit = _edit;
            ViewBag.Delete = _delete;

            ViewBag.isArchive = isArchive;


            return View(cases.OrderByDescending(x => x.CreatedDate));
        }

        public IActionResult AddLegalCase()
        {
            GetPermissions("Legal Section", "NA");
            if (_create == false && _edit == false) return Json("No Add/Edit permission");

            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.CourtTypeList = populateList.CourtTypeList;
            ViewBag.LegalCaseStatusTypeList = populateList.LegalCaseStatusTypeList;

            ViewBag.Mode = "Insert";
            ViewBag.ASSIGN_DATE_FORMAT = ASSIGN_DATE_FORMAT;
            LegalSectionModel model = new LegalSectionModel();
            model.CaseInitialDate = DateTime.Now;
            int count = legalSectionForm.GetLegalCaseCount();
            string caseNo;
            do
            {
                count++;
                caseNo = $"DVB{DateTime.Now.Year}{count:D5}";
            }
            while (legalSectionForm.GetCaseDetailsByCaseId(caseNo).Count() > 0);

            model.CaseNo = caseNo;
            model.IsNew = true;

            return View("LegalCaseForm", model);
        }

        public JsonResult AddEditLegalCaseDetails(LegalSectionModel legalSectionModel)
        {
            GetPermissions("Legal Section", "NA");
            if (_create == false && _edit == false) return Json("No Add/Edit permission");
            try
            {
                string result;
                var res = "";

                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {

                            case "AttachmentFileInEnglish":
                                legalSectionModel.AttachmentFileInEnglish = ms1.ToArray();
                                legalSectionModel.EnglishFileName = file.FileName;
                                legalSectionModel.EnglishContentType = file.ContentType;
                                break;
                        }
                    }
                if (legalSectionModel.ID == Guid.Empty)
                {
                    legalSectionModel.CreatedBy = User.Identity.Name;
                    legalSectionModel.ModifideBy = "";
                    result = legalSectionForm.AddEditLegalCaseDetails(legalSectionModel, false);
                    if(result == "add")
                    {
                        string body = $"Dear user,\r\nYour legal case with case number {legalSectionModel.CaseNo}  has been initiated in DVBETBF. The next hearing date is {legalSectionModel.NextHearingDate?.ToString("dd-MM-yyyy")}.\r\nthanks, \r\nDVBETBF.";
                   
                        string mail = legalSectionModel.Email;
                        SendEmail.EmailAction(legalSectionModel.PetitionerName, mail, $"Your legal case with case number {legalSectionModel.CaseNo}  has been initiated in DVBETBF", body);

                        var userList = _dVBUser.GetDVBUserByParam(null, legalSectionModel.NameOfCouncil,  null, "Legal Case", null);
                        if (userList.Count() > 0)
                        {
                          
                            body = $"Dear user,\r\nYour legal case with case number {legalSectionModel.CaseNo}  has been initiated successfully. The next hearing date is {legalSectionModel.NextHearingDate?.ToString("dd-MM-yyyy")}.\r\nthanks, \r\nDVBETBF.";

                            mail = userList.First().EmailAddress;
                            SendEmail.EmailAction(legalSectionModel.PetitionerName, mail, $"Your legal case with case number {legalSectionModel.CaseNo}  has been initiated successfully", body);

                        }
                    }

                }
                else
                {
                    legalSectionModel.ModifideBy = User.Identity.Name;
                    result = legalSectionForm.AddEditLegalCaseDetails(legalSectionModel, true);

                }
                return Json(result);

            }
            catch (Exception ex)
            {
                return Json("Error");

            }
        }

        public IActionResult EditLegalCase(Guid Id)
        {
            GetPermissions("Legal Section", "NA");
            if (!_edit) return Json("No Edit permission");
            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.CourtTypeList = populateList.CourtTypeList;
            ViewBag.LegalCaseStatusTypeList = populateList.LegalCaseStatusTypeList;
            ViewBag.InvestmentId = Id;
            ViewBag.Mode = "Update";
            ViewBag.ASSIGN_DATE_FORMAT = ASSIGN_DATE_FORMAT;
            var data = legalSectionForm.GetCaseDetailsById(Id);
            data.IsReadOnly = false;
            return View("LegalCaseForm", data);
        }

        public IActionResult ViewLegalCase(Guid Id)
        {
            GetPermissions("Legal Section", "NA");
            if (!_view) return Json("No View permission");
            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.CourtTypeList = populateList.CourtTypeList;
            ViewBag.LegalCaseStatusTypeList = populateList.LegalCaseStatusTypeList;
            ViewBag.InvestmentId = Id;
            ViewBag.Mode = "Select";
            ViewBag.ASSIGN_DATE_FORMAT = ASSIGN_DATE_FORMAT;
            var data = legalSectionForm.GetCaseDetailsById(Id);
            data.IsReadOnly = true;
            return View("LegalCaseForm", data);
        }


        [HttpPost]
        public IActionResult ApproveLegalCase([FromBody] Guid Id)
        {
            var returnValue = 0;
            try
            {
                returnValue = legalSectionForm.ApproveLegalCases(Id, User.Identity.Name);

            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Json(returnValue > 0 ? "Success" : "Fail");
        }

        [HttpPost]
        public IActionResult DeleteLegalCase([FromBody] Guid Id)
        {
            GetPermissions("Legal Section", "NA");
            if (!_delete) return Json("No delete permission");
            var returnValue = 0;
            try
            {
                returnValue = legalSectionForm.DeleteLegalCases(Id, User.Identity.Name);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Json(returnValue > 0 ? "Success" : "Fail");
        }

        private void GetPermissions(string modulename, string submodulename)
        {
            _create = _edit = _view = _delete = true;

            //if (User.Identity.Name == null) return;
            //var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, modulename, submodulename).FirstOrDefault();
            //if (permission != null)
            //{
            //    _create = permission.Create;
            //    _edit = permission.Edit;
            //    _view = permission.View;
            //    _delete = permission.Delete;
            //}
        }
        public ActionResult DownloadFile(Guid Id, bool isView = true)
        {
            GetPermissions("Legal Section", "NA");
            if (!_view) return Json("No View permission");
            if (Guid.Empty == Id) return BadRequest();

            LegalSectionModel data = legalSectionForm.GetCaseDetailsById(Id);


            byte[] fileBytes = data.AttachmentFileInEnglish;
            if (isView)
                Response.Headers.Add("Content-Disposition", "inline; filename=" + data.EnglishFileName);
            return File(fileBytes, data.EnglishContentType);



            throw new Exception("Specify file type");
        }
    }
}
