using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class OPDController : Controller
    {
        // GET: OPDController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MadiclaimRaise()
        {
            return View();
        }
        public ActionResult AppoveReject()
        {
            return View();
        }
    }
}
