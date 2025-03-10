using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class PpoCardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NonCashlessPpoCard()
        {
            return View();
        }
    }
}
