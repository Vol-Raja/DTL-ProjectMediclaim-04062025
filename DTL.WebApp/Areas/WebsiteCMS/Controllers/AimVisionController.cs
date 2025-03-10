using DTL.Business.CMS.AimVision;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class AimVisionController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IAimVisionService _addAimVisionService;

        public AimVisionController(IAimVisionService addAimVisionService)
        {
            _addAimVisionService = addAimVisionService;
        }
        // GET: AimVisionController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<AimVisionModel> AimVisionModels = _addAimVisionService.GetAimVisionModelByParam(null, isArchieved, isArchieved);
            return View(AimVisionModels.OrderByDescending(x => x.CreatedDate));

        }

        // GET: AimVisionController/Details/5
        public ActionResult Details(Guid id)
        {
            IEnumerable<AimVisionModel> AimVisions = _addAimVisionService.GetAimVisionModelByParam(id, null, null);
            AimVisionModel AimVisionModel = AimVisions.FirstOrDefault();
            AimVisionModel.ViewOnly = true;
            AimVisionModel.IsNew = false;
            return View("AddEditAimVision", AimVisionModel);
        }

        // GET: AimVisionController/Create
        public ActionResult Create()
        {
            AimVisionModel AimVisionModel = new()
            {
                IsNew = true
            };
            return View("AddEditAimVision", AimVisionModel);
        }

        // POST: AimVisionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AimVisionController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<AimVisionModel> AimVisions = _addAimVisionService.GetAimVisionModelByParam(id, null, null);
            AimVisionModel AimVisionModel = AimVisions.FirstOrDefault();
            
            AimVisionModel.IsNew = false;
            return View("AddEditAimVision", AimVisionModel);
        }


        // GET: AimVisionController/Delete/5
        public ActionResult Delete(Guid id)
        {
            IEnumerable<AimVisionModel> AimVisions = _addAimVisionService.GetAimVisionModelByParam(id, null, null);
            AimVisionModel AimVisionModel = AimVisions.FirstOrDefault();
            AimVisionModel.IsDeleted = true;
            _addAimVisionService.AddEditAimVisionData(AimVisionModel, true);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AimVisionModel AimVisionModel)
        {
            try
            {
                if (AimVisionModel.ID == System.Guid.Empty)
                    AimVisionModel.CreatedBy = User.Identity.Name;
                else
                    AimVisionModel.ModifideBy = User.Identity.Name;

               
                string result = _addAimVisionService.AddEditAimVisionData(AimVisionModel, AimVisionModel.ID != Guid.Empty);
                if (result == "add" || result == "update")
                {
                    TempData["Message"] = result == "add"? "Data saved successfully": "Data updated successfully";
                    return RedirectToAction(nameof(Index)); //Json(result);
                }
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("AimVision Save", ex);
                //return View("Error");
                return Json(ex.Message);

            }
        }

        public ActionResult DownloadFile(Guid AimVisionId)
        {
            IEnumerable<AimVisionModel> AimVisions = _addAimVisionService.GetAimVisionModelByParam(AimVisionId, null, null);
            AimVisionModel AimVisionModel = AimVisions.FirstOrDefault();

            return Ok();
           
        }
    }
}
