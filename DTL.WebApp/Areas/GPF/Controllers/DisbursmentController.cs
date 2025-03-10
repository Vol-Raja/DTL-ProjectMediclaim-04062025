using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    [Area("GPF")]
    public class DisbursmentController : Controller
    {
        
        // GET: DisbursmentController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoanApplicationDashboard()
        {
            return View();
        }
        public ActionResult AddLoanRefundable()
        {
            return View();

        }
        public ActionResult RefundableArchive()
        {
            return View();
        }
        public ActionResult RefundableView()
        {
            return View();
        }
        public ActionResult RefundableList()
        {
            return View();
        }
        public IActionResult NonRefundableView()
        {
            ViewBag.IsNonRefundable = true;
            return View("RefundableView");
        }
        public ActionResult NonRefundableList()
        {
            ViewBag.IsNonRefundable = true;
            return View("RefundableList");
        }
        public IActionResult NonRefundableVoucherList()
        {
            return View();
        }    
        public IActionResult RefundableVoucherList()
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
        public IActionResult AddNewVoucher()
        {
            return View();
        }
        public IActionResult RefundableVoucherListView()
        {
            return View();
        } 
        
        public IActionResult NonRefundableVoucherListView()
        {
            return View();
        }
        public IActionResult VoucherApplicationDashboard()
        {
            return View();
        }
        public IActionResult AddNonCashlessNewVoucher()
        {
            return View();
        }
        // GET: DisbursmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DisbursmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DisbursmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DisbursmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DisbursmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DisbursmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DisbursmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
