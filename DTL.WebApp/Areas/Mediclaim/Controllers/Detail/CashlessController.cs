using DTL.Business.Mediclaim.Detail.Cashless;
using DTL.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DTL.WebApp.Areas.Mediclaim.Controllers.Detail
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class CashlessController : Controller
    {
        private readonly ICashlessDetail _cashless;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;
        public CashlessController(ICashlessDetail cashless, IAssignPermission assignPermission)
        {
            _cashless = cashless;
            _assignPermission = assignPermission;
        }

        public IActionResult Index()
        {
            var cashlessModel = _cashless.GetCashlessListByCreatedBy(User.Identity.Name);
            GetPermissions();
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;
            return View(cashlessModel);
        }

        public IActionResult IndexCredit()
        {
            var cashlessModel = _cashless.GetCreditListByCreatedBy(User.Identity.Name);
            GetPermissions();
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;
            return View(cashlessModel);
        }

        [Route("Mediclaim/Cashless/PrintPreview/{claimId:int}")]
        public IActionResult PrintPreview([FromRoute] int claimId)
        {
            var cashlessModel = _cashless.GetCashlessById(claimId);
            return View(cashlessModel);
        }

        // add by rajan 05/04/25
        [Route("Mediclaim/Cashless/PrintPreviewCreditLetter/{SerialNo}")]
        public IActionResult PrintPreviewCreditLetter([FromRoute] string SerialNo)
        {
            var CashlessCredit = _cashless.GetMediclaimCreditLetter(SerialNo);
            return View(CashlessCredit);
        }
        private void GetPermissions()
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "Mediclaim", "Raise Cashless").FirstOrDefault();
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
