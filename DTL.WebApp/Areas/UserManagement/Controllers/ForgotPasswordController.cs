using DTL.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DTL.Business.UserManagement;
using DTL.Model.Models.UserManagement;
using DTL.WebApp.Common.Extensions;
using DocumentFormat.OpenXml.Spreadsheet;
using DTL.WebApp.Common.CommonClasses;

namespace DTL.WebApp.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    public class ForgotPasswordController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IForgotPassword _forgotPassword;

        public ForgotPasswordController(UserManager<ApplicationUser> userManager, IForgotPassword forgotPassword)
        {
            _userManager = userManager;
            _forgotPassword = forgotPassword;
        }

        [HttpPost]
        [Route("/UserManagement/ForgotPassword/{email}/GenerateOtp")]
        public IActionResult GenerateOtp([FromRoute] string email)
        {
            try
            {
                int randomnumber = RandomNumberGenerator.GetInt32(0, 999999);
                Guid validationToken = Guid.NewGuid();
                var model = new ForgotPasswordModel
                {
                    UserEmail = email,
                    OTP = Convert.ToString(randomnumber),
                    OtpValidationToken = validationToken.ToString()
                };

                //var _count = _forgotPassword.SaveOTPDetail(model);
                var _count = 1;
                if (_count > 0)
                {
                    return Ok(validationToken);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

        }

        [HttpPost]
        [Route("/UserManagement/ForgotPassword/{email}/VerifyOtp")]
        public IActionResult VerifyOtp([FromBody] ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                var _otpdetail = _forgotPassword.GetOTPDetail(forgotPasswordModel.UserEmail);
                if (_otpdetail != null)
                {
                    if (_otpdetail.OtpExpiry >= DateTime.Now
                         && _otpdetail.OTP == forgotPasswordModel.OTP)
                    {
                        return Ok(forgotPasswordModel.UserEmail);
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        [HttpPost]
        [Route("/UserManagement/ForgotPassword/{email}")]
        public async Task<IActionResult> ResetPassword([FromRoute] string email, [FromBody] TempPasswordResetModel input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _userdetails = await _userManager.FindByNameAsync(email);
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


         [Route("/UserManagement/ForgotPassword/SendPassword")]

        [HttpPost]
        public async Task<IActionResult> SendPassword(string Username)
        {
            try
            {
                if (string.IsNullOrEmpty(Username))
                {
                    return BadRequest();
                }
                var loginUser = await _userManager.FindByNameAsync(Username);



                //Check if user exists

                if (loginUser != null)
                {
                    var tempPassword = "";
                    string password = tempPassword.GeneratePassword();
                    string textmessage = string.Format("Hi {0},  Your temporary password is {1} Thanks ", loginUser.fullName, password);

                    var code = await _userManager.GeneratePasswordResetTokenAsync(loginUser);
                    var result = await _userManager.ResetPasswordAsync(loginUser, code, password);
                    if (result.Succeeded)
                    {
                        loginUser.IsTempPassword = true;
                        await _userManager.UpdateAsync(loginUser);
                        SendEmail.SendCredentialEmail(loginUser.fullName, loginUser.Email, loginUser.UserName, password);

                        TempData["Message"] = "Password Sent to your registered email.";

                        return Redirect("~/Identity/Account/Login");
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            ViewData["Message"] = "Password Sent to registered email.";

            return View(nameof(ForgotPassword));
            
        }
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        //public async Task<IActionResult> ForgotPassword(string Username)
        //{
        //    return View();
        //}
    }
    public class ForgotPasswordModel2
    {
        public string Username { get; set; }
    }

}
