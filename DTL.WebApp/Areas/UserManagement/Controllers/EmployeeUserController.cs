using DTL.Business.Logging;
using DTL.Business.UserManagement;
using DTL.Model.Models.UserManagement;
using DTL.WebApp.Common.CommonClasses;
using DTL.WebApp.Common.Extensions;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    public class EmployeeUserController : Controller
    {
        private readonly IEmployeeUser _employeeUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogging _logging;
        private readonly IConfiguration _configuration;

        public EmployeeUserController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,

            ILogging logging,
            IEmployeeUser employeeUser,
            IConfiguration configuration)
        {
            _employeeUser = employeeUser;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logging = logging;
        }

        public IActionResult EmployeeUserList(bool isArchived = false)
        {
            ViewBag.IsArchived = isArchived;
            if (isArchived)
            {
                var _employeeArchivedUserList = _employeeUser.GetArchiveEmployeeUser();
                return View(_employeeArchivedUserList.OrderByDescending(x => x.CreatedDate));
            }
            var _employeeUserList = _employeeUser.GetEmployeeUserByParam(null, null, null, null);
            return View(_employeeUserList.OrderByDescending(x => x.CreatedDate));
        }

        [HttpGet]
        [Route("/UserManagement/EmployeeUser/EmployeeUserForm/{mode?}/{id?}")]
        public IActionResult EmployeeUserForm([FromRoute] string mode = "create", [FromRoute] int? id = 0)
        {
            ViewBag.Mode = mode;
            ViewBag.EmpUserId = id;
            EmployeeUserModel model = null;
            if (id == 0)
                model = new EmployeeUserModel();
            else
                model = _employeeUser.GetEmployeeUserByParam(id, null, null, null).FirstOrDefault();
            if (mode == "view")
                model.IsReadOnly = true;
            return View(model);
        }

        [HttpPost]
        [Route("/UserManagement/EmployeeUser/Create")]
        public async Task<IActionResult> CreateEmployeeUser(EmployeeUserModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    //model.NameOfEmployee = "none";

                    if (model.EmpUserid != 0)
                        return await UpdateEmployeeUserAsync(model);

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
                        id = _employeeUser.AddEmployeeUser(model);

                        if (id > 0)
                        {
                            var returnValue = await GenerateEmployeeUserCredentialById(id, true);
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
                _logging.Savelog("CreateEmployeeUser", $"{ex.Message + ex.StackTrace}");
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(id);
        }



        public async Task<IActionResult> GenerateEmployeeUserCredentialById(int EmpUserid, bool flag = false)
        {
            if (!flag) { return Ok(); }
            string returnMessage = "";
            var smsservice = new MessageService(_configuration);
            try
            {
                var _user = _employeeUser.GetEmployeeUserByParam(EmpUserid, null, null, null).FirstOrDefault();

                var user = new ApplicationUser()
                {
                    Email = _user.EmailAddress,
                    PhoneNumber = _user.PhoneNumber,
                    UserName = _user.Username,
                    fullName = _user.NameOfEmployee,
                    EmailConfirmed = true,
                    IsTempPassword = true
                };
                var tempPassword = "";
                string password = tempPassword.GeneratePassword();
                string textmessage = string.Format("Hi {0},  Your temporary password is {1} Thanks ", _user.NameOfEmployee, password);

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
                        _logging.Savelog("GenerateEmployeeUserCredentialById", $"Sms sent status for {_user.EmailAddress} is {smsstatus}");
                        returnMessage = "success";
                    }
                }
                else
                {
                    var userResult = await _userManager.CreateAsync(user, password);
                    var setPhoneNumber = await _userManager.SetPhoneNumberAsync(user, user.PhoneNumber);

                    if (userResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Hospitals");   /*---chnage after role decide*/
                        SendEmail.SendCredentialEmail(user.fullName, user.Email, user.UserName, password);
                        var smsstatus = await smsservice.SendCredential(user.PhoneNumber, textmessage);
                        _logging.Savelog("GenerateEmployeeUserCredentialById", $"Sms sent status for {_user.EmailAddress} is {smsstatus}");
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
                _logging.Savelog("GenerateEmployeeUserCredentialById", $"{ex.Message}");
                return Json("Error");
            }
        }
        private bool IsDuplicateUserName(string username)
        {
            return _userManager.Users.Any(x => x.UserName == username);
        }

        private bool IsDuplicateEmailId(string emailAddress)
        {
            return _userManager.Users.Any(x => x.Email == emailAddress);
        }

        private Task<IActionResult> UpdateEmployeeUserAsync(EmployeeUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
