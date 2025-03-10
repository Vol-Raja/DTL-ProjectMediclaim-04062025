using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTL.Business.Common;
using Microsoft.AspNetCore.Mvc;
using DTL.Model.Models;
using DTL.Business.Grievance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using DTL.Business.UserManagement;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using DTL.WebApp.Common.CommonClasses;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using DTL.WebApp.Models;
using DTL.WebApp.Data.Migrations;

namespace DTL.WebApp.Controllers
{
    [Authorize(Roles = DTL.WebApp.Common.Constants.DVB_PENSION_TRUST)]
    public class GrievanceController : Controller
    {
        const string WITHDRAWN_BY_COMPLAINT = "Withdrawn by Complaint";
        const string RESOLVED = "Resolved";
        const string PENDING = "Pending";
        const string UNDER_PROCESS = "Under Process";
        private bool _create, _edit, _view, _delete = false;
        private static IAddGrievance _GrievanceService;
        private readonly IAssignPermission _assignPermission;
        private readonly UserManager<ApplicationUser> _userManager;
        public GrievanceController(IAddGrievance addGrieveance, IAssignPermission assignPermission, UserManager<ApplicationUser> userManager)

        {
            _GrievanceService = addGrieveance;
            _assignPermission = assignPermission;
            _userManager = userManager;
        }



        public async Task<IActionResult> Index()
        {
            var roles = (((ClaimsIdentity)User.Identity).Claims
                   .Where(c => c.Type == ClaimTypes.Role)
                   .Select(c => c.Value)).FirstOrDefault();
            ViewBag.Roles = roles;

            //var data = await _GrievanceService.GetGrievanceDetails();
            return View();
        }
        public async Task<IActionResult> GrievanceList(bool isArchive = false)
        {
            var roles = (((ClaimsIdentity)User.Identity).Claims
                   .Where(c => c.Type == ClaimTypes.Role)
                   .Select(c => c.Value)).FirstOrDefault();
            ViewBag.Roles = roles;


            GetPermissions("Grievance", "NA");
            ViewBag.Create = _create;
            ViewBag.View = _view;
            ViewBag.Edit = _edit;
            ViewBag.Delete = _delete;

            ViewBag.isArchive = isArchive;
            if (_view == false) return Json("No view permission");

            var data = await _GrievanceService.GetGrievanceDetails();
            IEnumerable<GrievanceModel> grievanceModels;
            if (isArchive)
                grievanceModels = data.Where(x => x.IsDeleted == isArchive);
            else
                grievanceModels = data.Where(x => x.IsDeleted == false && (x.Status == PENDING || x.Status == UNDER_PROCESS));

            return View(grievanceModels.OrderByDescending(x => x.CreatedDate));
        }
        public async Task<IActionResult> RequestGrievanceList(bool isArchive = false, bool isResolved = false)
        {
            var roles = (((ClaimsIdentity)User.Identity).Claims
                   .Where(c => c.Type == ClaimTypes.Role)
                   .Select(c => c.Value)).FirstOrDefault();
            ViewBag.Roles = roles;


            GetPermissions("Grievance", "NA");
            ViewBag.Create = _create;
            ViewBag.View = _view;
            ViewBag.Edit = _edit;
            ViewBag.Delete = _delete;
            ViewBag.isResolved = isResolved;
            ViewBag.isArchive = isArchive;
            if (_view == false) return Json("No view permission");
            var data = await _GrievanceService.GetGrievanceDetails();
            IEnumerable<GrievanceModel> grievanceModels = data.Where(x => x.IsDeleted == isArchive);
            if (isResolved)
            {
                grievanceModels = grievanceModels.Where(x => x.Status == RESOLVED || x.Status == WITHDRAWN_BY_COMPLAINT);
            }
            else
                grievanceModels = grievanceModels.Where(x => x.Status == PENDING || x.Status == UNDER_PROCESS);


            return View(grievanceModels.OrderByDescending(x => x.CreatedDate));
        }

        public IActionResult CreateGrievance()
        {

            GetPermissions("Grievance", "NA");

            if (_create == false) return Json("No create permission");

            ViewData["Title"] = "Add Grievance";
            var data = ReadJson.LoadJson();
            ViewBag.GrievanceDepartment = data.GrievanceDepartmentList;
            ViewBag.Mode = "Add";
            return View(new GrievanceModel());
        }
        public async Task<JsonResult> AddEditGrievance(GrievanceModel grievanceModel)
        {
            GetPermissions("Grievance", "NA");

            if (_create == false && _edit == false) return Json("No Add/Edit permission");

            try
            {
                string result;
                if (grievanceModel.ID == Guid.Empty)
                    grievanceModel.Status = "Pending";
                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {

                            case "AttachmentFileInEnglish":
                                grievanceModel.AttachmentFileInEnglish = ms1.ToArray();
                                grievanceModel.EnglishFileName = file.FileName;
                                grievanceModel.EnglishContentType = file.ContentType;
                                break;
                        }
                    }
                if (grievanceModel.ID == Guid.Empty)
                {
                    grievanceModel.CreatedBy = User.Identity.Name;
                    grievanceModel.ModifideBy = "";
                    result = _GrievanceService.AddEditGrievance(grievanceModel, false);

                    var body = $"Hello {User.Identity.Name}," +
                           $"{Environment.NewLine} New Grievance request has been submitted by {grievanceModel.UserType}." +
                           $"{Environment.NewLine} Grievance ID: {result}" +
                             $"{Environment.NewLine} Complaintant Name: {grievanceModel.Name}" +
                             $"{Environment.NewLine} Subject: {grievanceModel.Subject}";
                    ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    string mail = user.Email;
                    SendEmail.EmailAction(User.Identity.Name, mail, $"New Grievance request has been submitted by {grievanceModel.UserType}", body);
                    SendEmail.EmailAction(grievanceModel.Name, grievanceModel.EmailID, "Grievance submitted successfully", "Hi " + grievanceModel.Name + ",\nyour grievance submitted successfully.\r\nGrievance id: " + grievanceModel.GrievanceID + "\r\nThanks and regards \r\nDVBETBF \r\n.");


                }
                else
                {
                    grievanceModel.ModifideBy = User.Identity.Name;
                    result = _GrievanceService.AddEditGrievance(grievanceModel, true);

                }
                return Json(result);

            }
            catch (Exception ex)
            {
                return Json("Error-" + ex.Message);

            }
        }

        public IActionResult ViewGrievance(Guid Id)
        {
            GetPermissions("Grievance", "NA");

            if (_view == false) return Json("No View permission");
            var data = _GrievanceService.GetGrievanceDetailsById(Id);
            ViewData["Title"] = "View Grievance details";

            var commonJsonData = ReadJson.LoadJson();
            ViewBag.GrievanceDepartment = commonJsonData.GrievanceDepartmentList;
            ViewBag.Mode = "View";
            return View("ViewGrievance", data);
        }

        public IActionResult RequestViewGrievance(Guid Id, bool isResolved = false)
        {
            GetPermissions("Grievance", "NA");

            if (_view == false) return Json("No View permission");

            var data = _GrievanceService.GetGrievanceDetailsById(Id);
            ViewData["Title"] = "View Grievance details";
            var commonJsonData = ReadJson.LoadJson();
            ViewBag.GrievanceDepartment = commonJsonData.GrievanceDepartmentList;
            ViewBag.Mode = "View";
            ViewBag.isResolved = isResolved;
            return View("RequestViewGrievance", data);
        }

        public IActionResult EditGrievance(Guid Id)
        {
            GetPermissions("Grievance", "NA");

            if (_edit == false) return Json("No Edit permission");
            var data = _GrievanceService.GetGrievanceDetailsById(Id);
            ViewData["Title"] = "Edit Grievance details";
            var commonJsonData = ReadJson.LoadJson();
            ViewBag.GrievanceDepartment = commonJsonData.GrievanceDepartmentList;
            ViewBag.Mode = "Edit";
            return View("CreateGrievance", data);
        }

        public JsonResult DeleteGrievance(Guid Id)
        {
            GetPermissions("Grievance", "NA");

            if (_delete == false) return Json("No Delete permission");
            var data = _GrievanceService.GetGrievanceDetailsById(Id);
            data.IsDeleted = true;
            var result = _GrievanceService.AddEditGrievance(data, true);
            // var result = _GrievanceService.DeleteGrievance(Id);
            return Json("Deleted");
        }

        [HttpPost]
        public IActionResult GrievanceReplyMessage([FromBody] GrievanceModel grievanceModel)
        {
            var returnValue = 0;
            try
            {
                if (grievanceModel.ID.ToString() != null && !string.IsNullOrEmpty(grievanceModel.Reply))
                {
                    grievanceModel.ModifideBy = User.Identity.Name;
                    returnValue = _GrievanceService.GrievanceReplyMessage(grievanceModel);
                }
                else
                {
                    IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(allErrors);
                }
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
        public ActionResult DownloadFile(Guid Id, bool isView = true, bool isHead = false)
        {
            GetPermissions("Grievance", "NA");

            if (_view == false) return Json("No View permission");

            if (Guid.Empty == Id) return BadRequest();

            GrievanceModel data = _GrievanceService.GetGrievanceDetailsById(Id);


            byte[] fileBytes = null;
            string ContentType = null;
            string file = null;
            if (isHead)
            {
                ContentType = data.GrievanceHeadEnglishContentType;
                file = data.GrievanceHeadEnglishFileName;
                fileBytes = data.GrievanceHeadAttachmentFileInEnglish;
            }

            else
            {
                ContentType = data.EnglishContentType;
                file = data.EnglishFileName;
                fileBytes = data.AttachmentFileInEnglish;


            }


            if (fileBytes == null || fileBytes.Length == 0) throw new Exception("File not found");
            if (isView)
                Response.Headers.Add("Content-Disposition", "inline; filename=" + file);
            return File(fileBytes, ContentType);



            throw new Exception("File not found");
        }

        public JsonResult SetStatus(GrievanceModel grievanceModel)
        {
            GetPermissions("Grievance", "NA");

            if (_edit == false) return Json("No Edit permission");
            try
            {
                string result = "";


                if (grievanceModel.ID != Guid.Empty)
                {



                    var data = _GrievanceService.GetGrievanceDetailsById(grievanceModel.ID);
                    data.ModifideBy = User.Identity.Name;
                    data.Status = grievanceModel.Status;
                    data.Remarks = grievanceModel.Remarks;


                    if (Request.Form.Files.Count > 0)
                        foreach (var file in Request.Form.Files)
                        {
                            using var fs1 = file.OpenReadStream();
                            using var ms1 = new MemoryStream();
                            fs1.CopyTo(ms1);
                            switch (file.Name)
                            {

                                case "GrievanceHeadAttachmentFileInEnglish":
                                    data.GrievanceHeadAttachmentFileInEnglish = ms1.ToArray();
                                    data.GrievanceHeadEnglishFileName = file.FileName;
                                    data.GrievanceHeadEnglishContentType = file.ContentType;
                                    break;
                            }
                        }

                    result = _GrievanceService.AddEditGrievance(data, true);
                    if (data.Status == "Resolved")
                    {
                        SendEmail.EmailAction(grievanceModel.Name, grievanceModel.EmailID, "Grievance resolved successfully", "Hi " + grievanceModel.Name + ",\nYour grievance resolved by the DVBETBF grievance head.\r\nGrievance id: " + grievanceModel.GrievanceID + ".\r\nPlease check the remarks on the website. \r\nThanks and regards \r\nDVBETBF \r\n.");
                    }

                }
                return Json(result);

            }
            catch (Exception ex)
            {
                return Json("Error");

            }
        }
    }
}
