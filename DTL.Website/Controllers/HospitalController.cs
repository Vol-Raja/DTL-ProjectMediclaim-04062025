using DTL.Business.CMS.CMSHospital;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.Website.Controllers
{
    public class HospitalController : Controller
    {
        private static ICMSHospitalService _addCMSHospitalService;

        public HospitalController(ICMSHospitalService addCMSHospitalService)
        {
            _addCMSHospitalService = addCMSHospitalService;
        }
        public IActionResult Index()
        {
            IEnumerable<CMSHospitalModel> CMSHospitalModels = _addCMSHospitalService.GetCMSHospitalModelByParam(null, false);

            if (CMSHospitalModels.Count() > 0)
                CMSHospitalModels = CMSHospitalModels.OrderByDescending(x => x.CreatedDate);
            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", CMSHospitalModels);
            }
            return View(CMSHospitalModels);
        }
        public IActionResult EmpanelledHospitalList()
        {
            return View();
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
