using DTL.Business.CMS.Event;
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
    public class EventController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IEventService _addEventService;

        public EventController(IEventService addEventService)
        {
            _addEventService = addEventService;
        }
        // GET: EventController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<EventModel> Events = _addEventService.GetEventModelByParam(null, isArchieved);
            return View(Events.OrderByDescending(x => x.CreatedDate));
        }

        // GET: EventController/Details/5
        public ActionResult Details(Guid id)
        {

            IEnumerable<EventModel> Events = _addEventService.GetEventModelByParam(id, null);
            EventModel EventModel = Events.FirstOrDefault();
            EventModel.ViewOnly = true;
            EventModel.IsNew = false;
            return View("AddEditEvent", EventModel);
        }

        // GET: EventController/Create
        public ActionResult Create()
        {
            EventModel EventModel = new();
            EventModel.IsNew = true;
            return View("AddEditEvent", EventModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EventModel EventModel)
        {
            try
            {
                if (EventModel.ID == Guid.Empty)
                    EventModel.CreatedBy = User.Identity.Name;
                else
                    EventModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {
                            case "AttachmentFileInHindi":
                                EventModel.AttachmentFileInHindi = ms1.ToArray();
                                EventModel.HindiContentType = file.ContentType;
                                EventModel.HindiFileName = file.FileName;
                                break;
                            case "AttachmentFileInEnglish":
                                EventModel.AttachmentFileInEnglish = ms1.ToArray();
                                EventModel.EnglishFileName = file.FileName;
                                EventModel.EnglishContentType = file.ContentType;
                                break;
             
                                break;
                        }
                    }


                if (EventModel.ID != Guid.Empty)
                {
                    IEnumerable<EventModel> models = _addEventService.GetEventModelByParam(EventModel.ID, null);
                    EventModel oldModel = models.FirstOrDefault();
                    if (EventModel.AttachmentFileInHindi == null)
                    {
                        EventModel.HindiFileName = oldModel.HindiFileName;
                        EventModel.HindiContentType = oldModel.HindiContentType;
                        EventModel.AttachmentFileInHindi = oldModel.AttachmentFileInHindi;
                    }
                    if (EventModel.AttachmentFileInEnglish == null)
                    {
                        EventModel.EnglishFileName = oldModel.EnglishFileName;
                        EventModel.EnglishContentType = oldModel.EnglishContentType;
                        EventModel.AttachmentFileInEnglish = oldModel.AttachmentFileInEnglish;
                    }
              

                }

               

                string result = _addEventService.AddEditEventData(EventModel, EventModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("Event Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }
        // GET: EventController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<EventModel> Events = _addEventService.GetEventModelByParam(id, false);
            EventModel EventModel = Events.FirstOrDefault();
            EventModel.ViewOnly = EventModel.IsDeleted;
            EventModel.IsNew = false;
            return View("AddEditEvent", EventModel);
        }


        // GET: EventController/Delete/5
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<EventModel> Events = _addEventService.GetEventModelByParam(id, false);
            EventModel EventModel = Events.FirstOrDefault();
           
            EventModel.IsDeleted = true;
            _addEventService.AddEditEventData(EventModel, true);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DownloadFile(Guid id, string lang, bool isView = true)
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
