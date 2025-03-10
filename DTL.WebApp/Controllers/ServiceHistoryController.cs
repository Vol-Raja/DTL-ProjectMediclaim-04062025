using DTL.Model.Models;
using DTL.Business.ServiceDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{
    public class ServiceHistoryController : Controller
    {
        private static IServiceDetails _addServiceDetails;

        public ServiceHistoryController(IServiceDetails serviceDetails)
        {
            _addServiceDetails = serviceDetails;
        }
        // GET: ServiceHistoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServiceHistoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServiceHistoryController/Create
        public ActionResult Create()
        {
            return View();
        }

       
        // GET: ServiceHistoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ServiceHistoryController/Edit/5
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

        // GET: ServiceHistoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServiceHistoryController/Delete/5
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
