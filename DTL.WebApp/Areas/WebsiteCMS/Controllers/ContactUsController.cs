using DTL.Business.CMS.ContactUs;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Authorization;
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
    public class ContactUsController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IContactUsService _addContactUsService;

        public ContactUsController(IContactUsService addContactUsService)
        {
            _addContactUsService = addContactUsService;
        }
        // GET: ContactUsController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<ContactUsModel> ContactUss = _addContactUsService.GetContactUsModelByParam(null, isArchieved);
            return View(ContactUss?.OrderByDescending(x => x.CreatedDate));
        }

        // GET: ContactUsController/Details/5
        public ActionResult Details(Guid id)
        {

            IEnumerable<ContactUsModel> ContactUss = _addContactUsService.GetContactUsModelByParam(id, null);
            ContactUsModel ContactUsModel = ContactUss.FirstOrDefault();
            ContactUsModel.ViewOnly = true;
            ContactUsModel.IsNew = false;
            return View("AddEditContactUs", ContactUsModel);
        }

        // GET: ContactUsController/Create
        public ActionResult Create()
        {
            ContactUsModel ContactUsModel = new();
            ContactUsModel.IsNew = true;
            return View("AddEditContactUs", ContactUsModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ContactUsModel ContactUsModel)
        {
            try
            {
                if (ContactUsModel.ID == Guid.Empty)
                    ContactUsModel.CreatedBy = User.Identity.Name;
                else
                    ContactUsModel.ModifideBy = User.Identity.Name;


                string result = _addContactUsService.AddEditContactUsData(ContactUsModel, ContactUsModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("ContactUs Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }
        // GET: ContactUsController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<ContactUsModel> ContactUss = _addContactUsService.GetContactUsModelByParam(id, false);
            ContactUsModel ContactUsModel = ContactUss.FirstOrDefault();
            ContactUsModel.ViewOnly = ContactUsModel.IsDeleted;
            ContactUsModel.IsNew = false;
            return View("AddEditContactUs", ContactUsModel);
        }


        // GET: ContactUsController/Delete/5
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<ContactUsModel> ContactUss = _addContactUsService.GetContactUsModelByParam(id, false);
            ContactUsModel ContactUsModel = ContactUss.FirstOrDefault();

            ContactUsModel.IsDeleted = true;
            _addContactUsService.AddEditContactUsData(ContactUsModel, true);
            return RedirectToAction(nameof(Index));
        }




    }
}
