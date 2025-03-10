using DTL.Business.CMS.Form;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.Website.Controllers
{

    public class FormController : Controller
    {
        private static IFormService _addFormService;

        public FormController(IFormService addFormService)
        {
            _addFormService = addFormService;
        }
        // GET: FormController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<FormModel> Forms = _addFormService.GetFormModelByParam(null, isArchieved);
            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", Forms.OrderByDescending(x => x.CreatedDate));
            }
            return View(Forms.OrderByDescending(x => x.CreatedDate));
        }


        public ActionResult DownloadFile(Guid id, string lang = "English", bool isView = true)
        {

            IEnumerable<FormModel> Forms = _addFormService.GetFormModelByParam(id, null);
            FormModel FormModel = Forms.FirstOrDefault();

            if (lang == "English")
            {
                byte[] fileBytes = FormModel.AttachmentFileInEnglish;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + FormModel.EnglishFileName);
                return File(fileBytes, FormModel.EnglishContentType);
            }
            else if (lang == "Hindi")
            {
                byte[] fileBytes = FormModel.AttachmentFileInHindi;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + FormModel.HindiFileName);
                return File(fileBytes, FormModel.HindiContentType);
            }

            throw new Exception("Specify file type");
        }


    }
}
