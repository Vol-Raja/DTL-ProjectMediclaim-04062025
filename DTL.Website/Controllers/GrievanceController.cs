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
using DTL.WebApp.Common.CommonClasses;

namespace DTL.Website.Controllers
{
    public class GrievanceController : Controller
    {
        const string WITHDRAWN_BY_COMPLAINT = "Withdrawn by Complaint";
        const string RESOLVED = "Resolved";
        const string PENDING = "Pending";
        const string UNDER_PROCESS = "Under Process";
        private bool _create, _edit, _view, _delete = false;
        private static IAddGrievance _GrievanceService;
        private readonly IAssignPermission _assignPermission;
        public GrievanceController(IAddGrievance addGrieveance, IAssignPermission assignPermission)
        {
            _GrievanceService = addGrieveance;
            _assignPermission = assignPermission;
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


        public IActionResult CreateGrievance()
        {


            ViewData["Title"] = "Add Grievance";

            ViewBag.Mode = "Add";

            var model = new GrievanceModel();
            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "CreateGrievance", model);
            }

            return View(model);
        }
        public JsonResult AddEditGrievance(GrievanceModel grievanceModel)
        {

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
                    SendEmail.EmailAction(grievanceModel.Name, grievanceModel.EmailID, "Grievance submitted successfully", "Hi " + grievanceModel.Name + ",\nyour grievance submitted successfully.\r\nGrievance id: "+ grievanceModel.GrievanceID + "\r\nThanks and regards \r\nDVBETBF \r\n.");

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
                return Json("Error");

            }
        }

        public IActionResult ViewGrievance()
        {
            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "StatusGrievance");
            }
            return View("StatusGrievance");
        }
        [HttpGet]
        public IActionResult GetStatusGrievance([FromQuery] string ID)
        {
            int GrievanceID = 0;

            int.TryParse(ID, out GrievanceID);

            var data = _GrievanceService.GetGrievanceDetailsByGrievanceId(GrievanceID);

            return Json(data);
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



        private void GetPermissions(string modulename, string submodulename)
        {
            
            if (User.Identity.Name == null) return;
            _create = _edit = _view = _delete = true;
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
