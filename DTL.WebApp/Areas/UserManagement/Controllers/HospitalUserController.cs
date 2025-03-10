using DocumentFormat.OpenXml.Wordprocessing;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    [Authorize(Roles = "SuperAdmin")]
    public class HospitalUserController : Controller
    {
        private readonly IHospitalUser _hospitalUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogging _logging;
        public HospitalUserController(IHospitalUser hospitalUser,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogging logging)
        {
            _hospitalUser = hospitalUser;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logging = logging;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HospitalUserList(bool isArchived = false)
        {
            ViewBag.IsArchived = isArchived;
            if (isArchived)
            {
                var _hospitalArchivedUserList = _hospitalUser.GetArchiveHospitalUser();
                return View(_hospitalArchivedUserList.OrderByDescending(x => x.CreatedDate));
            }
            var _hospitalUserList = _hospitalUser.GetHospitalUserByParam(null, null, null, null, null);
            return View(_hospitalUserList.OrderByDescending(x => x.CreatedDate));
        }

        [HttpGet]
        [Route("/UserManagement/HospitalUser/HospitalUserForm/{mode?}/{id?}")]
        public IActionResult HospitalUserForm([FromRoute] string mode = "create", [FromRoute] int? id = 0)
        {
            ViewBag.Mode = mode;
            ViewBag.HospitalUserId = id;
            HospitalUserModel model = null;
            if (id == 0)
                model = new HospitalUserModel();
            else
                model = _hospitalUser.GetHospitalUserByParam(id, null, null, null, null).FirstOrDefault();

            if (mode == "view")
                model.IsReadOnly = true;

            return View(model);
        }
        [HttpPost]
        [Route("/UserManagement/HospitalUser/Create")]
        public async Task<IActionResult> CreateHospitalUser(HospitalUserModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    model.TypeOfHospital = "none";

                    if (model.HospitalUserId != 0)
                        return await UpdateHospitalUserAsync(model);

                    if (IsDuplicateEmailId(model.EmailAddress))
                    {
                        return Json("userExists");

                    }
                    else if (IsDuplicateUserName(model.Username))
                    {
                        return Json("DuplicateUserName");
                    }
                    else
                    {
                        model.ID = Guid.NewGuid();
                        model.CreatedBy = User.Identity.Name;
                        id = _hospitalUser.AddHospitalUser(model);

                        if (id > 0)
                        {
                            var returnValue = await GenerateHospitalUserCredentialById(id, true);
                        }
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
                _logging.Savelog("CreateHospitalUser", $"{ex.Message + ex.StackTrace}");
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(id);
        }

        [HttpPost]
        [Route("/UserManagement/HospitalUser/Update")]
        public async Task<IActionResult> UpdateHospitalUserAsync(HospitalUserModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    model.ModifideBy = User.Identity.Name;
                  

                    var hospUsers = _hospitalUser.GetHospitalUserByParam(model.HospitalUserId, null, null, null, null);
                    if (hospUsers.Count() > 0)
                    {
                       
                        var hospUser = hospUsers.First();
                        model.Username = hospUser.Username;
                        id = _hospitalUser.UpdateHospitalUser(model);

                        if (hospUser.EmailAddress != model.EmailAddress)
                        {
                            var applicationUser = await _userManager.Users.FirstAsync(x => x.Email == hospUser.EmailAddress);
                            if (applicationUser != null)
                            {

                                await ChangeEmailAsync(applicationUser, model.EmailAddress);
                   
                            }

                        }
                    }
                    SendEmail.EmailAction(model.NameOfHospital, model.EmailAddress, "Profile updated successfully", "Hi " + model.Username + ",\nyour profile updated by admin successfully.");

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
        async Task ChangeEmailAsync(ApplicationUser loginUser, string newEmail)
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(loginUser, newEmail);
            await _userManager.ChangeEmailAsync(loginUser, newEmail, token);
        }


        [HttpDelete]
        [Route("/UserManagement/HospitalUser/Delete/{id}")]
        public async Task<IActionResult> DeleteHospitalUser([FromRoute] int Id)
        {
            try
            {
                //   _hospitalUser.HospitalUserToggleIsDeleteFlag(Id, true, User.Identity.Name);
                var hospitalUsers = _hospitalUser.GetHospitalUserByParam(Id, null, null, null, null);
                var model = hospitalUsers.First();

                var deleted = await DeleteUserIdentity(model.EmailAddress);

                if (deleted)
                {
                    _hospitalUser.DeleteHospitalUserPermanently(model.ID);
                }
            }
            catch (System.Exception ex)
            {
                _logging.Savelog("DeleteHospitalUser", $"{ex.Message} {ex.StackTrace}");
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }
        [NonAction]
        public async Task<bool> DeleteUserIdentity(string emailId)
        {


            var user = await _userManager.FindByEmailAsync(emailId);
            if (user == null) return true;

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

            return true;

        }
        [HttpGet]
        [Route("/UserManagement/HospitalUser/GetHospitalUserById/{id}")]
        public IActionResult GetHospitalUserById([FromRoute] int Id)
        {
            var hospitalUser = _hospitalUser.GetHospitalUserByParam(Id, null, null, null, null);
            return Json(hospitalUser.FirstOrDefault());
        }

        public async Task<IActionResult> GenerateHospitalUserCredentialById(int hospitaluserid, bool flag = false)
        {
            if (!flag) { return Ok(); }
            string returnMessage = "";
            var smsservice = new MessageService(_configuration);
            try
            {
                var _user = _hospitalUser.GetHospitalUserByParam(hospitaluserid, null, null, null, null).FirstOrDefault();
                var user = new ApplicationUser()
                {
                    Email = _user.EmailAddress,
                    PhoneNumber = _user.PhoneNumber,
                    UserName = _user.Username,
                    fullName = _user.NameOfHospital,
                    EmailConfirmed = true,
                    IsTempPassword = true
                };
                var tempPassword = "";
                string password = tempPassword.GeneratePassword();
                string textmessage = string.Format("Hi {0},  Your temporary password is {1} Thanks ", _user.NameOfHospital, password);

                //Check if user exists
                var loginUser = await _userManager.FindByEmailAsync(_user.EmailAddress);
                if (loginUser != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(loginUser);
                    var result = await _userManager.ResetPasswordAsync(loginUser, code, password);
                    if (result.Succeeded)
                    {
                        loginUser.IsTempPassword = true;
                        await _userManager.UpdateAsync(loginUser);
                        SendEmail.SendCredentialEmail(user.fullName, user.Email, user.UserName, password);
                        var smsstatus = await smsservice.SendCredential(user.PhoneNumber, textmessage);
                        _logging.Savelog("GenerateHospitalUserCredentialById", $"Sms sent status for {_user.EmailAddress} is {smsstatus}");
                        returnMessage = "success";
                    }
                }
                else
                {
                    var userResult = await _userManager.CreateAsync(user, password);
                    var setPhoneNumber = await _userManager.SetPhoneNumberAsync(user, user.PhoneNumber);

                    if (userResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Hospitals");
                        SendEmail.SendCredentialEmail(user.fullName, user.Email, user.UserName, password);
                        var smsstatus = await smsservice.SendCredential(user.PhoneNumber, textmessage);
                        _logging.Savelog("GenerateHospitalUserCredentialById", $"Sms sent status for {_user.EmailAddress} is {smsstatus}");
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
                _logging.Savelog("GenerateHospitalUserCredentialById", $"{ex.Message}");
                return Json("Error");
            }
        }

        public IActionResult HospitalUserArchive()
        {
            var hospitalUser = _hospitalUser.GetArchiveHospitalUser();
            return View(hospitalUser);
        }
        public IActionResult HospitalUserAssignPermission()
        {
            return View();
        }

        [HttpPost]
        [Route("/UserManagement/HospitalUser/UnArchive/{id}")]
        public IActionResult UnArchiveHospitalUser([FromRoute] int Id)
        {
            try
            {
                _hospitalUser.HospitalUserToggleIsDeleteFlag(Id, false, User.Identity.Name);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }

        private bool IsDuplicateEmailId(string emailAddres)
        {
            return _userManager.Users.Any(x => x.Email == emailAddres);
        }
        private bool IsDuplicateUserName(string username)
        {
            return _userManager.Users.Any(x => x.UserName == username);
        }
    }
}
