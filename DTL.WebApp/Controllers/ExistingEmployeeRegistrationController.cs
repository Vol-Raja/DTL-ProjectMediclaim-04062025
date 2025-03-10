using DTL.Business.Common;
using DTL.Business.EmployeeRegistration;
using DTL.Business.Logging;
using DTL.Business.UserManagement;
using DTL.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{
    [Authorize]
    public class ExistingEmployeeRegistrationController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static IAddEmployee _addEmployeeService;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;
        private readonly ILogging _logging;
        public ExistingEmployeeRegistrationController(IAddEmployee addEmployeeService, IAssignPermission assignPermission, ILogging logging)
        {
            _addEmployeeService = addEmployeeService;
            _assignPermission = assignPermission;
            _logging = logging;
        }

        public async Task<IActionResult> Index()
            {
            try
            {
                var roles = (((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)).FirstOrDefault();
                ViewBag.Roles = roles;
                
                GetPermissions("Existing Employee Registration", "NA");
                ViewBag.Create = _create;
                ViewBag.View = _view;
                ViewBag.Edit = _edit;
                ViewBag.Delete = _delete;
                var data = await _addEmployeeService.GetAllEmployeeAsync(roles, true);
                return View(data);
            }
            catch (Exception ex)
            {
                log.Error("EmployeeRegistration Index", ex);
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Add Existing Employee";
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            ViewBag.Mode = "Add";
            return View("ExistingEmployeeProfile", new EmployeeProfileModel());
        }
       
        public IActionResult EditEmployee(Guid employeeId)
        {
            var data = _addEmployeeService.GetEmployeeById(employeeId);
            ViewData["Title"] = "Edit Employee Profile";
            var commonJsonData = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = commonJsonData.DTLOfficeList;
            var tempMobile = (string.IsNullOrEmpty(data.Mobile) ? null : data.Mobile.Split("-"));
            data.MobileCountryCode = (tempMobile != null ? tempMobile[0] : null);
            data.Mobile = (tempMobile != null ? tempMobile[1] : null);
            var tempPhone = (string.IsNullOrEmpty(data.Phone) ? null : data.Phone.Split("-"));
            data.PhoneCountryCode = (tempPhone != null ? tempPhone[0] : null);
            data.Phone = (tempPhone != null ? tempPhone[1] : null);
            ViewBag.Mode = "Edit";
            return View("ExistingEmployeeProfile", data);
        }

        public async Task<IActionResult> ArchiveEmployee()
        {
            try
            {
                var roles = (((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)).FirstOrDefault();
                ViewBag.Roles = roles;
                var data = await _addEmployeeService.GetAllArchiveEmployeeAsync(Guid.Empty);
                return View("ArchiveEmployee", data);
            }
            catch (Exception ex)
            {
                log.Error("ExixtingEmployeeRegistration ArchiveEmployee", ex);
                return View("Error");
            }
        }

        private void GetPermissions(string modulename, string submodulename)
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, modulename, submodulename).FirstOrDefault();
            if (permission != null)
            {
                _create = permission.Create;
                _edit = permission.Edit;
                _view = permission.View;
                _delete = permission.Delete;
            }
        }
    }
}
