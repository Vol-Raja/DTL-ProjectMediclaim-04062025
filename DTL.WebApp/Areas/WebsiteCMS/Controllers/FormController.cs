using DTL.Business.CMS.Form;
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
    public class FormController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IFormService _addFormService;

        public FormController(IFormService addFormService)
        {
            _addFormService = addFormService;
        }
        // GET: FormController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<FormModel> Forms = _addFormService.GetFormModelByParam(null, isArchieved);
            return View(Forms.OrderByDescending(x => x.CreatedDate));
        }

        // GET: FormController/Details/5
        public ActionResult Details(Guid id)
        {

            IEnumerable<FormModel> Forms = _addFormService.GetFormModelByParam(id, null);
            FormModel FormModel = Forms.FirstOrDefault();
            FormModel.ViewOnly = true;
            FormModel.IsNew = false;
            return View("AddEditForm", FormModel);
        }

        // GET: FormController/Create
        public ActionResult Create()
        {
            FormModel FormModel = new();
            FormModel.IsNew = true;
            return View("AddEditForm", FormModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FormModel FormModel)
        {
            try
            {
                if (FormModel.ID == Guid.Empty)
                    FormModel.CreatedBy = User.Identity.Name;
                else
                    FormModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {
                            case "AttachmentFileInHindi":
                                FormModel.AttachmentFileInHindi = ms1.ToArray();
                                FormModel.HindiContentType = file.ContentType;
                                FormModel.HindiFileName = file.FileName;
                                break;
                            case "AttachmentFileInEnglish":
                                FormModel.AttachmentFileInEnglish = ms1.ToArray();
                                FormModel.EnglishFileName = file.FileName;
                                FormModel.EnglishContentType = file.ContentType;
                                break;
             
                               
                        }
                    }


                if (FormModel.ID != Guid.Empty)
                {
                    IEnumerable<FormModel> models = _addFormService.GetFormModelByParam(FormModel.ID, null);
                    FormModel oldModel = models.FirstOrDefault();
                    if (FormModel.AttachmentFileInHindi == null)
                    {
                        FormModel.HindiFileName = oldModel.HindiFileName;
                        FormModel.HindiContentType = oldModel.HindiContentType;
                        FormModel.AttachmentFileInHindi = oldModel.AttachmentFileInHindi;
                    }
                    if (FormModel.AttachmentFileInEnglish == null)
                    {
                        FormModel.EnglishFileName = oldModel.EnglishFileName;
                        FormModel.EnglishContentType = oldModel.EnglishContentType;
                        FormModel.AttachmentFileInEnglish = oldModel.AttachmentFileInEnglish;
                    }
              

                }

               

                string result = _addFormService.AddEditFormData(FormModel, FormModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("Form Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }
        // GET: FormController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<FormModel> Forms = _addFormService.GetFormModelByParam(id, false);
            FormModel FormModel = Forms.FirstOrDefault();
            FormModel.ViewOnly = FormModel.IsDeleted;
            FormModel.IsNew = false;
            return View("AddEditForm", FormModel);
        }


        // GET: FormController/Delete/5
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<FormModel> Forms = _addFormService.GetFormModelByParam(id, false);
            FormModel FormModel = Forms.FirstOrDefault();
           
            FormModel.IsDeleted = true;
            _addFormService.AddEditFormData(FormModel, true);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DownloadFile(Guid id, string lang, bool isView = true)
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
