using DTL.WebApp.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DTL.WebApp.Areas.WebsiteCMS
{
    [Area("WebsiteCMS")]
    [Authorize(Roles = Constants.SUPER_ADMIN)]
    public class DashboardController : Controller
    {
        // GET: DashboardController
        public ActionResult Index()
        {
            return View("index");
        }

    }
}
