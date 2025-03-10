using DTL.Business.Common;
using DTL.Business.EmployeeRegistration;
using DTL.Business.GeneratePension;
using DTL.Business.UserManagement;
using DTL.Model.Models;
using DTL.Model.Models.UserManagement;
using DTL.WebApp.Common.Extensions;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{

    [Authorize]
    public class PensionerController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IAddEmployee _addEmployeeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IGeneratePension _generatePension;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;

        public PensionerController(IAddEmployee addEmployeeService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, 
            IGeneratePension generatePension,
            IAssignPermission assignPermission)
        {
            _addEmployeeService = addEmployeeService;
            _userManager = userManager;
            _roleManager = roleManager;
            _generatePension = generatePension;
            _assignPermission = assignPermission;
        }

        public IActionResult Index()
        {
            IEnumerable<AssignPermissionModel> assignPermissionModels = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, null, null);

            return View(assignPermissionModels);
            //return View();
        }
       
        public async Task<IActionResult> GeneratePPO()
        {
            try
            {
                var roles = (((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)).FirstOrDefault();
                ViewBag.Roles = roles;
                ViewBag.Mode = "Upsert";
                GetPermissions("Pension Payment Orders", "Generate PPO");
                ViewBag.Create = _create;
                ViewBag.View = _view;
                ViewBag.Edit = _edit;
                ViewBag.Delete = _delete;
                var data = await _addEmployeeService.GetAllEmployeeAsync(roles);
                return View(data);
            }
            catch (Exception ex)
            {
                log.Error("EmployeeRegistration Index", ex);
                return View("Error");
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreatePensionSlip(PensionSlipModel pensionSlip)
        {
            var result = "";
            if (pensionSlip.ID == Guid.Empty)
            {
                pensionSlip.CreatedBy = User.Identity.Name;
                result = _addEmployeeService.AddPensionSlip(pensionSlip, false);
                return Json("add");
            }
            else
            {
                pensionSlip.ModifideBy = User.Identity.Name;
                result = _addEmployeeService.AddPensionSlip(pensionSlip, true);
                return Json("update");
            }
        }

        public async Task<IActionResult> ViewPPO()
        {
            try
            {
                var roles = (((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)).FirstOrDefault();
                ViewBag.Roles = roles;
                ViewBag.Mode = "View";

                GetPermissions("Pension Payment Orders", "View PPO");
                ViewBag.Create = _create;
                ViewBag.View = _view;
                ViewBag.Edit = _edit;
                ViewBag.Delete = _delete;

                var data = await _addEmployeeService.GetAllEmployeeAsync(roles);
                return View(data);
            }
            catch (Exception ex)
            {
                log.Error("EmployeeRegistration Index", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> AMScrutiny()
        {
            try
            {
                var roles = (((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)).FirstOrDefault();
                ViewBag.Roles = roles;
                ViewBag.Mode = "Upsert";

                GetPermissions("Pension Payment Orders", "AM Scrutiny");
                ViewBag.Create = _create;
                ViewBag.View = _view;
                ViewBag.Edit = _edit;
                ViewBag.Delete = _delete;

                var data = await _addEmployeeService.GetAllEmployeeAsync(roles);
                data = data.Where(x => x.PPOStatusId <= 4);
                return View(data);
            }
            catch (Exception ex)
            {
                log.Error("EmployeeRegistration Index", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> HODApproval()
        {
            try
            {
                var roles = (((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)).FirstOrDefault();
                ViewBag.Roles = roles;
                ViewBag.Mode = "Upsert";
                
                GetPermissions("Pension Payment Orders", "HOD Approval");
                ViewBag.Create = _create;
                ViewBag.View = _view;
                ViewBag.Edit = _edit;
                ViewBag.Delete = _delete;

                var data = await _addEmployeeService.GetAllEmployeeAsync(roles);
                data = data.Where(x => x.PPOStatusId > 5 && x.PPOStatusId != 6);
                return View(data);
            }
            catch (Exception ex)
            {
                log.Error("EmployeeRegistration Index", ex);
                return View("Error");
            }
        }

        public IActionResult ViewEmployee(Guid employeeId)
        {
            var data = _addEmployeeService.GetEmployeeById(employeeId);
            ViewData["Title"] = "View Employee Profile";
            var commonJsonData = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = commonJsonData.DTLOfficeList;
            var tempMobile = (string.IsNullOrEmpty(data.Mobile) ? null : data.Mobile.Split("-"));
            data.MobileCountryCode = (tempMobile != null ? tempMobile[0] : null);
            data.Mobile = (tempMobile != null ? tempMobile[1] : null);
            var tempPhone = (string.IsNullOrEmpty(data.Phone) ? null : data.Phone.Split("-"));
            data.PhoneCountryCode = (tempPhone != null ? tempPhone[0] : null);
            data.Phone = (tempPhone != null ? tempPhone[1] : null);
            ViewBag.Mode = "View";
            return View("employeeProfile", data);
        }

        public IActionResult ViewEmployeeDetails(Guid employeeId)
        {
            ViewData["Title"] = "View Employee Details";
            ViewBag.EmpId = employeeId;
            ViewBag.Mode = "View";
            ViewBag.Roles = (((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)).FirstOrDefault();
            var data = _addEmployeeService.GetEmployeeDetailsById(employeeId);

            data.form5.AadharDocPath = data.form5.AadharCard.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_aadhar_card");
            data.form5.PanDocPath = data.form5.PanCard.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_pan_card");
            data.serviceHistory.PaySlip1Path = data.serviceHistory.PaySlip1.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps1");
            data.serviceHistory.PaySlip2Path = data.serviceHistory.PaySlip2.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps2");
            data.serviceHistory.PaySlip3Path = data.serviceHistory.PaySlip3.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps3");

            ViewBag.DOB = data.employeeProfile.DOB;
            ViewBag.ServiceStartDate = data.employeeProfile.ServiceStartDate;
            ViewBag.ServiceEndDate = data.employeeProfile.ServiceEndDate;
            ViewBag.ReasonOfRetirement = data.employeeProfile.ReasonOfRetirement;

            return View("employeeDetails", data);
        }

        public IActionResult PrintPPO(Guid employeeId)
        {
            ViewData["Title"] = "View Employee Details";
            ViewBag.EmpId = employeeId;
            ViewBag.Mode = "View";
            ViewBag.Roles = (((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)).FirstOrDefault();
            var data = _addEmployeeService.GetEmployeeDetailsById(employeeId);

            data.form5.AadharDocPath = data.form5.AadharCard.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_aadhar_card");
            data.form5.PanDocPath = data.form5.PanCard.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_pan_card");
            data.serviceHistory.PaySlip1Path = data.serviceHistory.PaySlip1.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps1");
            data.serviceHistory.PaySlip2Path = data.serviceHistory.PaySlip2.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps2");
            data.serviceHistory.PaySlip3Path = data.serviceHistory.PaySlip3.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps3");

            ViewBag.DOB = data.employeeProfile.DOB;
            ViewBag.ServiceStartDate = data.employeeProfile.ServiceStartDate;
            ViewBag.ServiceEndDate = data.employeeProfile.ServiceEndDate;
            ViewBag.ReasonOfRetirement = data.employeeProfile.ReasonOfRetirement;

            return View("PrintPPO", data);
            //return View();
        }

        public IActionResult EditEmployeeDetails(Guid employeeId)
        {
            try
            {
                ViewData["Title"] = "Edit Employee Details";
                ViewBag.EmpId = employeeId;
                ViewBag.Roles = (((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)).FirstOrDefault();
                var data = _addEmployeeService.GetEmployeeDetailsById(employeeId);

                ViewBag.EmployeeNumber = data.employeeProfile.EmployeeId;
                ViewBag.EmployeeName = data.employeeProfile.EmployeeName;

                data.form5.AadharDocPath = data.form5.AadharCard.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_aadhar_card");
                data.form5.PanDocPath = data.form5.PanCard.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_pan_card");
                data.serviceHistory.PaySlip1Path = data.serviceHistory.PaySlip1.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps1");
                data.serviceHistory.PaySlip2Path = data.serviceHistory.PaySlip2.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps2");
                data.serviceHistory.PaySlip3Path = data.serviceHistory.PaySlip3.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps3");

                ViewBag.DOB = data.employeeProfile.DOB;
                ViewBag.ServiceStartDate = data.employeeProfile.ServiceStartDate;
                ViewBag.ServiceEndDate = data.employeeProfile.ServiceEndDate;
                ViewBag.ReasonOfRetirement = data.employeeProfile.ReasonOfRetirement;

                return View("employeeDetails", data);
            }
            catch (Exception ex)
            {
                log.Error("Edit Employee Details", ex);
                return View("Error");
            }
        }

        [HttpPost]
        public JsonResult CreateForm5(Form5Model form5Model)
        {
            foreach (var file in Request.Form.Files)
            {
                using var fs1 = file.OpenReadStream();
                using var ms1 = new MemoryStream();
                fs1.CopyTo(ms1);
                switch (file.Name)
                {
                    case "AadharCard":
                        form5Model.AadharCard = ms1.ToArray();
                        break;
                    case "PanCard":
                        form5Model.PanCard = ms1.ToArray();
                        break;
                    case "SpecimenSignature1":
                        form5Model.SpecimenSignature1 = ms1.ToArray();
                        break;
                    case "SpecimenSignature2":
                        form5Model.SpecimenSignature2 = ms1.ToArray();
                        break;
                    case "SpecimenSignature3":
                        form5Model.SpecimenSignature3 = ms1.ToArray();
                        break;
                    case "ThumbImpression1":
                        form5Model.ThumbImpression1 = ms1.ToArray();
                        break;
                    case "ThumbImpression2":
                        form5Model.ThumbImpression2 = ms1.ToArray();
                        break;
                }
            }

            var result = "";
            if (form5Model.ID == Guid.Empty)
            {
                // employee.CreatedBy = User.Identity.Name;
                int a = _addEmployeeService.AddForm5Data(form5Model, false);
                return Json("add");
            }
            else
            {
                //employee.ModifideBy = User.Identity.Name;
                int a = _addEmployeeService.AddForm5Data(form5Model, true);
                return Json("update");
            }
        }

        // POST: FamilyDetailsController/Create
        [HttpPost]
        public JsonResult AddNominee(List<NomineeDetailsModel> nomineeDetailsModel)
        {
            if (nomineeDetailsModel.Any())
            {
                _addEmployeeService.RemoveNominee(nomineeDetailsModel.Select(x => x.EmployeeRegistrationId).FirstOrDefault());
                foreach (var data in nomineeDetailsModel)
                {
                    data.CreatedBy = User.Identity.Name;
                    int a = _addEmployeeService.AddNominee(data);
                }
                return Json("success");
            }
            else
            {
                return Json("Error");
            }
        }

        [HttpPost]
        public JsonResult CreateServiceHistory(ServiceHistoryModel serviceHistoryModel)
        {
            var result = string.Empty;
            var i = 0;
            foreach (var file in Request.Form.Files)
            {
                using var fs1 = file.OpenReadStream();
                using var ms1 = new MemoryStream();
                fs1.CopyTo(ms1);
                switch (i)
                {
                    case 0:
                        serviceHistoryModel.PaySlip1 = ms1.ToArray();
                        break;
                    case 1:
                        serviceHistoryModel.PaySlip2 = ms1.ToArray();
                        break;
                    case 2:
                        serviceHistoryModel.PaySlip3 = ms1.ToArray();
                        break;
                }
                i++;
            }
            if (serviceHistoryModel.ID == Guid.Empty)
            {
                serviceHistoryModel.CreatedBy = User.Identity.Name;
                result = _addEmployeeService.AddServiceHistory(serviceHistoryModel, false);
                return Json("add");
            }
            else
            {
                serviceHistoryModel.ModifideBy = User.Identity.Name;
                result = _addEmployeeService.AddServiceHistory(serviceHistoryModel, true);
                return Json("update");
            }
        }



        [HttpPost]
        public void UpatePesionAppStatus(Guid empId, int status, string role)
        {
            var result = _addEmployeeService.UpatePesionAppStatus(empId, status);
            var res = _addEmployeeService.UpatePesionOrderID(empId);


        }

        [HttpPost]
        public ActionResult DeleteFile(string path)
        {
            var fileName = System.IO.Path.GetFileName(path);
            fileName.DeleteTempFile();
            return Json("");
        }

        public JsonResult GetEmployeeByDTLOffice(string DtlOffice, String SearchType, String SearchVal)
        {
            IEnumerable<EmployeeProfileModel> EmpData;// = new IEnumerable<EmployeeProfileModel>();
            EmpData = _addEmployeeService.GetEmployeeByDTLOffice(DtlOffice, SearchType, SearchVal);

            var iEnm = EmpData as IEnumerable<EmployeeProfileModel>;
            List<EmployeeProfileModel> lst = iEnm.ToList();

            if (SearchType == "EmployeeId")
            {
                var res = (from N in lst
                           where N.EmployeeId.ToString().StartsWith(SearchVal)
                           select new { N.EmployeeId });
                return Json(res);
            }
            if (SearchType == "EmployeeName")
            {
                var res = (from N in lst
                           where N.EmployeeName.ToString().StartsWith(SearchVal)
                           select new { N.EmployeeName });
                return Json(res);
            }

            return Json("");



        }

        [HttpPost]
        public ActionResult UpdateEmployeeRegStatusByHRAdmin(Guid employeeId, string Role, int status, string Remarks)
        {
            var result = _addEmployeeService.UpdateRejectionStatusRemarks(employeeId, Role, status, Remarks);
            return Json("add");
        }

        public JsonResult DeleteEmp(Guid employeeId, string deleteReason)
        {
            try
            {
                var result = _addEmployeeService.DeleteEmp(employeeId, deleteReason);
                return Json("Deleted");
            }
            catch (Exception ex)
            {
                log.Error("DeleteEmp", ex);
                return Json("Error");
            }
        }

        public JsonResult GetFile(string fileName, Guid employeeId)
        {
            try
            {
                var data = _addEmployeeService.GetEmployeeDetailsById(employeeId);


                switch (fileName)
                {
                    case "aadhar":
                        data.form5.AadharDocPath = data.form5.AadharCard.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_aadhar_card");
                        return Json(data.form5.AadharDocPath);
                    case "pan":
                        data.form5.PanDocPath = data.form5.PanCard.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_pan_card");
                        return Json(data.form5.PanDocPath);

                    case "payslip":
                        data.serviceHistory.PaySlip1Path = data.serviceHistory.PaySlip1.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps1");
                        data.serviceHistory.PaySlip2Path = data.serviceHistory.PaySlip2.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps2");
                        data.serviceHistory.PaySlip3Path = data.serviceHistory.PaySlip3.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps3");
                        return Json(new
                        {
                            PaySlip1 = data.serviceHistory.PaySlip1Path,
                            PaySlip2 = data.serviceHistory.PaySlip2Path,
                            PaySlip3 = data.serviceHistory.PaySlip3Path
                        });
                    default:
                        return Json("NoRecord");
                }
            }
            catch (Exception ex)
            {
                log.Error("GetFile", ex);
                return Json("Error");
            }
        }

        public IActionResult GeneratePension()
        {
            //var _model = new GeneratePensionViewModel();
            //_model.PensionModel = new GeneratePensionModel();
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            return View();
        }

        [HttpGet]
        [Route("Pensioner/GetGeneratePensionByParam/{month?}/{year?}/{bank?}/{employer?}/{ppono?}")]
        public IActionResult GetGeneratePensionByParam([FromRoute] int? month, [FromRoute] int? year, [FromRoute] int? ppono, [FromRoute] string bank = null, [FromRoute] string employer = null)
        {
            //var _model = new GeneratePensionViewModel();
            employer = employer != "NA" ? employer : null;
            month = month > 0 ? month : null;
            year = year > 0 ? year : null;
            ppono = ppono > 0 ? ppono : null;
            bank = bank != "NA" ? bank : null;

            var _generatePensionList = _generatePension.GetGeneratePension(month, year, bank, employer, ppono);
            return Json(_generatePensionList);
        }

        public IActionResult PensionSlipCertificate()
        {
            return View();
        }

        [HttpGet]
        [Route("Pensioner/GetPensionSlipCertificateByParam/{month?}/{year?}/{ppono?}")]
        public IActionResult GetPensionSlipCertificateByParam([FromRoute] int? month, [FromRoute] int? year, [FromRoute] int? ppono)
        {  
            month = month > 0 ? month : null;
            year = year > 0 ? year : null;
            ppono = ppono > 0 ? ppono : null;
            
            var _generatePensionList = _generatePension.GetGeneratePension(month, year, null, null, ppono);
            return Json(_generatePensionList);
        }


        private void GetPermissions(string modulename, string submodulename)
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, modulename, submodulename).FirstOrDefault();
            if (permission != null)
            {
                _create = permission.Create;
                _edit = permission.Edit;
                _view = permission.View;
                _delete = permission.Delete;
            }
        }
    }
}
