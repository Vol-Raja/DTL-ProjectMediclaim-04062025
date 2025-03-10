using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult AddRole()
        {
            return PartialView("AddEditRoles", new IdentityRole());
        }

        public async Task<IActionResult> AddEditRole(IdentityRole role)
        {
            try
            {
                if (!_roleManager.Roles.Any(x => x.Id == role.Id))
                {
                    if (!await _roleManager.RoleExistsAsync(role.Name))
                    {
                        await _roleManager.CreateAsync(role);
                        return Json("add");
                    }
                    else
                    {
                        return Json("duplicate");
                    }
                }
                else
                {
                    var tempRole = _roleManager.Roles.FirstOrDefault(x => x.Id == role.Id);
                    tempRole.Name = role.Name;
                    await _roleManager.UpdateAsync(tempRole);
                    return Json("update");
                }

            }
            catch (Exception ex)
            {
                return Json("error");
            }
        }

        [HttpPost]
        public IActionResult GetRoleById(string roleId)
        {
            try
            {
                var role = _roleManager.Roles.FirstOrDefault(x => x.Id == roleId);
                return PartialView("AddEditRoles", role);
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            try
            {
                var role = _roleManager.Roles.FirstOrDefault(x => x.Id == roleId);
                await _roleManager.DeleteAsync(role);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
    }
}
