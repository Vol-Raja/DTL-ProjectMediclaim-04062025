using DTL.Business.GPF.GeneratePayment;
using DTL.Business.UserManagement;
using DTL.Model.Models.GPF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    [Area("GPF")]
    [Authorize(Roles = "DVB Pension Trust,Admin")]
    public class GeneratePaymentController : Controller
    {
        private readonly IGeneratePayment _generatePayment;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false; 
        public GeneratePaymentController(IGeneratePayment generatePayment,IAssignPermission assignPermission)
        {
            _generatePayment = generatePayment;
            _assignPermission = assignPermission;
        }

        public IActionResult Index()
        {
            var generatePaymentModel = _generatePayment.GetGeneratePaymentByParam(null, null, null);
            GetPermissions();
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;
            return View(generatePaymentModel);
        }

        [HttpGet]
        [Route("/GPF/GeneratePayment/Form")]
        public IActionResult GeneratePaymentForm()
        {
            return View();
        }

        [HttpPost]
        [Route("/GPF/GeneratePayment/Create")]
        public IActionResult CreateGeneratePayment([FromBody] GPFGeneratePaymentModel model)
        {
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedBy = User.Identity.Name;
                    id = _generatePayment.SaveGeneratePayment(model);
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
        [Route("/GPF/GeneratePayment/Delete/{paymentId}")]
        public IActionResult DeleteGeneratePayment([FromRoute] int paymentId)
        {
            try
            {
                var modifiedBy = User.Identity.Name;
                _generatePayment.DeleteGeneratePayment(paymentId, modifiedBy);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }

        
        [HttpGet]
        [Route("/GPF/GeneratePayment/View/{paymentId}")]
        public IActionResult GeneratePaymentView([FromRoute] int paymentId)
        {
            var generatePaymentModel = _generatePayment.GetGeneratePaymentByParam(paymentId, null, null).FirstOrDefault();
            return View(generatePaymentModel);
        }

        [HttpGet]
        [Route("/GPF/GeneratePayment/Load/ApplicationDetail/{applicationId}")]
        public IActionResult GetGPFWithdrawalByParam([FromRoute] string applicationId)
        {
            var generatePaymentModel = _generatePayment.GetGPFWithdrawalByParam(applicationId).FirstOrDefault();
            return Ok(generatePaymentModel);
        }

        [HttpGet]
        [Route("/GPF/GeneratePayment/Archieve")]
        public IActionResult GeneratePaymentArchive()
        {
            var generatePaymentModel = _generatePayment.GetArchiveGeneratePaymentByParam(null, null, null);
            return View(generatePaymentModel);
        }

        private void GetPermissions()
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "GPF", "Generate Payment").FirstOrDefault();
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
