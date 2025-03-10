using DocumentFormat.OpenXml.InkML;
using DTL.Business.CMS.About;
using DTL.Business.CMS.AimVision;
using DTL.Business.CMS.BannerImage;
using DTL.Business.CMS.CMSHospital;
using DTL.Business.CMS.Event;
using DTL.Business.CMS.Link;
using DTL.Business.CMS.Notice;
using DTL.Business.CMS.SocialMediaAccount;
using DTL.Business.CMS.Tender;
using DTL.Business.CMS.Testimony;
using DTL.Business.CMS.WhatsNew;
using DTL.Business.Visitor;
using DTL.Model.Models;
using DTL.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace DTL.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration _configuration;

        ITenderService _tenderService;
        ILinkService _linkService;
        INoticeService _noticeService;
        IWhatsNewService _whatsNewService;
        IAboutService _aboutService;
        IAimVisionService _aimVisionService;
        IBannerImageService _bannerImageService;
        ISocialMediaAccountService _socialMediaAccountService;
        IEventService _eventsService;
        IVisitorService _visitorService;
        ITestimonyService _addTestimonyService;
        public HomeController(ITenderService tenderService,
        ILinkService linkService,
        INoticeService noticeService,
        IWhatsNewService whatsNewService,
        IAboutService aboutService,
        IAimVisionService aimVisionService,
        IEventService eventsService,
        IVisitorService visitorService,
        ISocialMediaAccountService socialMediaAccountService,
        ITestimonyService addTestimonyService,
        IBannerImageService bannerImageService)
        {
            _tenderService = tenderService;
            _linkService = linkService;

            _noticeService = noticeService;
            _whatsNewService = whatsNewService;
            _aboutService = aboutService;
            _aimVisionService = aimVisionService;
            _bannerImageService = bannerImageService;
            _socialMediaAccountService = socialMediaAccountService;
            _eventsService = eventsService;
            _addTestimonyService = addTestimonyService;
            _visitorService = visitorService;

        }


        public IActionResult Index()
        {
            HomeModel model = new();
            try
            {
                model = new HomeModel();
                model.Notices = _noticeService.GetNoticeModelByParam(null, false, false)?.OrderByDescending(x => x.CreatedDate);
                model.Links = _linkService.GetLinkModelByParam(null, false, false)?.OrderByDescending(x => x.CreatedDate);
                model.Tenders = _tenderService.GetTenderModelByParam(null, false, false)?.OrderByDescending(x => x.CreatedDate);
                model.WhatsNews = _whatsNewService.GetWhatsNewModelByParam(null, false, false)?.OrderByDescending(x => x.CreatedDate);
                model.AboutUs = _aboutService.GetAboutModelByParam(null, false, false)?.LastOrDefault();
                model.AimVision = _aimVisionService.GetAimVisionModelByParam(null, false, false)?.LastOrDefault();
                model.BannerImages = _bannerImageService.GetBannerImageModelByParam(null, false, null, null)?.OrderByDescending(x => x.CreatedDate);
                model.SocialMediaAccount = _socialMediaAccountService.GetSocialMediaAccountModelByParam(null, false)?.LastOrDefault();
                model.Events = _eventsService.GetEventModelByParam(null, false)?.OrderByDescending(x => x.CreatedDate);
                model.Testimonies = _addTestimonyService.GetTestimonyModelByParam(null, false)?.OrderByDescending(x => x.CreatedDate);
                model.VisitorModel = _visitorService.GetVisitor().FirstOrDefault();
                if (model.SocialMediaAccount != null)
                {
                    PersistData("Facebook", model.SocialMediaAccount.Facebook);
                    PersistData("Instagram", model.SocialMediaAccount.Instagram);
                    PersistData("Youtube", model.SocialMediaAccount.Youtube);
                    PersistData("Twitter", model.SocialMediaAccount.Twitter);
                  
                }
                if (model.VisitorModel != null)
                {
                    PersistData("TotalVisitorCount", string.Format("{0:n0}", model.VisitorModel.TotalVisitorCount));
                    PersistData("TodayVisitorCount", string.Format("{0:n0}", model.VisitorModel.TodayVisitorCount));

                  
                }
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(assembly.Location);
                DateTime lastModified = fileInfo.LastWriteTime;
                
                PersistData("LastWriteTime", lastModified.ToString("dd/MM/yyyy"));
                UpdateVisitor();
            }
            catch (Exception ex)
            {
                throw;
            }
            string lang = Request.Cookies["lang"];
            if (lang?.ToLower() == "hn")
            {
                return View("hn/" + "index", model);
            }
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ChangeLanguage(string lang = "en")
        {
            Response.Cookies.Append(
                "Lang",
                lang,
                new CookieOptions()
                {
                    // Expires = DateTimeOffset.MaxValue,
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                }
            );

            if (string.IsNullOrEmpty(Request.Headers["Referer"]))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                string redirectUrl = Request.Headers["Referer"];
                return Redirect(redirectUrl);
            }

            return RedirectToAction(nameof(Index));
        }

        void UpdateVisitor()
        {

            string visitorId = Request.Cookies["VisitorId"];
            if (visitorId == null)
            {
                //Save to db
                var id = Guid.NewGuid();
                VisitorModel visitorModel = new VisitorModel();
                visitorModel.Id = id;
                visitorModel.VisitDate = DateTime.Now;
                try
                {
                    _visitorService.AddEditVisitorData(visitorModel);
                }
                catch (Exception)
                {

                    
                }

                Response.Cookies.Append("VisitorId", id.ToString(), new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true,
                    Secure = false,
                    Expires = DateTimeOffset.Now.AddDays(30)
                });
            }
        }

        void SetCookie(string key, string value)
        {
            ControllerContext.HttpContext.Response.Cookies.Delete(key);
            ControllerContext.HttpContext.Response.Cookies.Append(key, value, new CookieOptions { Expires = DateTime.Now.AddDays(1) });
        }
        void PersistData(string key, string value)
        {
            SetCookie(key, value);
            TempData[key] = value;
        }
    }
}
