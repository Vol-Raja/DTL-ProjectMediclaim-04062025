using DTL.Business.EmployeeRegistration;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTL.Model.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Security.Claims;
using DTL.WebApp.Common.Extensions;
using DTL.Business.Common;
using DTL.Model.CommonModels;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using DTL.WebApp.Common.CommonClasses;
using Microsoft.Extensions.Configuration;
using DTL.Business.UserManagement;
using DTL.Business.Logging;

namespace DTL.WebApp.Controllers
{
    [Authorize]
    public class EmployeeRegistrationController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static IAddEmployee _addEmployeeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;
        private readonly ILogging _logging;
        public EmployeeRegistrationController(IAddEmployee addEmployeeService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IAssignPermission assignPermission,
            ILogging logging)
        {
            _addEmployeeService = addEmployeeService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _assignPermission = assignPermission;
            _logging = logging;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var roles = (((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)).FirstOrDefault();
                ViewBag.Roles = roles;
                GetPermissions("Employee Registration", "NA");
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

        public async Task<IActionResult> ArchiveEmployee()
        {
            try
            {
                var roles = (((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)).FirstOrDefault();
                ViewBag.Roles = roles;
                var data = await _addEmployeeService.GetAllArchiveEmployeeAsync(Guid.Empty);
                return View("ArchiveEmployee", data);
            }
            catch (Exception ex)
            {
                log.Error("EmployeeRegistration ArchiveEmployee", ex);
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Add Employee";
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            ViewBag.Mode = "Add";
            return View("employeeProfile", new EmployeeProfileModel());
        }

        [HttpPost]
        public JsonResult AddEditEmployee(EmployeeProfileModel employee)
        {
            try
            {
                byte[] p1 = null;
                foreach (var file in Request.Form.Files)
                {

                    using var fs1 = file.OpenReadStream();
                    using var ms1 = new MemoryStream();
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();
                }
                employee.ProfileImage = p1;
                var result = "";
                employee.Mobile = employee.MobileCountryCode + "-" + employee.Mobile;
                employee.Phone = (string.IsNullOrEmpty(employee.PhoneCountryCode) ? employee.Phone : employee.PhoneCountryCode + "-" + employee.Phone);
                //employee.PPOStatusId = 1;
                if (employee.ID == Guid.Empty)
                {
                    employee.CreatedBy = User.Identity.Name;
                    result = _addEmployeeService.AddEditEmployeeData(employee, false);
                }
                else
                {
                    employee.ModifideBy = User.Identity.Name;
                    result = _addEmployeeService.AddEditEmployeeData(employee, true);
                }
                if (result.Contains("EMAIL_ID_UNIQUE"))
                    return Json("userExists");

                return Json(result);
            }
            catch (Exception ex)
            {
                log.Error("EmployeeRegistration AddEditEmployee", ex);
                return Json("Error");
            }
        }

        public JsonResult GetEmployeeDetails(string EmpID)
        {
            return Json(_addEmployeeService.GetEmployeeById(new Guid(EmpID)));
        }
        public IActionResult EditEmployee(Guid employeeId)
        {
            var data = _addEmployeeService.GetEmployeeById(employeeId);
            ViewData["Title"] = "Edit Employee Profile";
            var commonJsonData = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = commonJsonData.DTLOfficeList;
            var tempMobile = (string.IsNullOrEmpty(data.Mobile) ? null : data.Mobile.Split("-"));
            data.MobileCountryCode = (tempMobile != null ? tempMobile[0] : null);
            data.Mobile = (tempMobile != null ? tempMobile[1] : null);
            var tempPhone = (string.IsNullOrEmpty(data.Phone) ? null : data.Phone.Split("-"));
            data.PhoneCountryCode = (tempPhone != null ? tempPhone[0] : null);
            data.Phone = (tempPhone != null ? tempPhone[1] : null);
            ViewBag.Mode = "Edit";
            return View("employeeProfile", data);
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

        public IActionResult ViewArchiveEmployee(Guid employeeId)
        {
            var data = _addEmployeeService.GetArchiveEmployeeById(employeeId);

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

        public IActionResult EditEmployeeDetails(Guid employeeId)
        {
            ViewData["Title"] = "Edit Employee Details";
            ViewBag.EmpId = employeeId;
            ViewBag.Roles = (((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)).FirstOrDefault();
            var data = _addEmployeeService.GetEmployeeDetailsById(employeeId);

            data.form5.AadharDocPath = data.form5.AadharCard.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_aadhar_card");
            data.form5.PanDocPath = data.form5.PanCard.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_pan_card");
            data.serviceHistory.PaySlip1Path = data.serviceHistory.PaySlip1.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps1");
            data.serviceHistory.PaySlip2Path = data.serviceHistory.PaySlip2.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps2");
            data.serviceHistory.PaySlip3Path = data.serviceHistory.PaySlip3.ConvertBytesToPDF(data.employeeProfile.EmployeeName + "_ps3");

            return View("employeeDetails", data);
        }

        // POST: Form5Controller/Create
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

        [HttpPost]
        public ActionResult UpatePesionAppStatus(Guid empId, int status)
        {
            var result = _addEmployeeService.UpatePesionAppStatus(empId, status);
            return Json("add");
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

        public async Task<ActionResult> GenerateCredentialById(Guid employeeId)
        {
            string returnMessage = "";
            try
            {
                var smsservice = new MessageService(_configuration);
                var employee = _addEmployeeService.GetEmployeeById(employeeId);
                var user = new ApplicationUser()
                {
                    Email = employee.EmailAddress,
                    PhoneNumber = employee.Mobile,
                    UserName = employee.EmailAddress,
                    fullName = employee.EmployeeName,
                    EmailConfirmed = true
                };
                var tempPassword = "";
                string password = tempPassword.GeneratePassword();
                string textmessage = string.Format("Hi {0},  Your temporary password is {1} Thanks ", user.fullName, password);

                //Check if user exists
                var loginUser = await _userManager.FindByEmailAsync(user.Email);
                if (loginUser != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(loginUser);
                    var result = await _userManager.ResetPasswordAsync(loginUser, code, password);
                    if (result.Succeeded)
                    {
                        loginUser.IsTempPassword = false;
                        await _userManager.UpdateAsync(loginUser);
                        SendEmail.GenerateCreedential(user.fullName, user.Email, password);

                        await smsservice.SendCredential(user.PhoneNumber, textmessage);
                        returnMessage = "success";
                    }
                }
                else
                {
                    var userResult = await _userManager.CreateAsync(user, password);
                    var setPhoneNumber = await _userManager.SetPhoneNumberAsync(user, user.PhoneNumber);

                    if (userResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Pensioner");
                        SendEmail.GenerateCreedential(user.fullName, user.Email, password);
                        returnMessage = "success";
                    }
                    else
                    {
                        returnMessage = "userExists";
                    }
                }
                return Json(returnMessage);
            }
            catch (Exception ex)
            {
                log.Error("EmployeeRegistration GenerateCredentialById", ex);
                return Json("Error");
            }
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
