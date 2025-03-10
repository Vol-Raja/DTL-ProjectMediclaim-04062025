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

namespace DTL.Website.Controllers
{


    public class BannerImageController : Controller
    {
        private static IBannerImageService _addBannerImageService;

        public BannerImageController(IBannerImageService addBannerImageService)
        {
            _addBannerImageService = addBannerImageService;
        }

        public ActionResult Index()
        {
            IEnumerable<BannerImageModel> models = _addBannerImageService.GetBannerImageModelByParam(null, false, null, true);
            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", models?.OrderByDescending(x => x.CreatedDate));
            }
            return View(models?.OrderByDescending(x => x.CreatedDate));

        }
        public ActionResult DownloadFile(Guid BannerImageId)
        {
            IEnumerable<BannerImageModel> BannerImages = _addBannerImageService.GetBannerImageModelByParam(BannerImageId, null, null, null);
            BannerImageModel BannerImageModel = BannerImages.FirstOrDefault();

            if (BannerImageModel == null)
                return Ok();
            return File(BannerImageModel.Image, BannerImageModel.ContentType);

        }

    }
}
