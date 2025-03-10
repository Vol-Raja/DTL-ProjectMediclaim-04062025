using DTL.Business.CMS.Notice;
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
    public class NoticeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static INoticeService _addNoticeService;

        public NoticeController(INoticeService addNoticeService)
        {
            _addNoticeService = addNoticeService;
        }
        // GET: NoticeController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<NoticeModel> notices = _addNoticeService.GetNoticeModelByParam(null, isArchieved, isArchieved);
            return View(notices.OrderByDescending(x => x.CreatedDate));
        }

        // GET: NoticeController/Details/5
        public ActionResult Details(Guid id)
        {

            IEnumerable<NoticeModel> notices = _addNoticeService.GetNoticeModelByParam(id, null, null);
            NoticeModel noticeModel = notices.FirstOrDefault();
            noticeModel.ViewOnly = true;
            noticeModel.IsNew = false;
            return View("AddEditNotice", noticeModel);
        }

        // GET: NoticeController/Create
        public ActionResult Create()
        {
            NoticeModel noticeModel = new();
            noticeModel.IsNew = true;
            return View("AddEditNotice", noticeModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(NoticeModel noticeModel)
        {
            try
            {
                if (noticeModel.ID == Guid.Empty)
                    noticeModel.CreatedBy = User.Identity.Name;
                else
                    noticeModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                {
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {
                            case "AttachmentFileInHindi":
                                noticeModel.AttachmentFileInHindi = ms1.ToArray();
                                noticeModel.HindiContentType = file.ContentType;
                                noticeModel.HindiFileName = file.FileName;
                                break;
                            case "AttachmentFileInEnglish":
                                noticeModel.AttachmentFileInEnglish = ms1.ToArray();
                                noticeModel.EnglishFileName = file.FileName;
                                noticeModel.EnglishContentType = file.ContentType;
                                break;
                        }
                    }
                }
                else
                {
                    if (noticeModel.ID != Guid.Empty)
                    {
                        IEnumerable<NoticeModel> notices = _addNoticeService.GetNoticeModelByParam(noticeModel.ID, null, null);
                        NoticeModel oldModel = notices.FirstOrDefault();

                        noticeModel.AttachmentFileInHindi = oldModel.AttachmentFileInHindi;
                        noticeModel.AttachmentFileInEnglish = oldModel.AttachmentFileInEnglish;
                        noticeModel.HindiFileName = oldModel.HindiFileName;
                        noticeModel.HindiContentType = oldModel.HindiContentType;
                        noticeModel.EnglishContentType = oldModel.EnglishContentType;
                        noticeModel.EnglishFileName = oldModel.EnglishFileName;
                    }
                }

                noticeModel.NoticeDateHindi = noticeModel.NoticeDate;

                string result = _addNoticeService.AddEditNoticeData(noticeModel, noticeModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("Notice Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }
        // GET: NoticeController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<NoticeModel> notices = _addNoticeService.GetNoticeModelByParam(id, false, false);
            NoticeModel noticeModel = notices.FirstOrDefault();
            noticeModel.ViewOnly = noticeModel.IsArchieved;
            noticeModel.IsNew = false;
            return View("AddEditNotice", noticeModel);
        }


        // GET: NoticeController/Delete/5
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<NoticeModel> notices = _addNoticeService.GetNoticeModelByParam(id, false, false);
            NoticeModel noticeModel = notices.FirstOrDefault();
            noticeModel.IsArchieved = true;
            noticeModel.IsDeleted = true;
            _addNoticeService.AddEditNoticeData(noticeModel, true);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DownloadFile(Guid id, string lang, bool isView = true)
        {

            IEnumerable<NoticeModel> notices = _addNoticeService.GetNoticeModelByParam(id, null, null);
            NoticeModel noticeModel = notices.FirstOrDefault();

            if (lang == "English")
            {
                byte[] fileBytes = noticeModel.AttachmentFileInEnglish;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + noticeModel.EnglishFileName);
                return File(fileBytes, noticeModel.EnglishContentType, noticeModel.EnglishFileName);
            }
            else if (lang == "Hindi")
            {
                byte[] fileBytes = noticeModel.AttachmentFileInHindi;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + noticeModel.HindiFileName);
                return File(fileBytes, noticeModel.HindiContentType, noticeModel.HindiFileName);
            }

            throw new Exception("Specify file type");
        }


    }
}
