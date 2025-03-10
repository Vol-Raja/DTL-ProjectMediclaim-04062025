using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTL.Business.UserManagement;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using DTL.Model.Models.UserManagement;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAssignPermission _assignPermission;

        public DashboardController(UserManager<ApplicationUser> userManager, IAssignPermission assignPermission)
        {
            _userManager = userManager;
            _assignPermission = assignPermission;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = User.Identity;
            IEnumerable<AssignPermissionModel> assignPermissionModels = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, null, null);

            return View(assignPermissionModels);
            //return View();
        }
        [HttpGet]
        public IActionResult HospitalDashboard()
        {
            var user = User.Identity;
            IEnumerable<AssignPermissionModel> assignPermissionModels = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, null, null);

            return View(assignPermissionModels);
            //return View();
        }
        [HttpGet]
        public IActionResult EMPDashboard()
        {
            var user = User.Identity;
            IEnumerable<AssignPermissionModel> assignPermissionModels = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, null, null);

            return View(assignPermissionModels);
            //return View();
        }
    }
}
