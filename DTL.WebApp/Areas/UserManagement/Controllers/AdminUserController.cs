using DTL.Business.Logging;
using DTL.Business.UserManagement;
using DTL.Model.Models.UserManagement;
using DTL.WebApp.Common.CommonClasses;
using DTL.WebApp.Common.Extensions;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace DTL.WebApp.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    [Authorize(Roles = "MasterAdmin")]
    public class AdminUserController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IAdminUser _adminUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogging _logging;
        public AdminUserController(IAdminUser adminUser,
              UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogging logging)
        {
            _adminUser = adminUser;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logging = logging;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult AdminUserList()
        {
            var adminUserList = _adminUser.GetAdminUserByParam(null, null, null, null);
            return View(adminUserList.OrderByDescending(x=> x.CreatedDate));
        }

        [HttpGet]
        [Route("/UserManagement/AdminUser/AdminUserForm/{mode?}/{id?}")]
        public IActionResult AdminUserForm([FromRoute] string mode = "create", [FromRoute] int? id = 0)
        {
            ViewBag.Mode = mode;
            ViewBag.AdminUserId = id;
            ViewBag.IsReadOnly = mode == "view";
            return View();
        }

        [HttpPost]
        [Route("/UserManagement/AdminUser/Create")]
        public async Task<IActionResult> CreateAdminUser([FromBody] AdminUserModel model)
        {
            int id = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    if (IsDuplicateUsername(model.UserName))
                    {
                        return Json("userNameExists");
                    }
                    if (!IsDuplicateEmailId(model.EmailAddress))
                    {
                        model.ID = Guid.NewGuid();
                        model.CreatedBy = User.Identity.Name;
                        id = _adminUser.AddAdminUser(model);
                        await GenerateAdminUserCredentialById(id);
                    }

                    else
                    {
                        return Json("userExists");
                    }
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
                _logging.Savelog("CreateAdminUser", ex.Message + ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(id);
        }

        [HttpPost]
        [Route("/UserManagement/AdminUser/Update")]
        public async Task<IActionResult> UpdateAdminUserAsync([FromBody] AdminUserModel model)
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
                    adminuser.PhoneNumber=  model.PhoneNumber;
                    adminuser.ModifideBy = User.Identity.Name;
                    id = _adminUser.UpdateAdminUser(adminuser);

                    SendEmail.EmailAction(adminuser.Name, adminuser.EmailAddress, "Profile updated successfully", "Hi "+ adminuser.UserName + ",\nyour profile updated by super admin successfully.");


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
                _logging.Savelog("UpdateAdminUserAsync", ex.Message + ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(id);
        }

        [HttpDelete]
        [Route("/UserManagement/AdminUser/Delete/{id}")]
        public async Task< IActionResult> DeleteAdminUser([FromRoute] int Id)
        {
            try
            {
                var adminUsers = _adminUser.GetAdminUserByParam(Id, null, null, null);
                var model = adminUsers.First();

                var deleted = await DeleteUserIdentity(model.EmailAddress);

                if (deleted)
                {
                    _adminUser.DeleteAdminUserPermanently(model.AdminUserId);
                }
               // _adminUser.AdminUserToggleIsDeleteFlag(Id, true, User.Identity.Name);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                _logging.Savelog("DeleteAdminUser", ex.Message + ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }
        [NonAction]
        async Task<bool> DeleteUserIdentity(string emailId)
        {


            var user = await _userManager.FindByEmailAsync(emailId);
            if (user != null)
            {

                var rolesForUser = await _userManager.GetRolesAsync(user);

                var logins = await _userManager.GetLoginsAsync(user);

                var claims = await _userManager.GetClaimsAsync(user);

                foreach (var claim in claims)
                {
                    await _userManager.RemoveClaimAsync(user, claim);
                }

                foreach (var login in logins)
                {
                    await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {

                        var result = await _userManager.RemoveFromRoleAsync(user, item);
                    }
                }


                await _userManager.DeleteAsync(user);

            }
            return true;

        }

      
        [HttpGet]
        [Route("/UserManagement/AdminUser/GetAdminUserById/{id}")]
        public IActionResult GetAdminUserById([FromRoute] int Id)
        {
            var adminUser = _adminUser.GetAdminUserByParam(Id, null, null, null);
            return Json(adminUser.FirstOrDefault());
        }

        public async Task<IActionResult> GenerateAdminUserCredentialById(int adminuserid)
        {
            string returnMessage = "";
            try
            {
                var smsservice = new MessageService(_configuration);
                var _user = _adminUser.GetAdminUserByParam(adminuserid, null, null, null).FirstOrDefault();
                var user = new ApplicationUser()
                {
                    Email = _user.EmailAddress,
                    PhoneNumber = _user.PhoneNumber,
                    UserName = _user.UserName,
                    fullName = _user.Name,
                    EmailConfirmed = true,
                    IsTempPassword = true
                };
                var tempPassword = "";
                string password = tempPassword.GeneratePassword();
                string textmessage = string.Format("Hi {0},  Your temporary password is {1} Thanks ", _user.UserName, password);

                //_logging.Savelog("GenerateAdminUserCredentialById", "TempPassword Generated");

                //Check if user exists
                var loginUser = await _userManager.FindByEmailAsync(_user.EmailAddress);
                if (loginUser != null)
                {
                    // _logging.Savelog($"GenerateAdminUserCredentialById", $"{_user.EmailAddress} user found");

                    var code = await _userManager.GeneratePasswordResetTokenAsync(loginUser);
                    var result = await _userManager.ResetPasswordAsync(loginUser, code, password);

                 

                    if (result.Succeeded)
                    {
            
                        loginUser.IsTempPassword = true;
                        await _userManager.UpdateAsync(loginUser);
                        SendEmail.SendCredentialEmail(user.fullName, user.Email, user.UserName, password);

                        //_logging.Savelog("GenerateAdminUserCredentialById", $"Email sent for {_user.EmailAddress} ");

                        var smsstatus = await smsservice.SendCredential(_user.PhoneNumber, textmessage);
                        _logging.Savelog("GenerateAdminUserCredentialById", $"Sms sent status for {_user.EmailAddress} is {smsstatus}");
                        returnMessage = "success";
                    }
                    else
                    {
                        returnMessage = string.Join(",", result.Errors.Select(x => x.Code + " " + x.Description));
                    }
                }
                else
                {
                    var userResult = await _userManager.CreateAsync(user, password);
                    var setPhoneNumber = await _userManager.SetPhoneNumberAsync(user, user.PhoneNumber);

                    if (userResult.Succeeded)
                    {

                        await _userManager.AddToRoleAsync(user, "SuperAdmin");
                        SendEmail.SendCredentialEmail(user.fullName, user.Email, user.UserName, password);
                        //SendEmail.GenerateCreedential(user.fullName, user.Email, password);
                        var smsstatus = await smsservice.SendCredential(user.PhoneNumber, textmessage);
                        _logging.Savelog("GenerateAdminUserCredentialById", $"Sms sent status for {_user.EmailAddress} is {smsstatus}");
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
                _logging.Savelog("GenerateAdminUserCredentialById", ex.Message);
                return Json("Error");
            }
        }

        [HttpPost]
        [Route("/UserManagement/AdminUser/UnArchive/{id}")]
        public IActionResult AdminUserUnArchive([FromRoute] int Id)
        {
            try
            {
                _adminUser.AdminUserToggleIsDeleteFlag(Id, false, User.Identity.Name);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }

        [HttpGet]
        [Route("/UserManagement/AdminUser/Archive")]
        public IActionResult AdminUserArchive()
        {
            var adminUser = _adminUser.GetArchiveAdminUser();
            return View(adminUser);
        }

        private bool IsDuplicateEmailId(string emailAddres)
        {
            return _userManager.Users.Any(x => x.Email == emailAddres);
        }
        private bool IsDuplicateUsername(string username)
        {
            return _userManager.Users.Any(x => x.UserName == username);
        }
    }
}
