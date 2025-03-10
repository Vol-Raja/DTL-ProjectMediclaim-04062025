using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    public class IPDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult ProcessingList()
        //{   
        //    return View();
        //}

        public IActionResult DealingView()
        {
            return View();
        }
        public IActionResult VoucherForm()
        {
            return View();
        } 
    }
}
