using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.EmployeeDashboard.Controllers
{
    public class EmployeeLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
