using DTL.Business.CMS.ContactUs;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.Website.Controllers
{
  
    public class ContactUsController : Controller
    {
        
        private static IContactUsService _addContactUsService;

        public ContactUsController(IContactUsService addContactUsService)
        {
            _addContactUsService = addContactUsService;
        }
        // GET: ContactUsController
        public ActionResult Index(bool isArchieved = false)
        {
          
           IEnumerable<ContactUsModel> ContactUss = _addContactUsService.GetContactUsModelByParam(null, isArchieved);
            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", ContactUss);
            }
            return View(ContactUss);
        }

    }
}
