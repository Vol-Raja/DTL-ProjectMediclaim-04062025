using DTL.Business.CMS.AimVision;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.Website.Controllers
{
    
    public class AimVisionController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IAimVisionService _addAimVisionService;

        public AimVisionController(IAimVisionService addAimVisionService)
        {
            _addAimVisionService = addAimVisionService;
        }
        // GET: AimVisionController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<AimVisionModel> AimVisionModels = _addAimVisionService.GetAimVisionModelByParam(null, isArchieved, isArchieved);


            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", AimVisionModels);
            }
            return View(AimVisionModels);

        }

      
    }
}
