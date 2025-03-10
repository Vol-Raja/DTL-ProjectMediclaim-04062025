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
    [Authorize(Roles = "Master")]
    public class SubModuleController : Controller
    {

        private readonly IModule _module;
        private readonly ISubModule _submodule;
        public SubModuleController(ISubModule submodule, IModule module)
        {
            _submodule = submodule;
            _module = module;
        }

        public IActionResult SubModuleList()
        {
            var moduleList = _submodule.GetSubModuleByParam(null, null, null);
            return View(moduleList);
        }

        [HttpGet]
        [Route("/UserManagement/SubModule/SubModuleForm/{mode?}/{id?}")]
        public IActionResult SubModuleForm([FromRoute] string mode = "create", [FromRoute] int id = 0)
        {
            ViewBag.Mode = mode;
            ViewBag.SubModuleId = id;
            ViewBag.ModuleList = _module.GetModuleByParam(null, null);
            return View();
        }

        [HttpPost]
        [Route("/UserManagement/SubModule/Create")]
        public IActionResult CreateSubModule([FromBody] SubModuleModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    model.ID = Guid.NewGuid();
                    model.CreatedBy = User.Identity.Name;
                    id = _submodule.AddSubModule(model);
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

        [HttpPost]
        [Route("/UserManagement/SubModule/Update")]
        public IActionResult UpdateSubModule([FromBody] SubModuleModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    model.ModifideBy = User.Identity.Name;
                    id = _submodule.UpdateSubModule(model);
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
        [Route("/UserManagement/SubModule/Delete/{id}")]
        public IActionResult DeleteSubModule([FromRoute] int Id)
        {
            try
            {
                _submodule.DeleteSubModule(Id);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }

        [HttpGet]
        [Route("/UserManagement/SubModule/GetSubModuleById/{id}")]
        public IActionResult GetSubModuleById([FromRoute] int Id)
        {
            var submodule = _submodule.GetSubModuleByParam(Id, null, null);
            return Json(submodule.FirstOrDefault());
        }

        [HttpGet]
        [Route("/UserManagement/SubModule/Archive")]
        public IActionResult SubModuleArchive()
        {
            var module = _submodule.GetArchiveSubModule();
            return View(module);
        }
    }
}
