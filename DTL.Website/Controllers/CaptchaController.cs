using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace DTL.Website.Controllers
{

    [AllowAnonymous]
    public class CaptchaController : Controller
    {
        public IActionResult Index()
        {
           
            var code = GenerateCaptcha();
            TempData["CaptchaCode"] = code;
            return Ok(code);
        }
        public static string GenerateCaptcha()
        {
            string lowers = "abcdefghjkmnpqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNPQRSTUVWXYZ";
            string number = "23456789";


            Random random = new Random();

            string generated = "";
            for (int i = 1; i <= 2; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for (int i = 1; i <= 2; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for (int i = 1; i <= 2; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );



            return new string(generated.ToCharArray().
                OrderBy(s => (random.Next(2) % 2) == 0).ToArray());

        }
    }
}
