using DocumentFormat.OpenXml.Drawing.Charts;
using DTL.Business.CMS.About;
using DTL.Model.Models;
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
    public class AboutController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IAboutService _addAboutService;

        public AboutController(IAboutService addAboutService)
        {
            _addAboutService = addAboutService;
        }
        // GET: AboutController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<AboutModel> AboutModels = _addAboutService.GetAboutModelByParam(null, isArchieved, isArchieved);
            return View(AboutModels.OrderByDescending(x => x.CreatedDate));

        }
        public ActionResult Dashboard(bool isArchieved = false)
        {
            return View();

        }
        // GET: AboutController/Details/5
        public ActionResult Details(Guid id, bool isView = false)
        {
            IEnumerable<AboutModel> Abouts = _addAboutService.GetAboutModelByParam(id, null, null);
            AboutModel AboutModel = Abouts.FirstOrDefault();
            AboutModel.ViewOnly = isView;
            AboutModel.IsNew = false;
            return View("AddEditAbout", AboutModel);
        }

        // GET: AboutController/Create
        public ActionResult Create()
        {
            AboutModel AboutModel = new()
            {
                IsNew = true
            };
            return View("AddEditAbout", AboutModel);
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
            IEnumerable<AboutModel> Abouts = _addAboutService.GetAboutModelByParam(id, null, null);
            AboutModel AboutModel = Abouts.FirstOrDefault();

            AboutModel.IsNew = false;
            return View("AddEditAbout", AboutModel);
        }


        // GET: AboutController/Delete/5
        [HttpPost]
        public ActionResult Delete([FromBody] Guid Id)
        {
            IEnumerable<AboutModel> Abouts = _addAboutService.GetAboutModelByParam(Id, null, null);
            AboutModel AboutModel = Abouts.FirstOrDefault();
            AboutModel.IsDeleted = true;
            var result = _addAboutService.AddEditAboutData(AboutModel, true);
            //  return RedirectToAction(nameof(Index));
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AboutModel AboutModel)
        {
            try
            {
                if (AboutModel.ID == System.Guid.Empty)
                    AboutModel.CreatedBy = User.Identity.Name;
                else
                    AboutModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {

                            case "Image":
                                AboutModel.Image = ms1.ToArray();
                                AboutModel.ImageName = file.FileName;
                                AboutModel.ImageContentType = file.ContentType;
                                break;
                        }
                    }

                string result = _addAboutService.AddEditAboutData(AboutModel, AboutModel.ID != Guid.Empty);
                if (result == "add" || result == "update")
                {
                    TempData["Message"] = "Changes are saved";
                    return Json(result);
                    //return RedirectToAction(nameof(Index)); //Json(result);
                }
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("About Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }

        public ActionResult DownloadFile(Guid AboutId, bool isView = true)
        {
            IEnumerable<AboutModel> Abouts = _addAboutService.GetAboutModelByParam(AboutId, null, null);
            AboutModel AboutModel = Abouts.FirstOrDefault();
            if (AboutModel == null) return Ok();

            byte[] fileBytes = AboutModel.Image;
            if (isView)
                Response.Headers.Add("Content-Disposition", "inline; filename=" + AboutModel.ImageName);
            return File(fileBytes, AboutModel.ImageContentType);

        }
    }
}
