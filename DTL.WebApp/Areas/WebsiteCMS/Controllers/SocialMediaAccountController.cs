using DTL.Business.CMS.FooterLegalSection;
using DTL.Business.CMS.SocialMediaAccount;
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
    public class SocialMediaAccountController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static ISocialMediaAccountService _addSocialMediaAccountService;
        private static IFooterLegalSectionService _addFooterLegalSectionService;

        public SocialMediaAccountController(ISocialMediaAccountService addSocialMediaAccountService, IFooterLegalSectionService addFooterLegalSectionService)
        {
            _addSocialMediaAccountService = addSocialMediaAccountService;
            _addFooterLegalSectionService = addFooterLegalSectionService;
        }
        // GET: SocialMediaAccountController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<SocialMediaAccountModel> SocialMediaAccounts = _addSocialMediaAccountService.GetSocialMediaAccountModelByParam(null, isArchieved);
            return View(SocialMediaAccounts.OrderByDescending(x => x.CreatedDate));
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult AddLegal(Guid id)
        {
            FooterLegalSectionModel footerLegalSectionModel = new FooterLegalSectionModel();
            if (id == Guid.Empty)
            {
                footerLegalSectionModel.IsNew = true;
                return View(footerLegalSectionModel);
            }
            else
            {
                IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(id, false);
                return View(footerLegalSectionModels.FirstOrDefault());

            }

        }
        public ActionResult legalList()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            return View(footerLegalSectionModels);
            
        }
        // GET: SocialMediaAccountController/Details/5
        public ActionResult Details(Guid id)
        {

            IEnumerable<SocialMediaAccountModel> SocialMediaAccounts = _addSocialMediaAccountService.GetSocialMediaAccountModelByParam(id, null);
            SocialMediaAccountModel SocialMediaAccountModel = SocialMediaAccounts.FirstOrDefault();
            SocialMediaAccountModel.ViewOnly = true;
            SocialMediaAccountModel.IsNew = false;
            return View("AddEditSocialMediaAccount", SocialMediaAccountModel);
        }

        // GET: SocialMediaAccountController/Create
        public ActionResult Create()
        {
            SocialMediaAccountModel SocialMediaAccountModel = new();
            SocialMediaAccountModel.IsNew = true;
            return View("AddEditSocialMediaAccount", SocialMediaAccountModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SocialMediaAccountModel SocialMediaAccountModel)
        {
            try
            {
                if (SocialMediaAccountModel.ID == Guid.Empty)
                    SocialMediaAccountModel.CreatedBy = User.Identity.Name;
                else
                    SocialMediaAccountModel.ModifideBy = User.Identity.Name;






                string result = _addSocialMediaAccountService.AddEditSocialMediaAccountData(SocialMediaAccountModel, SocialMediaAccountModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("SocialMediaAccount Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }
        // GET: SocialMediaAccountController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<SocialMediaAccountModel> SocialMediaAccounts = _addSocialMediaAccountService.GetSocialMediaAccountModelByParam(id, false);
            SocialMediaAccountModel SocialMediaAccountModel = SocialMediaAccounts.FirstOrDefault();
            SocialMediaAccountModel.ViewOnly = SocialMediaAccountModel.IsDeleted;
            SocialMediaAccountModel.IsNew = false;
            return View("AddEditSocialMediaAccount", SocialMediaAccountModel);
        }


        // GET: SocialMediaAccountController/Delete/5
        public ActionResult Delete([FromBody] Guid id)
        {
            IEnumerable<SocialMediaAccountModel> SocialMediaAccounts = _addSocialMediaAccountService.GetSocialMediaAccountModelByParam(id, false);
            SocialMediaAccountModel SocialMediaAccountModel = SocialMediaAccounts.FirstOrDefault();

            SocialMediaAccountModel.IsDeleted = true;
            _addSocialMediaAccountService.AddEditSocialMediaAccountData(SocialMediaAccountModel, true);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveFooterLegalSection(FooterLegalSectionModel FooterLegalSectionModel)
        {
            try
            {
                if (FooterLegalSectionModel.ID == Guid.Empty)
                    FooterLegalSectionModel.CreatedBy = User.Identity.Name;
                else
                    FooterLegalSectionModel.ModifideBy = User.Identity.Name;


                string result = _addFooterLegalSectionService.AddEditFooterLegalSectionData(FooterLegalSectionModel, FooterLegalSectionModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(result);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("FooterLegalSection Save", ex);
                //return View("Error");
                return Json(ex.Message);

            }
        }
        public ActionResult DeleteFooterLegalSection([FromBody] Guid id)
        {
            IEnumerable<FooterLegalSectionModel> FooterLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(id, false);
            FooterLegalSectionModel FooterLegalSectionModel = FooterLegalSectionModels.FirstOrDefault();

            FooterLegalSectionModel.IsDeleted = true;
             _addFooterLegalSectionService.AddEditFooterLegalSectionData(FooterLegalSectionModel, true);
            return RedirectToAction(nameof(Index));
        }

    }
}
