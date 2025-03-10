using DTL.Business.CMS.Tender;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using DTL.Model.Models;
using Microsoft.AspNetCore.Authorization;

namespace DTL.WebApp.Areas.WebsiteCMS.Controllers
{
    [Area(StringConstants.CMS_AREA)]
    [Authorize(Roles = "SuperAdmin")]
    public class TenderController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static ITenderService _addTenderService;

        public TenderController(ITenderService addTenderService)
        {
            _addTenderService = addTenderService;
        }
        // GET: TenderController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<TenderModel> tenders = _addTenderService.GetTenderModelByParam(null, isArchieved, isArchieved);
            return View(tenders.OrderByDescending(x => x.CreatedDate));
        }

        // GET: TenderController/Details/5
        public ActionResult Details(Guid id)
        {

            IEnumerable<TenderModel> tenders = _addTenderService.GetTenderModelByParam(id, null, null);
            TenderModel tenderModel = tenders.FirstOrDefault();
            tenderModel.ViewOnly = true;
            tenderModel.IsNew = false;
            return View("AddEditTender", tenderModel);
        }

        // GET: TenderController/Create
        public ActionResult Create()
        {
            TenderModel tenderModel = new TenderModel();
            tenderModel.IsNew = true;
            return View("AddEditTender", tenderModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TenderModel tenderModel)
        {
            try
            {
                if (tenderModel.ID == Guid.Empty)
                    tenderModel.CreatedBy = User.Identity.Name;
                else
                    tenderModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {
                            case "AttachmentFileInHindi":
                                tenderModel.AttachmentFileInHindi = ms1.ToArray();
                                tenderModel.AttachmentTitleInHindi = file.FileName;
                                break;
                            case "AttachmentFileInEnglish":
                                tenderModel.AttachmentFileInEnglish = ms1.ToArray();
                                tenderModel.AttachmentTitleInEnglish = file.FileName;
                                break;
                        }
                    }
                else
                {
                    tenderModel.AttachmentFileInHindi = Convert.FromBase64String(tenderModel.AttachmentFileInHindiBase64);
                    tenderModel.AttachmentFileInEnglish = Convert.FromBase64String(tenderModel.AttachmentFileInEnglishBase64);
                }
                string result = _addTenderService.AddEditTenderData(tenderModel, tenderModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("Tender Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }
        // GET: TenderController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<TenderModel> tenders = _addTenderService.GetTenderModelByParam(id, false, false);
            TenderModel tenderModel = tenders.FirstOrDefault();
            tenderModel.ViewOnly = tenderModel.IsArchieved;
            tenderModel.IsNew = false;
            return View("AddEditTender", tenderModel);
        }


        // GET: TenderController/Delete/5
        [HttpPost]
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<TenderModel> tenders = _addTenderService.GetTenderModelByParam(id, false, false);
            TenderModel tenderModel = tenders.FirstOrDefault();
            tenderModel.IsArchieved = true;
            tenderModel.IsDeleted = true;
            _addTenderService.AddEditTenderData(tenderModel, true);
            //return RedirectToAction(nameof(Index));
            return Json("success");
        }
        public ActionResult DownloadFile(Guid Id, string lang = "en")
        {

            if (Guid.Empty == Id) return BadRequest();

            IEnumerable<TenderModel> tenders = _addTenderService.GetTenderModelByParam(Id, false, false);
            TenderModel tenderModel = tenders.FirstOrDefault();
            byte[] fileBytes = null;
            if (lang == "en")
            {
                fileBytes = tenderModel.AttachmentFileInEnglish;
            }
            else
            {
                fileBytes = tenderModel.AttachmentFileInHindi;
            }

            return File(fileBytes, "application/pdf");



            throw new Exception("Specify file type");
        }

    }
}
