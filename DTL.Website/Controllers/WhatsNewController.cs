using DTL.Business.CMS.WhatsNew;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.Website.Controllers
{

    public class WhatsNewController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IWhatsNewService _addWhatsNewService;

        public WhatsNewController(IWhatsNewService addWhatsNewService)
        {
            _addWhatsNewService = addWhatsNewService;
        }
        // GET: WhatsNewController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<WhatsNewModel> WhatsNews = _addWhatsNewService.GetWhatsNewModelByParam(null, isArchieved, isArchieved);
            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", WhatsNews?.OrderByDescending(x => x.CreatedDate));
            }

            return View(WhatsNews?.OrderByDescending(x => x.CreatedDate));
        }

        public ActionResult DownloadFile(Guid id, string lang = "en", bool isView = true)
        {

            IEnumerable<WhatsNewModel> WhatsNews = _addWhatsNewService.GetWhatsNewModelByParam(id, null, null);
            WhatsNewModel WhatsNewModel = WhatsNews.FirstOrDefault();

            if (lang == "en")
            {
                byte[] fileBytes = WhatsNewModel.AttachmentFileInEnglish;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + WhatsNewModel.EnglishFileName);
                return File(fileBytes, WhatsNewModel.EnglishContentType);
            }
            else if (lang == "hn")
            {
                byte[] fileBytes = WhatsNewModel.AttachmentFileInHindi;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + WhatsNewModel.HindiFileName);
                return File(fileBytes, WhatsNewModel.HindiContentType);
            }

            throw new Exception("Specify file type");
        }


    }
}
