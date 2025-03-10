using DTL.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTL.WebApp.Common.Extensions;
using DTL.WebApp.Common.CommonClasses;

namespace DTL.WebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userData = _userManager.Users.ToList();
                var model = new List<UserViewModel>();
                foreach (var data in userData)
                {
                    var tempData = new UserViewModel
                    {
                        Email = data.Email,
                        PhoneNumber = data.PhoneNumber,
                        UserId = data.Id,
                        UserName = data.UserName,
                        fullName = data.fullName

                    };
                    var roleName = await _userManager.GetRolesAsync(data);
                    if (roleName.Count > 0)
                        tempData.Role = string.Join(",", roleName);

                    model.Add(tempData);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult AddUser()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();
            return PartialView("AddEditUser", new UserViewModel());
        }

        public async Task<IActionResult> AddEditUser(UserViewModel userView)
        {
            try
            {
                var tempPassword = "";
                if (string.IsNullOrEmpty(userView.UserId) )
                {
                    if (!_userManager.Users.Any(x => x.Email == userView.Email))
                    {
                        var user = new ApplicationUser()
                        {
                            Email = userView.Email,
                            PhoneNumber = userView.PhoneNumber,
                            UserName = userView.Email,
                            fullName = userView.fullName,
                            EmailConfirmed = true
                        };
                        var password = tempPassword.GeneratePassword();
                        var result = await _userManager.CreateAsync(user,password);
                        IdentityResult setPhoneNumber = await _userManager.SetPhoneNumberAsync(user, user.PhoneNumber);
                        if (result.Succeeded)
                        {
                            foreach (var role in userView.Role.Split(","))
                            {
                                await _userManager.AddToRoleAsync(user, role);
                            }
                            SendEmail.GenerateCreedential(user.fullName, user.Email, password);
                            return Json("add");
                        }
                        else
                        {
                            return Json("addError");
                        }
                    }
                    else
                    {
                        return Json("userExists");
                    }
                }
                else
                {
                    var tempUser = _userManager.Users.FirstOrDefault(x => x.Id == userView.UserId);
                    tempUser.Email = userView.Email;
                    tempUser.PhoneNumber = userView.PhoneNumber;
                    tempUser.UserName = userView.Email;
                    tempUser.fullName = userView.fullName;
                    tempUser.EmailConfirmed = true;
                    var result = await _userManager.UpdateAsync(tempUser);
                    if (result.Succeeded)
                    {
                      var tempRole =  await _userManager.GetRolesAsync(tempUser);
                        await _userManager.RemoveFromRolesAsync(tempUser,tempRole);
                        foreach (var role in userView.Role.Split(","))
                        {
                            await _userManager.AddToRoleAsync(tempUser, role);
                        }
                        return Json("update");
                    }
                    else
                    {
                        return Json("updateError");
                    }
                }

            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }

        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                var data = _userManager.Users.FirstOrDefault(x => x.Id == userId);
                var roleName = await _userManager.GetRolesAsync(data);
                ViewBag.Roles = _roleManager.Roles.ToList();
                return PartialView("AddEditUser", new UserViewModel
                {
                    Email = data.Email,
                    PhoneNumber = data.PhoneNumber,
                    UserId = data.Id,
                    UserName = data.UserName,
                    Role = string.Join(",", roleName)
                });
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                    IdentityResult result = IdentityResult.Success;

                    var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
                    var roleName = await _userManager.GetRolesAsync(user);
                foreach (var item in roleName)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, item);
                    if (result != IdentityResult.Success)
                        break;
                }
                if (result == IdentityResult.Success)
                {
                    await _userManager.DeleteAsync(user);
                }
                    return Json(true);
                
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
    }
}
