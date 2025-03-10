using DTL.Business.CMS.Link;
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
    public class LinkController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static ILinkService _addLinkService;

        public LinkController(ILinkService addLinkService)
        {
            _addLinkService = addLinkService;
        }
        // GET: LinkController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<LinkModel> linkModels = _addLinkService.GetLinkModelByParam(null, isArchieved, isArchieved);
            return View(linkModels.OrderByDescending(x => x.CreatedDate));

        }

        // GET: LinkController/Details/5
        public ActionResult Details(Guid id)
        {
            IEnumerable<LinkModel> links = _addLinkService.GetLinkModelByParam(id, null, null);
            LinkModel linkModel = links.FirstOrDefault();
            linkModel.ViewOnly = true;
            linkModel.IsNew = false;
            return View("AddEditLink", linkModel);
        }

        // GET: LinkController/Create
        public ActionResult Create()
        {
            LinkModel linkModel = new()
            {
                IsNew = true
            };
            return View("AddEditLink", linkModel);
        }

        // POST: LinkController/Create
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

        // GET: LinkController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<LinkModel> links = _addLinkService.GetLinkModelByParam(id, null, null);
            LinkModel linkModel = links.FirstOrDefault();
            linkModel.ViewOnly = linkModel.IsArchieved;
            linkModel.IsNew = false;
            return View("AddEditLink", linkModel);
        }


        // GET: LinkController/Delete/5
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<LinkModel> links = _addLinkService.GetLinkModelByParam(id, null, null);
            LinkModel linkModel = links.FirstOrDefault();
            linkModel.IsArchieved = true;
            linkModel.IsDeleted = true;
            _addLinkService.AddEditLinkData(linkModel, true);
            return Json("success");
            //  return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(LinkModel linkModel)
        {
            try
            {
                if (linkModel.ID == System.Guid.Empty)
                    linkModel.CreatedBy = User.Identity.Name;
                else
                    linkModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {
                            case "FileContent":
                                linkModel.FileContent = ms1.ToArray();
                                linkModel.DocumentFileName = file.FileName;
                                linkModel.FileSize = Convert.ToInt32(file.Length / 1024);//KB
                                linkModel.ContentType = file.ContentType;
                                break;

                        }
                    }
                else
                {
                    if (linkModel.ID != Guid.Empty)
                    {
                        IEnumerable<LinkModel> linkModels = _addLinkService.GetLinkModelByParam(linkModel.ID, null, null);
                        LinkModel oldModel = linkModels.FirstOrDefault();

                        linkModel.DocumentFileName = oldModel.DocumentFileName;
                        linkModel.ContentType = oldModel.ContentType;
                        linkModel.FileContent = oldModel.FileContent;

                    }

                }
                string result = _addLinkService.AddEditLinkData(linkModel, linkModel.ID != Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("Link Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }

        public ActionResult DownloadFile(Guid linkId)
        {
            IEnumerable<LinkModel> links = _addLinkService.GetLinkModelByParam(linkId, null, null);
            LinkModel linkModel = links.FirstOrDefault();

            byte[] fileBytes = linkModel.FileContent;
            return File(fileBytes, linkModel.ContentType, linkModel.DocumentFileName);
        }
    }
}
