using DTL.Business.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Views.Shared.Components.MenuComponent
{
    public class MenuViewComponent :ViewComponent
    {
        private static IAuthService _authService;
        private readonly UserManager<IdentityUser> _userManager;

        public MenuViewComponent(IAuthService authService,
            UserManager<IdentityUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = await _authService.GetRolePageAccessByIdAsync(user.Id);

            return View(model);
        }
    }
}
