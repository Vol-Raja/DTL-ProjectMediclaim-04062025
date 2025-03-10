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
    public class ModuleController : Controller
    {
        private readonly IModule _module;
        public ModuleController(IModule module)
        {
            _module = module;
        }

        public IActionResult ModuleList()
        {
            var moduleList = _module.GetModuleByParam(null, null);
            return View(moduleList);
        }

        [HttpGet]
        [Route("/UserManagement/Module/ModuleForm/{mode?}/{id?}")]
        public IActionResult ModulesForm([FromRoute] string mode = "create", [FromRoute] int id = 0)
        {
            ViewBag.Mode = mode;
            ViewBag.ModuleId = id;
            return View();
        }

        [HttpPost]
        [Route("/UserManagement/Module/Create")]
        public IActionResult CreateModule([FromBody] ModuleModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    model.ID = Guid.NewGuid();
                    model.CreatedBy = User.Identity.Name;
                    id = _module.AddModule(model);
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
        [Route("/UserManagement/Module/Update")]
        public IActionResult UpdateModule([FromBody] ModuleModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    model.ModifideBy = User.Identity.Name;
                    id = _module.UpdateModule(model);
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
        [Route("/UserManagement/Module/Delete/{id}")]
        public IActionResult DeleteModule([FromRoute] int Id)
        {
            try
            {
                _module.DeleteModule(Id);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }

        [HttpGet]
        [Route("/UserManagement/Module/GetModuleById/{id}")]
        public IActionResult GetModuleById([FromRoute] int Id)
        {
            var module = _module.GetModuleByParam(Id, null);
            return Json(module.FirstOrDefault());
        }

        [HttpGet]
        [Route("/UserManagement/Module/Archive")]
        public IActionResult ModuleArchive()
        {
            var module = _module.GetArchiveModule();
            return View(module);
        }
    }
}
