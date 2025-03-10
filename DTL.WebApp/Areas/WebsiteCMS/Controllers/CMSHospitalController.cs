using DTL.Business.CMS.CMSHospital;
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
    public class CMSHospitalController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static ICMSHospitalService _addCMSHospitalService;

        public CMSHospitalController(ICMSHospitalService addCMSHospitalService)
        {
            _addCMSHospitalService = addCMSHospitalService;
        }
        // GET: CMSHospitalController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<CMSHospitalModel> CMSHospitals = _addCMSHospitalService.GetCMSHospitalModelByParam(null, isArchieved);
            return View(CMSHospitals.OrderByDescending(x => x.CreatedDate));
        }

        // GET: CMSHospitalController/Details/5
        public ActionResult Details(Guid id)
        {

            IEnumerable<CMSHospitalModel> CMSHospitals = _addCMSHospitalService.GetCMSHospitalModelByParam(id, null);
            CMSHospitalModel CMSHospitalModel = CMSHospitals.FirstOrDefault();
            CMSHospitalModel.ViewOnly = true;
            CMSHospitalModel.IsNew = false;
            return View("AddEditCMSHospital", CMSHospitalModel);
        }

        // GET: CMSHospitalController/Create
        public ActionResult Create()
        {
            CMSHospitalModel CMSHospitalModel = new();
            CMSHospitalModel.IsNew = true;
            return View("AddEditCMSHospital", CMSHospitalModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CMSHospitalModel CMSHospitalModel)
        {
            try
            {
                if (CMSHospitalModel.ID == Guid.Empty)
                    CMSHospitalModel.CreatedBy = User.Identity.Name;
                else
                    CMSHospitalModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {
                            case "AttachmentFileInHindi":
                                CMSHospitalModel.AttachmentFileInHindi = ms1.ToArray();
                                CMSHospitalModel.HindiContentType = file.ContentType;
                                CMSHospitalModel.HindiFileName = file.FileName;
                                break;
                            case "AttachmentFileInEnglish":
                                CMSHospitalModel.AttachmentFileInEnglish = ms1.ToArray();
                                CMSHospitalModel.EnglishFileName = file.FileName;
                                CMSHospitalModel.EnglishContentType = file.ContentType;
                                break;
             
                               
                        }
                    }


                if (CMSHospitalModel.ID != Guid.Empty)
                {
                    IEnumerable<CMSHospitalModel> models = _addCMSHospitalService.GetCMSHospitalModelByParam(CMSHospitalModel.ID, null);
                    CMSHospitalModel oldModel = models.FirstOrDefault();
                    if (CMSHospitalModel.AttachmentFileInHindi == null)
                    {
                        CMSHospitalModel.HindiFileName = oldModel.HindiFileName;
                        CMSHospitalModel.HindiContentType = oldModel.HindiContentType;
                        CMSHospitalModel.AttachmentFileInHindi = oldModel.AttachmentFileInHindi;
                    }
                    if (CMSHospitalModel.AttachmentFileInEnglish == null)
                    {
                        CMSHospitalModel.EnglishFileName = oldModel.EnglishFileName;
                        CMSHospitalModel.EnglishContentType = oldModel.EnglishContentType;
                        CMSHospitalModel.AttachmentFileInEnglish = oldModel.AttachmentFileInEnglish;
                    }
              

                }

               

                string result = _addCMSHospitalService.AddEditCMSHospitalData(CMSHospitalModel, CMSHospitalModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("CMSHospital Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }
        // GET: CMSHospitalController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<CMSHospitalModel> CMSHospitals = _addCMSHospitalService.GetCMSHospitalModelByParam(id, false);
            CMSHospitalModel CMSHospitalModel = CMSHospitals.FirstOrDefault();
            CMSHospitalModel.ViewOnly = CMSHospitalModel.IsDeleted;
            CMSHospitalModel.IsNew = false;
            return View("AddEditCMSHospital", CMSHospitalModel);
        }


        // GET: CMSHospitalController/Delete/5
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<CMSHospitalModel> CMSHospitals = _addCMSHospitalService.GetCMSHospitalModelByParam(id, false);
            CMSHospitalModel CMSHospitalModel = CMSHospitals.FirstOrDefault();
           
            CMSHospitalModel.IsDeleted = true;
            _addCMSHospitalService.AddEditCMSHospitalData(CMSHospitalModel, true);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DownloadFile(Guid id, string lang, bool isView = true)
        {

            IEnumerable<CMSHospitalModel> CMSHospitals = _addCMSHospitalService.GetCMSHospitalModelByParam(id, null);
            CMSHospitalModel CMSHospitalModel = CMSHospitals.FirstOrDefault();

            if (lang == "English")
            {
                byte[] fileBytes = CMSHospitalModel.AttachmentFileInEnglish;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + CMSHospitalModel.EnglishFileName);
                return File(fileBytes, CMSHospitalModel.EnglishContentType);
            }
            else if (lang == "Hindi")
            {
                byte[] fileBytes = CMSHospitalModel.AttachmentFileInHindi;
                if (isView)
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + CMSHospitalModel.HindiFileName);
                return File(fileBytes, CMSHospitalModel.HindiContentType);
            }

            throw new Exception("Specify file type");
        }


    }
}
