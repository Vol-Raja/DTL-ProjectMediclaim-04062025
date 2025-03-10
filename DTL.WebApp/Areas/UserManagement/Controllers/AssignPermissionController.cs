using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTL.Business.UserManagement;
using DTL.Model.Models.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DTL.WebApp.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    [Authorize(Roles ="SuperAdmin")]
    public class AssignPermissionController : Controller
    {

        private readonly IModule _module;
        private readonly ISubModule _submodule;
        private readonly IAssignPermission _assignPermission;

        public AssignPermissionController(IModule module, ISubModule submodule, IAssignPermission assignPermission)
        {
            _module = module;
            _submodule = submodule;
            _assignPermission = assignPermission;
        }

        [Route("/UserManagement/AssignPermission/{userType}/{userId}")]
        public IActionResult Index([FromRoute] string userType, [FromRoute] string userId)
        {
            ViewBag.UserId = userId;
            ViewBag.UserType = userType;
            ViewBag.UserLabel = userType == "HospitalUser" ? "Hospital" : "DVB";
            var moduleList = _module.GetModuleByParam(null, null);
            var removelist = new List<string>() { "Admin User", "Roles", "Modules", "Sub Modules", "DVB User", "Hospital User" };
            ViewBag.ModuleList = moduleList.Where(x => !removelist.Contains(x.ModuleName));
            return View();
        }

        [HttpGet]
        [Route("/UserManagement/LoadAssignPermission/{useremail}")]
        public IActionResult LoadAssignPermission([FromRoute]string useremail)
        {
            var permissions = _assignPermission.GetAssignPermissionByParam(useremail, null,null);
            return Json(permissions);
        }

        [HttpPost]
        [Route("/UserManagement/AssignPermission/")]
        public IActionResult AssigPermission([FromBody]AssignPermissionModel assignPermissionModel)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _assignPermission.GetAssignPermissionByParam(assignPermissionModel.UserId,
                                            assignPermissionModel.ModuleName,
                                            assignPermissionModel.SubModuleName).ToList();

                    if (data.Count() == 0)
                    {
                        assignPermissionModel.CreatedBy = User.Identity.Name;
                        id = _assignPermission.AddAssignPermission(assignPermissionModel);
                    }
                    else
                    {
                        var permissionId = data.FirstOrDefault().AssignPermissionId;
                        assignPermissionModel.ModifideBy = User.Identity.Name;
                        assignPermissionModel.AssignPermissionId = permissionId;
                        id = _assignPermission.UpdateAssignPermission(assignPermissionModel);
                    }
                }
                else
                {
                    IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(allErrors);
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(id);
        }

        [HttpDelete]
        [Route("/UserManagement/AssignPermission/Delete/{assignPermissionId}")]
        public IActionResult DeleteAssigPermission([FromRoute] int assignPermissionId)
        {
            _assignPermission.DeleteAssignPermission(assignPermissionId);
            return Ok();
        }

        [HttpGet]
        [Route("/UserManagement/AssignPermission/Submodule/{modulename}")]
        public IActionResult GetSubModule([FromRoute] string modulename)
        {
            var submoduleList = _submodule.GetSubModuleByParam(null, modulename, null);
            return Ok(submoduleList);
        }
    }
}
