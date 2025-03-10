using DTL.Business.CMS.Notice;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.Website.Controllers
{
  
    public class NoticeController : Controller
    {
       
        private static INoticeService _addNoticeService;

        public NoticeController(INoticeService addNoticeService)
        {
            _addNoticeService = addNoticeService;
        }
        // GET: NoticeController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<NoticeModel> Notices = _addNoticeService.GetNoticeModelByParam(null, isArchieved, isArchieved);

            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", Notices?.OrderByDescending(x => x.CreatedDate));
            }

            return View(Notices?.OrderByDescending(x => x.CreatedDate));
        }

   

 
        public ActionResult DownloadFile(Guid id, string lang = "en", bool isView = true)
        {

            IEnumerable<NoticeModel> Notices = _addNoticeService.GetNoticeModelByParam(id, null, null);
            NoticeModel NoticeModel = Notices.FirstOrDefault();

            if (lang == "en")
            {
                byte[] fileBytes = NoticeModel.AttachmentFileInEnglish;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + NoticeModel.EnglishFileName);
                return File(fileBytes, NoticeModel.EnglishContentType);
            }
            else if (lang == "hn")
            {
                byte[] fileBytes = NoticeModel.AttachmentFileInHindi;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + NoticeModel.HindiFileName);
                return File(fileBytes, NoticeModel.HindiContentType);
            }

            throw new Exception("Specify file type");
        }


    }
}
