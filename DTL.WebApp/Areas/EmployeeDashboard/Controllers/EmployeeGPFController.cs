using DTL.Model.Models.GPF;
using DTL.WebApp.Areas.EmployeeDashboard.Data;
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
    public class EmployeeGPFController : Controller
    {
        private readonly IGPF _empInfo;
        private readonly UserManager<ApplicationUser> _userManager;
       

        public EmployeeGPFController(IGPF empInfo, UserManager<ApplicationUser> userManager)
        {
            _empInfo = empInfo;
            _userManager = userManager;
        }
        public IActionResult GetGPF_Data()
        {
            return View();
        }
        private async Task<ApplicationUser> GetUserDetailFromAspNetUser()
        {
            var _userdetails = await _userManager.FindByNameAsync(User.Identity.Name);
            return _userdetails;
        }

        public IActionResult GetEmployeeData(string ppo_no, string orgCode = "all", string retirementStatus = "all")
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            var data = _empInfo.Get(_detail.UserName, orgCode, retirementStatus);
            //IEnumerable<GPFEmployeeInfoModel> data = _empInfo.Get(ppo_no, orgCode, retirementStatus);
            return Ok(data);
        }

        public IActionResult ViewGPFEmployeeProfile()
        {
            return View();
        }

        public IActionResult GetEmployeeDetails(string externalCode, string ppo)
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            GPFEmployeeDetail data = _empInfo.GetEmployee(externalCode, _detail.UserName);
            return Ok(data);
        }
    }
}
