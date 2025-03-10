using DTL.Business.CMS.About;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static System.Net.WebRequestMethods;

namespace DTL.WebApp.Areas.WebsiteCMS.Controllers
{
    [Area(StringConstants.CMS_AREA)]
    [Authorize(Roles = "SuperAdmin")]
    public class TrusteeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IAboutService _addAboutService;

        public TrusteeController(IAboutService addAboutService)
        {
            _addAboutService = addAboutService;
        }
        // GET: AboutController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<TrusteeModel> TrusteeModels = _addAboutService.GetTrusteeModelByParam(null, isArchieved);
            return View(TrusteeModels.OrderByDescending(x => x.CreatedDate));

        }

        // GET: AboutController/Details/5
        public ActionResult Details(Guid id)
        {
            IEnumerable<TrusteeModel> Trustees = _addAboutService.GetTrusteeModelByParam(id, null);
            TrusteeModel Trustee = Trustees.FirstOrDefault();
            Trustee.ReadOnly = true;
            Trustee.IsNew = false;
            return View("AddEditAbout", Trustee);
        }

        // GET: AboutController/Create
        public ActionResult Create()
        {
            TrusteeModel TrusteeModel = new()
            {
                IsNew = true
            };
            return View("AddEditTrustee", TrusteeModel);
        }

        // POST: AboutController/Create
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

        // GET: AboutController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<TrusteeModel> trusteeModels = _addAboutService.GetTrusteeModelByParam(id, null);
            TrusteeModel trusteeModel = trusteeModels.FirstOrDefault();

            trusteeModel.IsNew = false;
            return View("AddEditTrustee", trusteeModel);
        }


        // GET: AboutController/Delete/5
        [HttpPost]
        public ActionResult Delete([FromBody] Guid Id)
        {
            IEnumerable<TrusteeModel> TrusteeModels = _addAboutService.GetTrusteeModelByParam(Id, null);
            TrusteeModel TrusteeModel = TrusteeModels.FirstOrDefault();
            TrusteeModel.IsDeleted = true;
            _addAboutService.AddEditTrusteeData(TrusteeModel, true);
            return Json("success");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TrusteeModel trusteeModel)
        {
            try
            {
                if (trusteeModel.ID == Guid.Empty)
                    trusteeModel.CreatedBy = User.Identity.Name;
                else
                    trusteeModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                {
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {

                            case "Image":
                                trusteeModel.Image = ms1.ToArray();
                                trusteeModel.ImageName = file.FileName;
                                trusteeModel.ImageContentType = file.ContentType;
                                break;
                        }
                    }
                }
                else
                {
                    if (trusteeModel.ID != Guid.Empty)
                    {
                        IEnumerable<TrusteeModel> trusteeModels = _addAboutService.GetTrusteeModelByParam(trusteeModel.ID, null);
                        TrusteeModel oldModel = trusteeModels.FirstOrDefault();
                        trusteeModel.Image = oldModel.Image;
                        trusteeModel.ImageName = oldModel.ImageName;
                        trusteeModel.ImageContentType = oldModel.ImageContentType;

                    }
                }

                string result = _addAboutService.AddEditTrusteeData(trusteeModel, trusteeModel.ID != Guid.Empty);
                if ((result == "add") || result == "update")
                {
                    // TempData["Message"] = "Changes are saved";
                    return Json(result);
                    //return RedirectToAction(nameof(Index)); //Json(result);
                }
                else
                    return Json(result);


            }
            catch (Exception ex)
            {
                log.Error("About Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }

        public ActionResult DownloadFile(Guid TrusteeId, bool isView = true)
        {
            IEnumerable<TrusteeModel> TrusteeModels = _addAboutService.GetTrusteeModelByParam(TrusteeId, null);
            TrusteeModel TrusteeModel = TrusteeModels.FirstOrDefault();
            if (TrusteeModel == null) return Ok();

            byte[] fileBytes = TrusteeModel.Image;
            if (isView)
                Response.Headers.Add("Content-Disposition", "inline; filename=" + TrusteeModel.ImageName);
            return File(fileBytes, TrusteeModel.ImageContentType);




        }

    }
}
