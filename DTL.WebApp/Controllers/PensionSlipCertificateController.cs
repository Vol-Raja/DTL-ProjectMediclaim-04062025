using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{
    public class PensionSlipCertificateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
