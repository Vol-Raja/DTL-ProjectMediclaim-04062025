using DocumentFormat.OpenXml.Spreadsheet;
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
    [Authorize(Roles = "SuperAdmin,Master")]
    public class MasterAdminUserController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogging _logging;
        public MasterAdminUserController(
              UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            //   IConfiguration configuration,
            //   ILogging logging
            )
        {

            _userManager = userManager;
            _roleManager = roleManager;
            // _configuration = configuration;
            //  _logging = logging;
        }

        [NonAction]
        public async Task<IActionResult> CreateMasterAdminUser(string token)
        {


            if (string.IsNullOrEmpty(token) || token != "BB2762B7-C229-4E63-97D2-E3EE630FC928") return Ok();

            UserViewModel userViewModel = new UserViewModel
            {
                UserName = "admin",
                fullName = "adminsystem",
                Email = "volsadhana@gmail.com",
                PhoneNumber = "8870067876"
            };
            if (IsDuplicateUsername(userViewModel.UserName))
            {
                throw new Exception("userNameExists");
            }
            if (!IsDuplicateEmailId(userViewModel.Email))
            {

                await GenerateMasterAdminUserCredentialById(userViewModel);
            }

            else
            {
                throw new Exception("EmailIdExists");
            }
            return Ok();
        }


        [NonAction]
        private async Task<IActionResult> GenerateMasterAdminUserCredentialById(UserViewModel _user)
        {
            string returnMessage = "";
            try
            {
                //  var smsservice = new MessageService(_configuration);

                if (!await _roleManager.RoleExistsAsync(Common.Constants.MASTER_ADMIN))
                {
                    var role = new IdentityRole();
                    role.Name = Common.Constants.MASTER_ADMIN;
                    await _roleManager.CreateAsync(role);

                }

                var user = new ApplicationUser()
                {
                    Email = _user.Email,
                    PhoneNumber = _user.PhoneNumber,
                    UserName = _user.UserName,
                    fullName = _user.fullName,
                    EmailConfirmed = true,
                    IsTempPassword = true
                };
                var tempPassword = "";
                string password = tempPassword.GeneratePassword();
                string textmessage = string.Format("Hi {0},  Your temporary password is {1} Thanks ", _user.UserName, password);


                //Check if user exists
                var loginUser = await _userManager.FindByEmailAsync(_user.Email);
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

                        //  var smsstatus = await smsservice.SendCredential(_user.PhoneNumber, textmessage);
                        //  _logging.Savelog("GenerateAdminUserCredentialById", $"Sms sent status for {_user.Email} is {smsstatus}");
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

                        await _userManager.AddToRoleAsync(user, Common.Constants.MASTER_ADMIN);
                        SendEmail.SendCredentialEmail(user.fullName, user.Email, user.UserName, password);

                        //   var smsstatus = await smsservice.SendCredential(user.PhoneNumber, textmessage);
                        //  _logging.Savelog("GenerateAdminUserCredentialById", $"Sms sent status for {_user.Email} is {smsstatus}");
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
                _logging.Savelog("GenerateMasterAdminUserCredentialById", ex.Message);
                return Json("Error");
            }
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
