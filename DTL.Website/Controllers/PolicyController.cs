using DTL.Business.CMS.FooterLegalSection;
using DTL.Business.CMS.Form;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.Website.Controllers
{
    public class PolicyController : Controller
    {
        private static IFooterLegalSectionService _addFooterLegalSectionService;

        public PolicyController(IFooterLegalSectionService addFooterLegalSectionService)
        {
            _addFooterLegalSectionService = addFooterLegalSectionService;
        }
        public ActionResult Index()
        {

            return View();
        }

        public IActionResult Accessibility()
        {
            return View();
        }
        public IActionResult SiteMap()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }
            return View();
        }
        public IActionResult ContentArchivalPolicy()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }
            return View();
        }
        public IActionResult ContentContributionPolicy()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }
            return View();
        }
        public IActionResult ContentReviewPolicy()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }
            return View();
        }
        public IActionResult ContingencyManagementPlan()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }
            return View();
        }
        public IActionResult CopyrightPolicy()
        {

            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }

            return View();
        }
        public IActionResult Disclaimer()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }
            return View();
        }
        public IActionResult HyperLinkingPolicy()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }
            return View();
        }
        public IActionResult SecurityPolicy()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }
            return View();
        }
        public IActionResult TermsConditions()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }
            return View();
        }
        public IActionResult WebsiteMonitoringPlan()
        {
            IEnumerable<FooterLegalSectionModel> footerLegalSectionModels = _addFooterLegalSectionService.GetFooterLegalSectionModelByParam(null, false);
            if (footerLegalSectionModels?.Count() > 0)
            {
                return View(footerLegalSectionModels.First());
            }
            return View();
        }
        public IActionResult ScreenReaderAccess()
        {
            return View();
        }

    }
}
