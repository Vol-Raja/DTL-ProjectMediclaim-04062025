using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    [Area("GPF")]
    public class Am_DmController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoanProcessingDashboard()
        {
            return View();
        }
        public IActionResult NonRefundableList()
        {
            ViewBag.IsNonRefundable = true;
            return View("RefundableList");
        }
        public IActionResult RefundableList()
        {
            return View();
        }
        public IActionResult RefundableView()
        {
            return View();
        }
        public IActionResult NonRefundableView()
        {
            ViewBag.IsNonRefundable = true;
            return View("RefundableView");
        }
        public IActionResult CurrentGPFBalance()
        {
            return View();
        }
        public IActionResult edlisApproval()
        {
            return View();
        }
        public IActionResult settlementApproval()
        {
            return View();
        }
        public IActionResult edlisApprovalView()
        {
            return View();
        }
        public IActionResult settlementApprovalView()
        {
            return View();
        }
    }
}
