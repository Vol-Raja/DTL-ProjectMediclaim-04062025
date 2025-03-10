using DTL.WebApp.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DTL.WebApp.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    [AllowAnonymous]
    public class CaptchaController : Controller
    {
        public IActionResult Index()
        {
            string captcha = "";
            var code = captcha.GenerateCaptcha();
            TempData["CaptchaCode"] = code;
            return Ok(code);
        }
    }
}
