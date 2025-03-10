using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    [Area("GPF")]
    public class ReportsController : Controller
    {
      
        // GET: ReportsController
        public ActionResult Index()
        {
            return View();
        } 
        public ActionResult Agreports()
        {
            return View();
        } 
        public ActionResult Amdmreports()
        {
            return View();
        }
        public ActionResult DisbusmentReport()
        {
            return View();
        }  
        public ActionResult GPFProcessingReport()
        {
            return View();
        }
    }
}
