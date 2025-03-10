using DTL.Business.CMS.Link;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTL.Website.Controllers
{
    public class LinkController : Controller
    {
        private static ILinkService _addLinkService;

        public LinkController(ILinkService addLinkService)
        {
            _addLinkService = addLinkService;
        }
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<LinkModel> linkModels = _addLinkService.GetLinkModelByParam(null, isArchieved, isArchieved);

            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", linkModels?.OrderByDescending(x => x.CreatedDate));
            }
            return View(linkModels?.OrderByDescending(x => x.CreatedDate));

        }
        public IActionResult Download(Guid id)
        {
            IEnumerable<LinkModel> links = _addLinkService.GetLinkModelByParam(id, null, null);
            LinkModel linkModel = links.FirstOrDefault();

            byte[] fileBytes = linkModel.FileContent;
            string contentType;
            if (linkModel.ContentType == null)
                new FileExtensionContentTypeProvider().TryGetContentType(linkModel.DocumentFileName, out contentType);
            else
                contentType = linkModel.ContentType;
            return File(fileBytes, contentType);
        }
    }
}
