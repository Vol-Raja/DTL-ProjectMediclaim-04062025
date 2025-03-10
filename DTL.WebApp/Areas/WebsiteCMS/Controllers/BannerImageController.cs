using DTL.Business.CMS.BannerImage;
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
    public class BannerImageController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IBannerImageService _addBannerImageService;

        public BannerImageController(IBannerImageService addBannerImageService)
        {
            _addBannerImageService = addBannerImageService;
        }
        // GET: BannerImageController
        public ActionResult Index(bool isGallery = false, bool isArchieved = false)
        {
            IEnumerable<BannerImageModel> BannerImageModels = _addBannerImageService.GetBannerImageModelByParam(null, isArchieved, isArchieved, isGallery);
            if (isGallery)
                return View("Index_Gallery", BannerImageModels.OrderByDescending(x => x.CreatedDate));
            else
                return View(BannerImageModels.OrderByDescending(x => x.CreatedDate));

        }

        // GET: BannerImageController/Details/5
        public ActionResult Details(Guid id)
        {
            IEnumerable<BannerImageModel> BannerImages = _addBannerImageService.GetBannerImageModelByParam(id, null, null, null);
            BannerImageModel BannerImageModel = BannerImages.FirstOrDefault();
            BannerImageModel.ViewOnly = true;
            BannerImageModel.IsNew = false;
            return View("AddEditBannerImage", BannerImageModel);
        }

        // GET: BannerImageController/Create
        public ActionResult Create(bool IsGallery = false)
        {
            BannerImageModel BannerImageModel = new()
            {
                IsNew = true,
                IsGallery = IsGallery
            };

            if (BannerImageModel.IsGallery)
                return View("AddEditGallery", BannerImageModel);
            else
                return View("AddEditBannerImage", BannerImageModel);
        }

        // POST: BannerImageController/Create
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

        // GET: BannerImageController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<BannerImageModel> BannerImages = _addBannerImageService.GetBannerImageModelByParam(id, null, null, null);
            BannerImageModel BannerImageModel = BannerImages.FirstOrDefault();

            BannerImageModel.IsNew = false;

            if (BannerImageModel.IsGallery)
                return View("AddEditGallery", BannerImageModel);
            else
                return View("AddEditBannerImage", BannerImageModel);


        }


        // GET: BannerImageController/Delete/5
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<BannerImageModel> BannerImages = _addBannerImageService.GetBannerImageModelByParam(id, null, null, null);
            BannerImageModel BannerImageModel = BannerImages.FirstOrDefault();
            BannerImageModel.IsDeleted = true;
            _addBannerImageService.AddEditBannerImageData(BannerImageModel, true);
            return RedirectToAction(nameof(Index), BannerImageModel.IsGallery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BannerImageModel BannerImageModel)
        {
            try
            {
                if (BannerImageModel.ID == System.Guid.Empty)
                    BannerImageModel.CreatedBy = User.Identity.Name;
                else
                    BannerImageModel.ModifideBy = User.Identity.Name;

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
                                BannerImageModel.Image = ms1.ToArray();
                                BannerImageModel.FileName = file.FileName;
                                BannerImageModel.ContentType = file.ContentType;
                                break;
                        }
                    }
                }
                else
                {
                    if (BannerImageModel.ID != Guid.Empty)
                    {
                        IEnumerable<BannerImageModel> Models = _addBannerImageService.GetBannerImageModelByParam(BannerImageModel.ID, null, null, null);
                        BannerImageModel oldModel = Models.FirstOrDefault();
                        BannerImageModel.Image = oldModel.Image;
                        BannerImageModel.FileName = oldModel.FileName;
                        BannerImageModel.ContentType = oldModel.ContentType;

                    }
                }
                string result = _addBannerImageService.AddEditBannerImageData(BannerImageModel, BannerImageModel.ID != Guid.Empty);
                if (result == "add" || result == "update")
                {
                    TempData["Message"] = "Changes are saved";
                    return Json(result);//RedirectToAction(nameof(Index)); 
                }
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("BannerImage Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }

        public ActionResult DownloadFile(Guid BannerImageId)
        {
            IEnumerable<BannerImageModel> BannerImages = _addBannerImageService.GetBannerImageModelByParam(BannerImageId, null, null, null);
            BannerImageModel BannerImageModel = BannerImages.FirstOrDefault();


            return File(BannerImageModel.Image, BannerImageModel.ContentType, BannerImageModel.FileName);

        }
    }
}
