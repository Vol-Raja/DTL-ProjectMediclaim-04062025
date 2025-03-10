using DTL.Business.UserManagement;
using DTL.Model.Models.UserManagement;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.EmployeeDashboard.Controllers
{

    [Area("EmployeeDashboard")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAssignPermission _assignPermission;

        public EmployeeController(UserManager<ApplicationUser> userManager, IAssignPermission assignPermission)
        {
            _userManager = userManager;
            _assignPermission = assignPermission;
        }

        [HttpGet]
        public IActionResult Emp_Dashboard()
        {
            var user = User.Identity;
            IEnumerable<AssignPermissionModel> assignPermissionModels = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, null, null);

            return View(assignPermissionModels);
            //return View();
        }
    }
}
