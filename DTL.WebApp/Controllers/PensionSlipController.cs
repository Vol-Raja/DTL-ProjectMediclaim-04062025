using DTL.Model.Models;
using DTL.Business.PensionSlip;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{
    public class PensionSlipController : Controller
    {
        private readonly IPensionSlipService _pensionSlipService; 
        public PensionSlipController(IPensionSlipService pensionSlipService)
        {
            _pensionSlipService = pensionSlipService;
        }
        // GET: PensionSlipController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PensionSlipController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PensionSlipController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PensionSlipController/Create
        
        // GET: PensionSlipController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PensionSlipController/Edit/5
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

        // GET: PensionSlipController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PensionSlipController/Delete/5
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
