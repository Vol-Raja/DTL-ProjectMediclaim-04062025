using DTL.Business.CMS.About;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.Website.Controllers
{
    
    public class AboutController : Controller
    {
        private static IAboutService _addAboutService;

        public AboutController(IAboutService addAboutService)
        {
            _addAboutService = addAboutService;
        }
        // GET: AboutController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<AboutModel> AboutModels = _addAboutService.GetAboutModelByParam(null, isArchieved, isArchieved);


            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", AboutModels);
            }
            return View(AboutModels);

        }


    }
}
