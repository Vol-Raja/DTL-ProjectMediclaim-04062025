using DTL.Business.UserManagement;
using DTL.WebApp.Common.CommonClasses;
using DTL.WebApp.Common.Extensions;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
   // [Authorize(Roles = "Master")] 
    public class MasterUserController : Controller
    {
        private readonly IAdminUser _adminUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MasterUserController(IAdminUser adminUser,
              UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _adminUser = adminUser;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View("MasterDashboard");
         
        }

        public IActionResult ModulesList()
        {
            return View();
        }

        public IActionResult SubModulesList()
        {
            return View();
        }
        public IActionResult HospitalList()
        {
            return View();
        }
        public IActionResult AddClaimType()
        {
            return View();
        }
        public IActionResult AddHospital()
        {
            return View();
        }
        public IActionResult claimTypeArchives()
        {
            return View();
        }
        public IActionResult claimTypeList()
        {
            return View();
        }
        public IActionResult HospitalArchives()
        {
            return View();
        }
        public IActionResult DepartmentList()
        {
            return View();
        }
        public IActionResult AddDepartment()
        {
            return View();
        }
        public IActionResult AddDesignation()
        {
            return View();
        }
        public IActionResult AddMedicalCardCategory()
        {
            return View();
        }
        public IActionResult AddOrganization()
        {
            return View();
        }
        public IActionResult AddTypeOfClaim()
        {
            return View();
        }
        public IActionResult DepartmentArchives()
        {
            return View();
        }
        public IActionResult DesignationArchives()
        {
            return View();
        }
        public IActionResult DesignationList()
        {
            return View();
        }
        public IActionResult MedicalCardCategoryArchives()
        {
            return View();
        }
        public IActionResult MedicalCardCategoryList()
        {
            return View();
        }
        public IActionResult OrganizationArchives()
        {
            return View();
        }
        public IActionResult RolesList()
        {
            return View();
        }
        public IActionResult OrganizationList()
        {
            return View();
        }
        public IActionResult TypeOfClaimArchivesList()
        {
            return View();
        }
        public IActionResult TypeOfClaimList()
        {
            return View();
        }
    }
}
