using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    public class AsoController : Controller
    {
       
        // GET: AsoController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AsoProcesingDashboard()
        {
            return View();
        }
        public ActionResult NonCashlessAppovedReject()
        {
            return View();
        }
        public ActionResult CashLessAppovalReject()
        {
            return View();
        }

        


        // GET: AsoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AsoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AsoController/Create
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

        // GET: AsoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AsoController/Edit/5
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

        // GET: AsoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AsoController/Delete/5
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
