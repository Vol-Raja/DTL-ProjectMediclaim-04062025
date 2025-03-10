using DTL.API.Models;
using DTL.Business.UserManagement;
using DTL.Model.Models.UserManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DTL.API.Controllers.Account
{
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IForgotPassword _forgotPassword;
        //private readonly ILogger<LoginModel> _logger;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IForgotPassword forgotPassword)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _forgotPassword = forgotPassword;
        }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        [HttpPost]
        [Route("account/login")]
        public async Task<IActionResult> OnPostAsync([FromBody]InputModel _input)
        {   
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(_input.Email, _input.Password, _input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var _userdetail = await _userManager.FindByEmailAsync(_input.Email);

                    var _userResponse = new
                    {
                        id = _userdetail.Id,
                        fullName = _userdetail.fullName,
                        email = _userdetail.Email,
                        phoneNumber = _userdetail.PhoneNumber
                    };

                    //_logger.LogInformation("User logged in.");
                    return Ok(_userResponse);
                }
                if (result.IsLockedOut)
                {
                    //_logger.LogWarning("User account locked out.");
                    return NotFound("User account locked out.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest("Invalid login attempt.");
                }
            }

            // If we got this far, something failed, redisplay form
            return Ok();
        }

        [HttpPost]
        [Route("account/forgotPassword/{email}/generateOtp")]
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

                var _count = _forgotPassword.SaveOTPDetail(model);

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
        [Route("account/forgotPassword/{email}/verifyOtp")]
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
        [Route("account/resetPassword/{email}")]
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
    }
}

