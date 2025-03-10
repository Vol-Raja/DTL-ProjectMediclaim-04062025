using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    public class SoController : Controller
    {
        // GET: SoController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CashlessProcessingList()
        {
            return RedirectPermanent("/Mediclaim/Processing/CashlessClaims");
        }
        public ActionResult NonCashlessProcessingList()
        {
            return RedirectPermanent("/Mediclaim/Processing/NonCashlessClaims");
        }
        public ActionResult ProcessingDashbord()
        {
            return View();

        }
        public ActionResult CashLessAppovalReject()
        {
            return View();

        }
        public ActionResult NonCashlessAppovedReject()
        {
            return View();

        }
        // GET: SoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SoController/Create
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

        // GET: SoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SoController/Edit/5
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

        // GET: SoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SoController/Delete/5
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
