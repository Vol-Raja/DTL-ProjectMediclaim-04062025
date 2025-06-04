using DTL.Business.Logging;
using DTL.Business.UserManagement;
using DTL.Model.Models.UserManagement;
using DTL.WebApp.Common;
using DTL.WebApp.Common.CommonClasses;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAssignPermission _assignPermission;
        private readonly ILogging _logging;
        private readonly IDVBUser _dvbUser;
        private readonly IHospitalUser _hospitalUser;
        private readonly IAdminUser _adminUser;
        private readonly IEmployeeUser _employeeUser;
        public HomeController(ILogging logging, UserManager<ApplicationUser> userManager, IAssignPermission assignPermission, IDVBUser dVBUser, IHospitalUser hospitalUser, IAdminUser adminUser, IEmployeeUser employeeUser)
        {
            _logging = logging;
            _userManager = userManager;
            _assignPermission = assignPermission;
            _dvbUser = dVBUser;
            _hospitalUser = hospitalUser;
            _adminUser = adminUser;
            _employeeUser = employeeUser;
        }

        public IActionResult Index()
        {
            var roles = (((ClaimsIdentity)User.Identity).Claims
                  .Where(c => c.Type == ClaimTypes.Role)
                  .Select(c => c.Value)).FirstOrDefault();
            ViewBag.Roles = roles;

            var _detail = GetUserDetailFromAspNetUser().Result;
            var isTempPassword = IsTempPassword().Result;

            if (isTempPassword)
            {
                return RedirectToAction("ChangePassword", "Account", new { Area = "Identity" });

                //   return Redirect("ResetTempPassword");
            }

            //var userGuid = new Guid(User.Claims.ToList()[0].Value);
            // HttpContext.Session.Set("LoadModules", _assignPermission.GetAssignPermissionByParam(userGuid, null, null));
            // = _assignPermission.GetAssignPermissionByParam(userGuid, null, null);

            IEnumerable<AssignPermissionModel> assignPermissionModels = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, null, null);

            if (roles == Constants.SUPER_ADMIN)
            {
                PersistData(Constants.Dashboard, Constants.SUPER_ADMIN_DASHBOARD);
                return View(Constants.SUPER_ADMIN_DASHBOARD, assignPermissionModels);
            }
            if (roles == Constants.MASTER_ADMIN)
            {
                PersistData(Constants.Dashboard, Constants.MASTER_ADMIN_DASHBOARD);
                return Redirect("/UserManagement/AdminUser/AdminUserList");
            }
            var userList = _dvbUser.GetDVBUserByParam(null, null, _detail.Email, null, null);
            if (userList.Count() > 0)
            {
                var dvbuser = userList.First();
                PersistData(Constants.Dashboard, dvbuser.Dashboard == null ? "" : dvbuser.Dashboard);

                var dashboardUrl = dvbuser.DashboardUrl;
                if (string.IsNullOrEmpty(dashboardUrl) == false)
                    return Redirect(dashboardUrl);
            }

            var EmpUserData = _employeeUser.GetEmployeeUserByParam(null, null, _detail.Email, null);
            if (EmpUserData.Count() > 0)
            {
                PersistData(Constants.Dashboard, Constants.Emp_Dashboard);
                var dashboardUrl = "/EmployeeDashboard/Employee/Emp_Dashboard";

                if (string.IsNullOrEmpty(dashboardUrl) == false)
                    return Redirect(dashboardUrl);
            }

            else
            {
                var hospiList = _hospitalUser.GetHospitalUserByParam(null, null, _detail.Email, null, null);
                if (hospiList.Count() > 0)
                {
                    var user = hospiList.First();
                    PersistData(Constants.Dashboard, Constants.Hospital_Dashboard);

                    var dashboardUrl = "/Mediclaim/Dashboard/HospitalDashboard";
                    if (string.IsNullOrEmpty(dashboardUrl) == false)
                        return Redirect(dashboardUrl);
                }
            }
            return View(assignPermissionModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        void SetCookie(string key, string value)
        {
            ControllerContext.HttpContext.Response.Cookies.Delete(key);
            ControllerContext.HttpContext.Response.Cookies.Append(key, value, new CookieOptions { Expires = DateTime.Now.AddDays(1) });
        }
        void PersistData(string key, string value)
        {
            SetCookie(key, value);
            TempData[key] = value;
        }
        [HttpGet]
        [Route("/Identity/Account/ResetTempPassword/")]
        public async Task<IActionResult> ResetTempPassword()
        {
            return View();
        }

        //[HttpGet]
        //[Route("/Identity/Account/IsUserHavingTempPassword/")]
        //public async Task<IActionResult> IsUserHavingTempPassword()
        //{
        //    var _isTempPassword = await IsTempPassword();
        //    return Ok(_isTempPassword);
        //}


        [HttpPost]
        [Route("/Identity/Account/UpdateTempPassword/")]
        public async Task<IActionResult> UpdateTempPassword([FromBody] TempPasswordResetModel input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _userdetails = await _userManager.FindByNameAsync(User.Identity.Name);
                    var code = await _userManager.GeneratePasswordResetTokenAsync(_userdetails);
                    var result = await _userManager.ResetPasswordAsync(_userdetails, code, input.Password);
                    if (result.Succeeded)
                    {
                        _userdetails.IsTempPassword = false;
                        await _userManager.UpdateAsync(_userdetails);
                        return Ok();
                    }
                }
                else
                {
                    IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(allErrors);

                }
            }
            catch (Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return NoContent();
        }

        private async Task<bool> IsTempPassword()
        {
            var _detail = await GetUserDetailFromAspNetUser();
            return _detail.IsTempPassword;
        }
        private async Task<ApplicationUser> GetUserDetailFromAspNetUser()
        {
            var _userdetails = await _userManager.FindByNameAsync(User.Identity.Name);
            return _userdetails;
        }
        [HttpGet]

        public async Task<IActionResult> ViewSelfProfile(bool isReadOnly = true)
        {
            // var _userdetails = await _userManager.FindByNameAsync(User.Identity.Name);
            var role = (((ClaimsIdentity)User.Identity).Claims
                 .Where(c => c.Type == ClaimTypes.Role)
                 .Select(c => c.Value)).FirstOrDefault();
            if (role.Equals("DVB Pension Trust"))
            {
                var designation = _dvbUser.GetDesignations();
                var departments = _dvbUser.GetDepartments();
                ViewBag.Designation = designation;
                ViewBag.Departments = departments;
                DVBUserModel dVBUser = _dvbUser.GetDVBUserByUsername(User.Identity.Name);
                dVBUser.IsReadOnly = isReadOnly;
                return View("DVBUserForm", dVBUser);
            }
            if (role.Equals("Hospitals"))
            {
                HospitalUserModel hospitalUserModel = _hospitalUser.GetHospitalUserByUsername(User.Identity.Name);
                hospitalUserModel.IsReadOnly = isReadOnly;
                return View("HospitalUserForm", hospitalUserModel);
            }
            if (role.Equals(Constants.SUPER_ADMIN))
            {
                var adminUser = _adminUser.GetAdminUserByUsername(User.Identity.Name);

                ViewBag.IsReadOnly = isReadOnly;
                ViewBag.Mode = isReadOnly ? "view" : "Edit";
                ViewBag.Role = "Admin";
                ViewBag.AdminUserId = adminUser.AdminUserId;
                return View("AdminUserForm", adminUser);


            }
            if (role.Equals(Constants.MASTER_ADMIN))
            {
                var masterAdminUser = _userManager.Users.Where(x => x.UserName == User.Identity.Name).ToList().First();

                ViewBag.IsReadOnly = isReadOnly;
                ViewBag.Mode = isReadOnly ? "view" : "Edit";
                ViewBag.Role = Constants.MASTER_ADMIN;

                return View("MasterAdminUserForm", masterAdminUser);


            }

            return View();
        }
        [HttpPost]

        public async Task<IActionResult> UpdateHospitalUser(HospitalUserModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    if (User.Identity.IsAuthenticated == false)
                        return Json("Authenticated failed. Login and try again");

                    var hospitalUser = _hospitalUser.GetHospitalUserByParam(model.HospitalUserId, null, null, null, null).First();
                    var loginUser = await _userManager.FindByNameAsync(User.Identity.Name);

                    if (hospitalUser.EmailAddress.ToLower() != model.EmailAddress.ToLower())
                    {
                        var existingUsers = _hospitalUser.GetHospitalUserByParam(null, null, model.EmailAddress, null, null);//check if email is already used by other
                        if (existingUsers.Count() > 0)
                        {
                            return Json("userExists");
                        }
                        var isUserExist = _userManager.Users.Any(x => x.Email == model.EmailAddress);
                        if (isUserExist)
                            return Json("userExists");


                        if (loginUser.Email.ToLower() != hospitalUser.EmailAddress.ToLower())
                            return Json("User can update his email only");

                        hospitalUser.EmailAddress = model.EmailAddress;

                        var token = await _userManager.GenerateChangeEmailTokenAsync(loginUser, hospitalUser.EmailAddress);
                        await _userManager.ChangeEmailAsync(loginUser, hospitalUser.EmailAddress, token);
                    }
                    hospitalUser.Address = model.Address;

                    hospitalUser.ModifideBy = User.Identity.Name;


                    if (hospitalUser.PhoneNumber != model.PhoneNumber)
                    {
                        await _userManager.SetPhoneNumberAsync(loginUser, hospitalUser.PhoneNumber);
                        hospitalUser.PhoneNumber = model.PhoneNumber;

                    }
                    id = _hospitalUser.UpdateHospitalUser(hospitalUser);

                    SendEmail.EmailAction(hospitalUser.NameOfHospital, hospitalUser.EmailAddress, "Profile updated successfully", "Hi " + hospitalUser.Username + ",\n your profile has been updated successfully \r\nif not updated by you, please contact to administrator");

                }
                else
                {
                    IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(allErrors);
                }
            }
            catch (System.Exception ex)
            {
                _logging.Savelog("UpdateHospitalUser", $"{ex.Message}");
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDVBUser(DVBUserModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    if (User.Identity.IsAuthenticated == false)
                        return Json("Authenticated failed. Login and try again");

                    var dvbUser = _dvbUser.GetDVBUserByParam(model.DVBUserId, null, null, null, null).First();
                    var loginUser = await _userManager.FindByNameAsync(User.Identity.Name);

                    if (dvbUser.EmailAddress.ToLower() != model.EmailAddress.ToLower())
                    {
                        if (loginUser.Email.ToLower() != dvbUser.EmailAddress.ToLower())
                            return Json("User can update his email only");


                        var existingUser = _dvbUser.GetDVBUserByParam(null, null, model.EmailAddress, null, null);//check if email is already used

                        if (existingUser.Count() > 0)
                        {
                            return Json("userExists");
                        }
                        var isUserExist = _userManager.Users.Any(x => x.Email == model.EmailAddress);
                        if (isUserExist)
                            return Json("userExists");

                        var token = await _userManager.GenerateChangeEmailTokenAsync(loginUser, dvbUser.EmailAddress);
                        await _userManager.ChangeEmailAsync(loginUser, dvbUser.EmailAddress, token);
                        dvbUser.EmailAddress = model.EmailAddress;
                    }
                    if (dvbUser.PhoneNumber != model.PhoneNumber)
                    {
                        await _userManager.SetPhoneNumberAsync(loginUser, dvbUser.PhoneNumber);
                        dvbUser.PhoneNumber = model.PhoneNumber;
                    }

                    dvbUser.Address = model.Address;
                    dvbUser.ModifideBy = User.Identity.Name;
                    id = _dvbUser.UpdateDVBUser(dvbUser);


                    SendEmail.EmailAction(dvbUser.Name, dvbUser.EmailAddress, "Profile updated successfully", "Hi " + dvbUser.Username + ",\nyour profile has been updated successfully \r\nif not updated by you, please contact to administrator");

                }
                else
                {
                    IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(allErrors);
                }
            }
            catch (System.Exception ex)
            {
                _logging.Savelog("UpdateDVBUser", $"{ex.Message}");
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(id);
        }



        [HttpPost]

        public async Task<IActionResult> UpdateAdminUser([FromBody] AdminUserModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    var loginUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    var adminuser = _adminUser.GetAdminUserByParam(model.AdminUserId, null, null, null).First();


                    if (adminuser.EmailAddress.ToLower() != model.EmailAddress.ToLower())
                    {
                        var existingUsers = _adminUser.GetAdminUserByParam(null, null, model.EmailAddress, null);//check if email is already used by other
                        if (existingUsers.Count() > 0)
                        {
                            return Json("userExists");
                        }
                        var isUserExist = _userManager.Users.Any(x => x.Email == model.EmailAddress);
                        if (isUserExist)
                            return Json("userExists");



                        adminuser.EmailAddress = model.EmailAddress;

                        var token = await _userManager.GenerateChangeEmailTokenAsync(loginUser, adminuser.EmailAddress);
                        await _userManager.ChangeEmailAsync(loginUser, adminuser.EmailAddress, token);
                    }

                    adminuser.Name = model.Name;
                    adminuser.PhoneNumber = model.PhoneNumber;
                    adminuser.ModifideBy = User.Identity.Name;
                    id = _adminUser.UpdateAdminUser(adminuser);
                    SendEmail.EmailAction(adminuser.Name, adminuser.EmailAddress, "Profile updated successfully", "Hi " + adminuser.UserName + ",\nyour profile has been updated successfully \r\nif not updated by you, please contact to administrator");

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

            return Ok(id);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateMasterAdminUser(ApplicationUser model)
        {
            int id = 0;
            try
            {

                var loginUser = await _userManager.FindByNameAsync(User.Identity.Name);



                if (loginUser.Email.ToLower() != model.Email.ToLower())
                {
                    var isEmailExist = _userManager.Users.Any(x => x.Email == model.Email);//check if email is already used by other
                    if (isEmailExist)
                    {
                        TempData["ErrorMessage"] = "Email-id is already used";
                        return   RedirectToAction(nameof(ViewSelfProfile), new { isReadOnly = false });
                        //return  Json("userExists");
                    }


                    var token = await _userManager.GenerateChangeEmailTokenAsync(loginUser, model.Email);
                    await _userManager.ChangeEmailAsync(loginUser, model.Email, token);

                    SendEmail.EmailAction(loginUser.fullName, model.Email, "Email updated successfully", "Hi " + loginUser.UserName + ",\nyour Email has been updated successfully \r\nif not updated by you, please contact to administrator");

                }

            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            TempData["SuccessMessage"] = "Profile updated successfully";
            return RedirectToAction(nameof(ViewSelfProfile), new { isReadOnly = false });
        }

    }
}
