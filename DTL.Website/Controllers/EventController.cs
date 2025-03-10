using DTL.Business.CMS.Event;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.Website.Controllers
{
    
    public class EventController : Controller
    {
      
        private static IEventService _addEventService;

        public EventController(IEventService addEventService)
        {
            _addEventService = addEventService;
        }
        // GET: EventController
        public ActionResult Index(bool isArchieved = false){
            IEnumerable<EventModel> Events = _addEventService.GetEventModelByParam(null, isArchieved);
            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", Events?.OrderByDescending(x => x.CreatedDate));
            }
            return View(Events?.OrderByDescending(x => x.CreatedDate));
        }

      
        public ActionResult DownloadFile(Guid id, string lang= "English", bool isView = true)
        {

            IEnumerable<EventModel> Events = _addEventService.GetEventModelByParam(id, null);
            EventModel EventModel = Events.FirstOrDefault();

            if (lang == "English")
            {
                byte[] fileBytes = EventModel.AttachmentFileInEnglish;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + EventModel.EnglishFileName);
                return File(fileBytes, EventModel.EnglishContentType);
            }
            else if (lang == "Hindi")
            {
                byte[] fileBytes = EventModel.AttachmentFileInHindi;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + EventModel.HindiFileName);
                return File(fileBytes, EventModel.HindiContentType);
            }

            throw new Exception("Specify file type");
        }


    }
}
