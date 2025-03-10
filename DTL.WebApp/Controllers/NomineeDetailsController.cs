using DTL.Model.Models;
using DTL.Business.NomineeDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{
    public class NomineeDetailsController : Controller
    {
        private static INomineeDetails _iNomineeDetails;
        public NomineeDetailsController(INomineeDetails nomineeDetails)
        {
            _iNomineeDetails = nomineeDetails;
        }
        // GET: FamilyDetailsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FamilyDetailsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FamilyDetailsController/Create
        public ActionResult Create()
        {
            return View();
        }

       

        // GET: FamilyDetailsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FamilyDetailsController/Edit/5
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

        // GET: FamilyDetailsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FamilyDetailsController/Delete/5
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
