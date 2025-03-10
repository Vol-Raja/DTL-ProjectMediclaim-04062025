using DTL.Business.Common;
using DTL.Business.LegalSection;
using DTL.Business.UserManagement;
using DTL.Model.CommonModels;
using DTL.Model.Models;
using DTL.Model.Models.UserManagement;
using DTL.WebApp.Common.CommonClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTL.WebApp.Controllers
{

    [Authorize]
    public class LegalAdvisorController : Controller
    {
        public const string DATE_FORMAT = "dd-MM-yyyy";
        public const string ASSIGN_DATE_FORMAT = "yyyy-MM-dd";
        public const string TIME_FORMAT = "hh:mm tt";
        public const string ASSIGN_TIME_FORMAT = "HH\\:mm";
        private const string APPROVED = "Approved";
        private const string REJECTED = "Rejected";
        private static ILegalSectionForm legalSectionForm;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;
        public LegalAdvisorController(ILegalSectionForm _legalSectionForm, IAssignPermission assignPermission)
        {
            legalSectionForm = _legalSectionForm;
            _assignPermission = assignPermission;
        }
        public IActionResult Index()
        {

            AssignPermissionModel assignPermissionModel = GetPermissions("Legal Advisor", "NA");
            if (!_view) return Json("No view permission");
            ViewBag.Create = _create;
            ViewBag.View = _view;
            ViewBag.Edit = _edit;
            ViewBag.Delete = _delete;

            return View(assignPermissionModel);
        }


        public IActionResult AddEditLegalCaseDetails(LegalSectionModel legalSectionModel)
        {
            GetPermissions("Legal Advisor", "NA");
            if (!_edit) return Json("No edit permission");
            try
            {
                string result;
                var res = "";


                if (legalSectionModel.ID == Guid.Empty)
                {
                    throw new ArgumentNullException("ID param is null");
                }
                else
                {
                    var updatedLegalSectionModel = legalSectionForm.GetCaseDetailsById(legalSectionModel.ID);

                    updatedLegalSectionModel.ModifideBy = User.Identity.Name;
                    updatedLegalSectionModel.LegalAdvisorStatus = (bool)legalSectionModel.Approve ? APPROVED : REJECTED;
                    updatedLegalSectionModel.LegalAdvisorRemarks = legalSectionModel.LegalAdvisorRemarks;
                    result = legalSectionForm.AddEditLegalCaseDetails(updatedLegalSectionModel, true);
                    if (Convert.ToBoolean(updatedLegalSectionModel.Approve))
                        SendEmail.EmailAction(updatedLegalSectionModel.PetitionerName, updatedLegalSectionModel.Email, "Legal case with case number " + updatedLegalSectionModel.CaseNo + " has been approved ", "Hi " + updatedLegalSectionModel.PetitionerName + ",\nYour legal case with case number " + updatedLegalSectionModel.CaseNo + " has been approved by the legal advisor.\r\nRemarks: " + updatedLegalSectionModel.LegalAdvisorRemarks + ". \r\nThanks and regards \r\nDVBETBF \r\n.");
                    else
                        SendEmail.EmailAction(updatedLegalSectionModel.PetitionerName, updatedLegalSectionModel.Email, "Legal case with case number " + updatedLegalSectionModel.CaseNo + " has been rejected ", "Hi " + updatedLegalSectionModel.PetitionerName + ",\nYour legal case with case number " + updatedLegalSectionModel.CaseNo + " has been rejected by the legal advisor.\r\nRemarks: " + updatedLegalSectionModel.LegalAdvisorRemarks + ". \r\nThanks and regards \r\nDVBETBF \r\n.");

                }
                return Json("update"); //return RedirectToAction(nameof(ViewLegalCases));

            }
            catch (Exception ex)
            {
                return Json("Error");

            }
        }



        public IActionResult ViewLegalCase(Guid Id, bool isReadOnly = false)
        {
            GetPermissions("Legal Advisor", "NA");

            if (_view == false) return Json("No View permission");

            CommonModel populateList = ReadJson.LoadJson();

            var data = legalSectionForm.GetCaseDetailsById(Id);
            if (data == null) return Ok(data);
            data.Approve = data.LegalAdvisorStatus == null ? null : data.LegalAdvisorStatus == APPROVED;
            if (isReadOnly)
                data.IsReadOnly = isReadOnly;
            return View(data);
        }

        public IActionResult ViewLegalCases(bool isReviewedOnly = false)
        {
            GetPermissions("Legal Advisor", "NA");
            if (_view == false) return Json("No View permission");
            ViewData["Title"] = isReviewedOnly ? "Approval/Reject Cases" : "Legal Case Requests";
            ViewBag.Create = _create;
            ViewBag.View = _view;
            ViewBag.Edit = _edit;
            ViewBag.Delete = _delete;
            ViewBag.isReviewedOnly = isReviewedOnly;
            var data = legalSectionForm.GetAllCases();
            List<LegalSectionModel> cases = new List<LegalSectionModel>();

            cases = data.Where(x => x.IsDeleted == false).ToList();
            if (isReviewedOnly)
            {
                cases = cases.Where(x => x.LegalAdvisorStatus != null).ToList();
                cases.ForEach(x => x.IsReadOnly = true);
            }
            else
            {
                cases = cases.Where(x => x.LegalAdvisorStatus == null).ToList();
            }
            return View(cases.OrderByDescending(x => x.CreatedDate));
        }

        private AssignPermissionModel GetPermissions(string modulename, string submodulename)
        {
            _create = _edit = _view = _delete = true;

            if (User.Identity.Name == null) return null;

            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, modulename, submodulename).FirstOrDefault();
            if (permission != null)
            {
                _create = permission.Create;
                _edit = permission.Edit;
                _view = permission.View;
                _delete = permission.Delete;
            }
            return permission;
        }
        public ActionResult DownloadFile(Guid Id, bool isView = true)
        {
            GetPermissions("Legal Section", "NA");
            if (!_view) throw new UnauthorizedAccessException("No View permission");
            if (Guid.Empty == Id) return BadRequest();

            var data = legalSectionForm.GetCaseDetailsById(Id);


            byte[] fileBytes = data.AttachmentFileInEnglish;
            if (isView)
                Response.Headers.Add("Content-Disposition", "inline; filename=" + data.EnglishFileName);
            return File(fileBytes, data.EnglishContentType);



            throw new Exception("Specify file type");
        }
    }
}
