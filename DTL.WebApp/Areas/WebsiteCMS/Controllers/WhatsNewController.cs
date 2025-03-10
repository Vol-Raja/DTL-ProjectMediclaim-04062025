using DTL.Business.CMS.WhatsNew;
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
            return View(WhatsNews.OrderByDescending(x => x.CreatedDate));
        }

        // GET: WhatsNewController/Details/5
        public ActionResult Details(Guid id)
        {

            IEnumerable<WhatsNewModel> WhatsNews = _addWhatsNewService.GetWhatsNewModelByParam(id, null, null);
            WhatsNewModel WhatsNewModel = WhatsNews.FirstOrDefault();
            WhatsNewModel.ViewOnly = true;
            WhatsNewModel.IsNew = false;
            return View("AddEditWhatsNew", WhatsNewModel);
        }

        // GET: WhatsNewController/Create
        public ActionResult Create()
        {
            WhatsNewModel WhatsNewModel = new();
            WhatsNewModel.IsNew = true;
            return View("AddEditWhatsNew", WhatsNewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(WhatsNewModel WhatsNewModel)
        {
            try
            {
                if (WhatsNewModel.ID == Guid.Empty)
                    WhatsNewModel.CreatedBy = User.Identity.Name;
                else
                    WhatsNewModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {
                            case "AttachmentFileInHindi":
                                WhatsNewModel.AttachmentFileInHindi = ms1.ToArray();
                                WhatsNewModel.HindiContentType = file.ContentType;
                                WhatsNewModel.HindiFileName = file.FileName;
                                break;
                            case "AttachmentFileInEnglish":
                                WhatsNewModel.AttachmentFileInEnglish = ms1.ToArray();
                                WhatsNewModel.EnglishFileName = file.FileName;
                                WhatsNewModel.EnglishContentType = file.ContentType;
                                break;
                            case "Image":
                                WhatsNewModel.Image = ms1.ToArray();
                                WhatsNewModel.ImageFileName = file.FileName;
                                WhatsNewModel.ImageContentType = file.ContentType;
                                break;
                        }
                    }


                if (WhatsNewModel.ID != Guid.Empty)
                {
                    IEnumerable<WhatsNewModel> models = _addWhatsNewService.GetWhatsNewModelByParam(WhatsNewModel.ID, null, null);
                    WhatsNewModel oldModel = models.FirstOrDefault();
                    if (WhatsNewModel.AttachmentFileInHindi == null)
                    {
                        WhatsNewModel.HindiFileName = oldModel.HindiFileName;
                        WhatsNewModel.HindiContentType = oldModel.HindiContentType;
                        WhatsNewModel.AttachmentFileInHindi = oldModel.AttachmentFileInHindi;
                    }
                    if (WhatsNewModel.AttachmentFileInEnglish == null)
                    {
                        WhatsNewModel.EnglishFileName = oldModel.EnglishFileName;
                        WhatsNewModel.EnglishContentType = oldModel.EnglishContentType;
                        WhatsNewModel.AttachmentFileInEnglish = oldModel.AttachmentFileInEnglish;
                    }
                    if (WhatsNewModel.Image == null)
                    {
                        WhatsNewModel.ImageFileName = oldModel.ImageFileName;
                        WhatsNewModel.ImageContentType = oldModel.ImageContentType;
                        WhatsNewModel.Image = oldModel.Image;
                    }

                }

                WhatsNewModel.WhatsNewDateHindi = WhatsNewModel.WhatsNewDate;

                string result = _addWhatsNewService.AddEditWhatsNewData(WhatsNewModel, WhatsNewModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("WhatsNew Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }
        // GET: WhatsNewController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<WhatsNewModel> WhatsNews = _addWhatsNewService.GetWhatsNewModelByParam(id, false, false);
            WhatsNewModel WhatsNewModel = WhatsNews.FirstOrDefault();
            WhatsNewModel.ViewOnly = WhatsNewModel.IsArchieved;
            WhatsNewModel.IsNew = false;
            return View("AddEditWhatsNew", WhatsNewModel);
        }


        // GET: WhatsNewController/Delete/5
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<WhatsNewModel> WhatsNews = _addWhatsNewService.GetWhatsNewModelByParam(id, false, false);
            WhatsNewModel WhatsNewModel = WhatsNews.FirstOrDefault();
            WhatsNewModel.IsArchieved = true;
            WhatsNewModel.IsDeleted = true;
            _addWhatsNewService.AddEditWhatsNewData(WhatsNewModel, true);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DownloadFile(Guid id, string lang, bool isView = true)
        {

            IEnumerable<WhatsNewModel> WhatsNews = _addWhatsNewService.GetWhatsNewModelByParam(id, null, null);
            WhatsNewModel WhatsNewModel = WhatsNews.FirstOrDefault();

            if (lang == "English")
            {
                byte[] fileBytes = WhatsNewModel.AttachmentFileInEnglish;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + WhatsNewModel.EnglishFileName);
                return File(fileBytes, WhatsNewModel.EnglishContentType);
            }
            else if (lang == "Hindi")
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
