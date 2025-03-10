using Microsoft.AspNetCore.Mvc;
using DTL.Business.CMS.Tender;
using DTL.Model.Models.CMS;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.StaticFiles;
using System.Linq;

namespace DTL.Website.Controllers
{
    public class TenderController : Controller
    {
        private static ITenderService _addTenderService;

        public TenderController(ITenderService addTenderService)
        {
            _addTenderService = addTenderService;
        }
        // GET: TenderController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<TenderModel> tenders = _addTenderService.GetTenderModelByParam(null, isArchieved, isArchieved);

            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", tenders?.OrderByDescending(x => x.CreatedDate));
            }

            return View(tenders?.OrderByDescending(x => x.CreatedDate));
        }
        public IActionResult Download(Guid id, string lang = "en")
        {
            IEnumerable<TenderModel> Tenders = _addTenderService.GetTenderModelByParam(id, null, null);
            TenderModel TenderModel = Tenders.FirstOrDefault();
            string contentType = "application/pdf";
            if (lang == "en")
            {
                byte[] fileBytes = TenderModel.AttachmentFileInEnglish;

                if (fileBytes == null) return new StatusCodeResult(404);
            //    new FileExtensionContentTypeProvider().TryGetContentType(TenderModel.AttachmentTitleInEnglish, out contentType);

                return File(fileBytes, contentType);
            }
            else if (lang == "hn")
            {
                byte[] fileBytes = TenderModel.AttachmentFileInHindi;

                if (fileBytes == null) return new StatusCodeResult(404);
                //new FileExtensionContentTypeProvider().TryGetContentType(TenderModel.AttachmentTitleInHindi, out contentType);
              
                return File(fileBytes, contentType);
            }
            return Ok();
        }
    }
}
