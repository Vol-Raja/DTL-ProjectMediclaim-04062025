using DTL.WebApp.Areas.EmployeeDashboard.Data;
using DTL.WebApp.Areas.EmployeeDashboard.Models;
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
    public class PPOController : Controller
    {
        private readonly IPPO iPPO;
        private readonly UserManager<ApplicationUser> _userManager;
        public PPOController(IPPO _ippo, UserManager<ApplicationUser> userManager)
        {
            iPPO = _ippo;
            _userManager = userManager;
        }
        private async Task<ApplicationUser> GetUserDetailFromAspNetUser()
        {
            var _userdetails = await _userManager.FindByNameAsync(User.Identity.Name);
            return _userdetails;
        }
        [HttpGet]
        public IActionResult GetPPO_Data(string ppo_no)
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            var AllPPO_data = iPPO.GetEmployeePPO(_detail.UserName);
            return View(AllPPO_data);
        }

        [Route("EmployeeDashboard/PPO/ViewEmployeeDetails/{ppo_no:int}")]
        public IActionResult ViewEmployeeDetails([FromRoute] string ppoemp)
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            var _claimDetail = iPPO.ViewEmployee(_detail.UserName);
            return View(_claimDetail);
          
        }

        
    }
}
