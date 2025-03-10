using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Presentation;
using DTL.Business.Logging;
using DTL.Business.UserManagement;
using DTL.Model.Models;
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
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    [Authorize(Roles = "SuperAdmin")]
    public class DVBUserController : Controller
    {
        private readonly IDVBUser _dvbUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogging _logging;

        public DVBUserController(IDVBUser dvbUser,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogging logging)
        {
            _dvbUser = dvbUser;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logging = logging;
        }

        public IActionResult DVBUserList(bool isArchived = false)
        {
            var _dvbUserList = _dvbUser.GetDVBUserByParam(null, null, null, null, null);

            var list = _dvbUserList.Where(x => x.IsDeleted == isArchived).ToList();
            list.ForEach(x => x.IsReadOnly = isArchived);
            ViewBag.isArchived = isArchived;
            return View(list.OrderByDescending(x => x.CreatedDate));
        }

        [HttpGet]
        [Route("/UserManagement/DVBUser/DVBUserForm/{mode?}/{id?}")]
        public IActionResult DVBUserForm([FromRoute] string mode = "create", [FromRoute] int? id = 0)
        {
            ViewBag.Mode = mode;
            ViewBag.DVBUserId = id;
            //Logic for populating designations
            var designations = _dvbUser.GetDesignations();
            var departments = _dvbUser.GetDepartments();
            ViewBag.Designations = designations;
            ViewBag.Departments = departments;
            DVBUserModel model;
            if (id == 0)
                model = new DVBUserModel();
            else
            {
                var dvbUsers = _dvbUser.GetDVBUserByParam(id, null, null, null, null);
                model = dvbUsers.First();
                if (mode == "View")
                    model.IsReadOnly = true;
            }

            return View(model);
        }

        [HttpPost]
        [Route("/UserManagement/DVBUser/Create")]
        public async Task<IActionResult> CreateDVBUser(DVBUserModel model)
        {
            int id = 0;
            string message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ID != Guid.Empty)
                    {
                        return await UpdateDVBUser(model);
                    }
                    if (IsDuplicateEmailId(model.EmailAddress))
                    {
                        message = "userExists";
                        return Json(message);
                    }
                    else if (IsDuplicateUsername(model.Username))
                    {
                        message = "usernameExists";
                        return Json(message);
                    }
                    else
                    {
                        model.ID = Guid.NewGuid();
                        model.CreatedBy = User.Identity.Name;
                        id = _dvbUser.AddDVBUser(model);
                        if (id > 0)
                        {
                            var returnValue = await GenerateDVBUserCredentialById(id, true);
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
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                _logging.Savelog("CreateDVBUser", $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(id);
        }

        [HttpPost]
        [Route("/UserManagement/DVBUser/Update")]
        public async Task<IActionResult> UpdateDVBUser(DVBUserModel model)
        {
            int id = 0;
            string username = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    model.ModifideBy = User.Identity.Name;
                    var dvbUsers = _dvbUser.GetDVBUserByParam(model.DVBUserId, null, null, null, null);

                    if (dvbUsers.Count() > 0)
                    {
                        var dvbUser = dvbUsers.First();
                        model.Username = dvbUser.Username;
                        id = _dvbUser.UpdateDVBUser(model);                  
                        username = dvbUser.Username;
                        if (dvbUser.EmailAddress != model.EmailAddress)
                        {
                            var applicationUser = await _userManager.Users.FirstAsync(x => x.Email == dvbUser.EmailAddress);
                            if (applicationUser != null)
                            {

                                await ChangeEmailAsync(applicationUser, model.EmailAddress);

                            }

                        }
                    }
                    SendEmail.EmailAction(model.Name, model.EmailAddress, "Profile updated successfully", "Hi " + username + ",\nyour profile updated by admin successfully.");

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

        async Task ChangeEmailAsync(ApplicationUser loginUser, string newEmail)
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(loginUser, newEmail);
            await _userManager.ChangeEmailAsync(loginUser, newEmail, token);
        }

        [HttpDelete]
        [Route("/UserManagement/DVBUser/Delete/{id}")]
        public async Task<IActionResult> DeleteDVBUser([FromRoute] int Id)
        {
            try
            {
                var dvbUsers = _dvbUser.GetDVBUserByParam(Id, null, null, null, null);
                var model = dvbUsers.First();

                var deleted = await DeleteUserIdentity(model.EmailAddress);

                if (deleted)
                {
                    _dvbUser.DeleteDVBUserPermanently(model.ID);
                }

                // _dvbUser.DVBUserToggleIsDeleteFlag(Id, true, User.Identity.Name);
            }
            catch (System.Exception ex)
            {
                _logging.Savelog("DeleteDVBUser", $"{ex.Message} {ex.StackTrace}");
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
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
        [Route("/UserManagement/DVBUser/GetDVBUserById/{id}")]
        public IActionResult GetDVBUserById([FromRoute] int Id)
        {
            var dvbUser = _dvbUser.GetDVBUserByParam(Id, null, null, null, null).FirstOrDefault();
            var user = _userManager.FindByEmailAsync(dvbUser.EmailAddress).Result;
            //dvbUser.Username = user.Email;            
            return Json(dvbUser);
        }
        [NonAction]
        public async Task<ActionResult> GenerateDVBUserCredentialById(int dvbUserId, bool flag = false)
        {
            string returnMessage = "";
            if (!flag) { return Ok(); }
            try
            {
                var smsservice = new MessageService(_configuration);
                var _user = _dvbUser.GetDVBUserByParam(dvbUserId, null, null, null, null).FirstOrDefault();
                var user = new ApplicationUser()
                {
                    Email = _user.EmailAddress,
                    PhoneNumber = _user.PhoneNumber,
                    UserName = _user.Username,
                    fullName = _user.Name,
                    EmailConfirmed = true,
                    IsTempPassword = true,
                    Designation = _user.Designation
                };
                var tempPassword = "";
                string password = tempPassword.GeneratePassword();
                string textmessage = string.Format("Hi {0},  Your temporary password is {1} Thanks ", _user.Username, password);

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
                        _logging.Savelog("GenerateDVBUserCredentialById", $"Sms sent status for {_user.EmailAddress} is {smsstatus}");
                        returnMessage = "success";
                    }
                }
                else
                {
                    var userResult = await _userManager.CreateAsync(user, password);
                    var setPhoneNumber = await _userManager.SetPhoneNumberAsync(user, user.PhoneNumber);

                    if (userResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "DVB Pension Trust");
                        SendEmail.SendCredentialEmail(user.fullName, user.Email, user.UserName, password);
                        var smsstatus = await smsservice.SendCredential(user.PhoneNumber, textmessage);
                        _logging.Savelog("GenerateDVBUserCredentialById", $"Sms sent status for {_user.EmailAddress} is {smsstatus}");
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
                _logging.Savelog("GenerateDVBUserCredentialById", $"{ex.Message}");
                return Json("Error");
            }
        }

        public IActionResult DVBUserArchive()
        {
            var archieveUsers = _dvbUser.GetArchiveDVBUser();
            return View(archieveUsers);
        }
        public IActionResult DVBUserAssignPermission()
        {
            return View();
        }

        [HttpGet]
        [Route("/UserManagement/DVBUser/UnArchive/{id}")]
        public IActionResult DVBUserUnArchive(int Id)
        {
            try
            {
                _dvbUser.DVBUserToggleIsDeleteFlag(Id, false, User.Identity.Name);
            }
            catch (System.Exception ex)
            {
                _logging.Savelog("DVBUserUnArchive", $"{ex.Message}");
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return RedirectToAction(nameof(DVBUserList), new { isArchived = true });
        }

        [HttpGet]
        [Route("/UserManagement/DvbUser/Dashboard/{dashboardFor}")]
        public IActionResult GetDashboardFor([FromRoute] string dashboardFor)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(dashboardFor))
            {
                return BadRequest("Invalid dashboard selection");
            }
            var dashboardDetail = _dvbUser.GetDashboards(dashboardFor).ToList();
            if (dashboardDetail.Count > 0)
            {
                return Ok(dashboardDetail);
            }
            else
            {
                return NotFound("No data found");
            }
        }
        private bool IsDuplicateEmailId(string emailAddres)
        {
            bool isfound = false;

            isfound = _userManager.Users.Any(x => x.Email == emailAddres);
            if (isfound == false)
            {
                var list = _dvbUser.GetDVBUserByParam(null, null, emailAddres, null, null);
                isfound = list.Count() > 0;
            }
            return isfound;

        }
        private bool IsDuplicateUsername(string username)
        {
            return _userManager.Users.Any(x => x.UserName == username);

        }
    }
}
