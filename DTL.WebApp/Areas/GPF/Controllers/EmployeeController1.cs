using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    public class EmployeeController1 : Controller
    {
        [Area("GPF")]
        public ActionResult Index()
        {
            return View();
        }
     
        public ActionResult AddEmployeeForm()
        {
            return View();
        }
        public ActionResult EmployeeProfileCreationArchives()
        {
            return View();
        }
        public ActionResult viewEmployeeProfile()
        {
            return View();
        }
      

    }
}
