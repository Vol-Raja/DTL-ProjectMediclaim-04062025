using DTL.Business.Mediclaim.Detail.NonCashless;
using DTL.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DTL.WebApp.Areas.Mediclaim.Controllers.Detail
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class NonCashlessController : Controller
    {
        private readonly INonCashlessDetail _nonCashless; 
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;
        public NonCashlessController(INonCashlessDetail nonCashless, IAssignPermission assignPermission)
        {
            _nonCashless = nonCashless;
            _assignPermission = assignPermission;
        }

        public IActionResult Index()
        {
            var nonCashlessModel = _nonCashless.GetNonCashlessListByCreatedBy(User.Identity.Name); 
            GetPermissions();
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;
            return View(nonCashlessModel);
        }

        [Route("Mediclaim/NonCashless/PrintPreview/{claimId:int}")]
        public IActionResult PrintPreview([FromRoute]int claimId)
        {
            var nonCashlessModel = _nonCashless.GetNonCashlessByClaimId(claimId);
            return View(nonCashlessModel);
        }

        [HttpDelete]
        [Route("/Mediclaim/NonCashless/Delete/{claimId}")]
        public IActionResult DeleteClaim([FromRoute] int claimId)
        {
            var count = _nonCashless.UpdateCashlessDelete(claimId, true, User.Identity.Name);
            if (count > 0)
            {
                return Ok("Success");
            }
            else {
                return Ok("Failed");
            }
        }

        private void GetPermissions()
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "Mediclaim", "Raise NonCashless").FirstOrDefault();
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
