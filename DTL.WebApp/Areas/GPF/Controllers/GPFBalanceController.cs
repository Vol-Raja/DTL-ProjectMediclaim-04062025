using DTL.Business.Common;
using DTL.Business.GPF.Processing;
using DTL.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    //[Area("GPF")]
    [Authorize(Roles = "DVB Pension Trust,Admin")]
    public class GPFBalanceController : Controller
    {
        private readonly IProcessing _processing;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;

        public GPFBalanceController(IProcessing processing, IAssignPermission assignPermission)
        {
            _processing = processing;
            _assignPermission = assignPermission;
        }

        public IActionResult Index()
        {
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;
            return View();
        }

        /// <summary>
        /// Used to search gpfbalance by parameters
        /// </summary>
        /// <param name="employeeName"></param>
        /// <param name="employer"></param>
        /// <param name="employeeId"></param>
        /// <returns>Processing Detail List</returns>
        [Route("GPF/GPFBalance/GetGPFBalance/{employeeName?}/{employer?}/{employeeId?}")]
        public IActionResult GetGPFBalance([FromRoute] string employeeName, [FromRoute] string employer = null, [FromRoute] string employeeId = null)
        {
            employeeName = employeeName != "NA" ? employeeName : null;
            employer = employer != "NA" ? employer : null;
            employeeId = employeeId != "0" ? employeeId : null;

            var gpfProcessingModel = _processing.GetDetailByParam(employer, null, null, employeeId, employeeName);
            return Json(gpfProcessingModel);
        }

        private void GetPermissions()
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "GPF", "GPF Balance").FirstOrDefault();
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
