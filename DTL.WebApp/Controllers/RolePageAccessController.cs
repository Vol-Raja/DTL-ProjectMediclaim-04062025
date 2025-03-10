using DTL.Business.Auth;
using DTL.Model.Models;
using DTL.WebApp.Common.CommonClasses;
using DTL.WebApp.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{
    public class RolePageAccessController : Controller
    {
        private static IAuthService _authService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolePageAccessController(IAuthService authService,
             UserManager<IdentityUser> userManager,
             RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _authService.GetRolePageAccessAsync();
                return View(data);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        public IActionResult AddPageAccess()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();
            ViewBag.Pages = typeof(MenuPages).GetAllPublicConstantValues<string>();
            return PartialView("AddEditRolePages", new RolePageModel());
        }


        public IActionResult AddEditRolePages(RolePageModel rolePage)
        {
            try
            {
                if (rolePage.Id == 0)
                {
                    rolePage.RoleId = _roleManager.Roles.FirstOrDefault(x => x.Name == rolePage.RoleName.Trim()).Id;
                    var data = _authService.AddEditRolePageAccess(0, rolePage.RoleId, rolePage.PageName);
                    return Json("add");
                }
                else
                {
                    rolePage.RoleId = _roleManager.Roles.FirstOrDefault(x => x.Name == rolePage.RoleName.Trim()).Id;
                    _authService.AddEditRolePageAccess(rolePage.Id, rolePage.RoleId, rolePage.PageName);
                    return Json("update");
                }
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }

        public IActionResult GetRolePagesById(int rolePageId)
        {
            try
            {
                ViewBag.Roles = _roleManager.Roles.Select(x=>x.Name).ToList();
                ViewBag.Pages = typeof(MenuPages).GetAllPublicConstantValues<string>();
                var data = _authService.GetRolePageAccessByIdAsync(rolePageId);
                return PartialView("AddEditRolePages", data);
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }
        public IActionResult DeleteRolePages(int rolePageId)
        {
            try
            {
                var data = _authService.DeleteRolePageAccess(rolePageId);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }
    }
}
