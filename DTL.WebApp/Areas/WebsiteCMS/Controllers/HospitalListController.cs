using DTL.Business.CMS.BannerImage;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.WebApp.Areas.WebsiteCMS.Controllers
{
    [Area(StringConstants.CMS_AREA)]
    [Authorize(Roles = "SuperAdmin")]
    public class HospitalListController : Controller
    {
        // GET: HospitalListController
        public ActionResult Index()
        {
            return View();
        }   
        public ActionResult AddEditHospital()
        {
            return View();
        }

        // GET: HospitalListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HospitalListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HospitalListController/Create
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

        // GET: HospitalListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HospitalListController/Edit/5
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

        // GET: HospitalListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HospitalListController/Delete/5
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
