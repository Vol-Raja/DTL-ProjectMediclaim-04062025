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
using static System.Net.WebRequestMethods;

namespace DTL.Website.Controllers
{

    public class TrusteeController : Controller
    {

        private static IAboutService _addAboutService;

        public TrusteeController(IAboutService addAboutService)
        {
            _addAboutService = addAboutService;
        }
        // GET: AboutController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<TrusteeModel> TrusteeModels = _addAboutService.GetTrusteeModelByParam(null, isArchieved);
            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", TrusteeModels);
            }
            return View(TrusteeModels);

        }

    }
}
