using DTL.Business.CMS.RuleActHelpline;
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
    public class RuleActHelplineController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
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
                return View("HelplineList", RuleActHelplines.OrderByDescending(x => x.CreatedDate));
            }
            else
                return View(RuleActHelplines.OrderByDescending(x => x.CreatedDate));
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        // GET: RuleActHelplineController/Details/5
        public ActionResult Details(Guid id)
        {

            IEnumerable<RuleActHelplineModel> RuleActHelplines = _addRuleActHelplineService.GetRuleActHelplineModelByParam(id, null, null);
            RuleActHelplineModel RuleActHelplineModel = RuleActHelplines.FirstOrDefault();
            RuleActHelplineModel.ViewOnly = true;
            RuleActHelplineModel.IsNew = false;
            return View("AddEditRuleActHelpline", RuleActHelplineModel);
        }

        // GET: RuleActHelplineController/Create
        public ActionResult Create(bool IsHelpline = false)
        {
            RuleActHelplineModel RuleActHelplineModel = new();
            RuleActHelplineModel.IsNew = true;
            RuleActHelplineModel.IsHelpline = IsHelpline;
            if (IsHelpline)
                return View("AddEditHelpline", RuleActHelplineModel);
            else
                return View("AddEditRuleAct", RuleActHelplineModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(RuleActHelplineModel RuleActHelplineModel)
        {
            try
            {
                if (RuleActHelplineModel.ID == Guid.Empty)
                    RuleActHelplineModel.CreatedBy = User.Identity.Name;
                else
                    RuleActHelplineModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {
                            case "AttachmentFileInHindi":
                                RuleActHelplineModel.AttachmentFileInHindi = ms1.ToArray();
                                RuleActHelplineModel.HindiContentType = file.ContentType;
                                RuleActHelplineModel.HindiFileName = file.FileName;
                                break;
                            case "AttachmentFileInEnglish":
                                RuleActHelplineModel.AttachmentFileInEnglish = ms1.ToArray();
                                RuleActHelplineModel.EnglishFileName = file.FileName;
                                RuleActHelplineModel.EnglishContentType = file.ContentType;
                                break;

                        }
                    }


                if (RuleActHelplineModel.ID != Guid.Empty)
                {
                    IEnumerable<RuleActHelplineModel> models = _addRuleActHelplineService.GetRuleActHelplineModelByParam(RuleActHelplineModel.ID, null, null);
                    RuleActHelplineModel oldModel = models.FirstOrDefault();
                    if (RuleActHelplineModel.AttachmentFileInHindi == null)
                    {
                        RuleActHelplineModel.HindiFileName = oldModel.HindiFileName;
                        RuleActHelplineModel.HindiContentType = oldModel.HindiContentType;
                        RuleActHelplineModel.AttachmentFileInHindi = oldModel.AttachmentFileInHindi;
                    }
                    if (RuleActHelplineModel.AttachmentFileInEnglish == null)
                    {
                        RuleActHelplineModel.EnglishFileName = oldModel.EnglishFileName;
                        RuleActHelplineModel.EnglishContentType = oldModel.EnglishContentType;
                        RuleActHelplineModel.AttachmentFileInEnglish = oldModel.AttachmentFileInEnglish;
                    }


                }



                string result = _addRuleActHelplineService.AddEditRuleActHelplineData(RuleActHelplineModel, RuleActHelplineModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("RuleActHelpline Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }
        // GET: RuleActHelplineController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<RuleActHelplineModel> RuleActHelplines = _addRuleActHelplineService.GetRuleActHelplineModelByParam(id, false, null);
            RuleActHelplineModel RuleActHelplineModel = RuleActHelplines.FirstOrDefault();
            RuleActHelplineModel.ViewOnly = RuleActHelplineModel.IsDeleted;
            RuleActHelplineModel.IsNew = false;

            if (RuleActHelplineModel.IsHelpline)
                return View("AddEditHelpline", RuleActHelplineModel);
            else
                return View("AddEditRuleAct", RuleActHelplineModel);

        }


        // GET: RuleActHelplineController/Delete/5
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<RuleActHelplineModel> RuleActHelplines = _addRuleActHelplineService.GetRuleActHelplineModelByParam(id, false, null);
            RuleActHelplineModel RuleActHelplineModel = RuleActHelplines.FirstOrDefault();

            RuleActHelplineModel.IsDeleted = true;
            _addRuleActHelplineService.AddEditRuleActHelplineData(RuleActHelplineModel, true);
            return RedirectToAction(nameof(Index));
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
