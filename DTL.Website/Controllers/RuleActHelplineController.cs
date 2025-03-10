using DTL.Business.CMS.RuleActHelpline;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.Website.Controllers
{

    public class RuleActHelplineController : Controller
    {

        private static IRuleActHelplineService _addRuleActHelplineService;

        public RuleActHelplineController(IRuleActHelplineService addRuleActHelplineService)
        {
            _addRuleActHelplineService = addRuleActHelplineService;
        }
        // GET: RuleActHelplineController
        public ActionResult Index(bool isHelpline = false)
        {
            IEnumerable<RuleActHelplineModel> RuleActHelplines = _addRuleActHelplineService.GetRuleActHelplineModelByParam(null, false, isHelpline);
            if (isHelpline)
            {
                string lang = Request.Cookies["lang"];
                if (lang?.ToLower() == "hn")
                {
                    return View("hn/" + "HelplineList", RuleActHelplines?.OrderByDescending(x => x.CreatedDate));
                }
                return View("HelplineList", RuleActHelplines?.OrderByDescending(x => x.CreatedDate));
            }
            else
            {
                string lang = Request.Cookies["lang"];
                if (lang?.ToLower() == "hn")
                {
                    return View("hn/" + "Index", RuleActHelplines?.OrderByDescending(x => x.CreatedDate));
                }
                return View(RuleActHelplines?.OrderByDescending(x => x.CreatedDate));
            }
              
        }


        public ActionResult DownloadFile(Guid id, string lang = "English", bool isView = true)
        {

            IEnumerable<RuleActHelplineModel> RuleActHelplines = _addRuleActHelplineService.GetRuleActHelplineModelByParam(id, null, null);
            RuleActHelplineModel RuleActHelplineModel = RuleActHelplines.FirstOrDefault();

            if (lang == "English")
            {
                byte[] fileBytes = RuleActHelplineModel.AttachmentFileInEnglish;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + RuleActHelplineModel.EnglishFileName);
                return File(fileBytes, RuleActHelplineModel.EnglishContentType);
            }
            else if (lang == "Hindi")
            {
                byte[] fileBytes = RuleActHelplineModel.AttachmentFileInHindi;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + RuleActHelplineModel.HindiFileName);
                return File(fileBytes, RuleActHelplineModel.HindiContentType);
            }

            throw new Exception("Specify file type");
        }


    }
}
