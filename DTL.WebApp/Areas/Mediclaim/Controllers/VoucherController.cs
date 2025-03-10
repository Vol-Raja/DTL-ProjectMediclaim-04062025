using ClosedXML.Excel;
using DTL.Business.Mediclaim.Detail.Cashless;
using DTL.Business.Mediclaim.Detail.NonCashless;
using DTL.Business.Mediclaim.Voucher;
using DTL.Business.UserManagement;
using DTL.Model.Models.Mediclaim;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class VoucherController : Controller
    {
        private readonly IVoucher _voucher;
        private readonly INonCashlessDetail _nonCashlessDetail;
        private readonly ICashlessDetail _cashlessDetail;
        private readonly IAssignPermission _assignPermission;
        private readonly UserManager<ApplicationUser> _userManager;
        private bool _create, _edit, _view, _delete = false;

        public VoucherController(IVoucher voucher,
            IAssignPermission assignPermission,
            INonCashlessDetail nonCashlessDetail,
            ICashlessDetail cashlessDetail,
            UserManager<ApplicationUser> userManager)
        {
            _voucher = voucher;
            _nonCashlessDetail = nonCashlessDetail;
            _cashlessDetail = cashlessDetail;
            _assignPermission = assignPermission;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Mediclaim/{claimType}/Vouchers")]
        public IActionResult Index([FromRoute] string claimType)
        {
            ViewBag.ClaimType = claimType.ToLower();
            return View();
        }


        #region Common Methods

        [Route("Mediclaim/Voucher/AddNewVoucher/{claimType}")]
        public IActionResult AddNewVoucher([FromRoute] string claimType)
        {
            ViewBag.ClaimType = claimType;
            return View();
        }

        /// <summary>
        /// Create new voucher in system
        /// </summary>
        /// <param name="mediclaimVoucherModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Mediclaim/Voucher/SaveMediclaimVoucher")]
        public IActionResult SaveMediclaimVoucher([FromBody] MediclaimVoucherModel mediclaimVoucherModel)
        {
            int _voucherId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    mediclaimVoucherModel.CreatedBy = User.Identity.Name;
                    _voucherId = _voucher.SaveMediclaimVoucher(mediclaimVoucherModel);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(_voucherId);
        }


        [Route("Mediclaim/Voucher/{claimtype}/{voucherNo?}/{claimId?}/{pageNumber?}")]
        [HttpGet]
        public IActionResult GetMediclaimVouchersByParam([FromRoute] string claimtype, [FromRoute] int? voucherNo, [FromRoute] int? claimId = null, [FromRoute] string pageNumber = null)
        {
            //1%2F1%2F0001
            voucherNo = voucherNo > 0 ? voucherNo : null;
            claimId = claimId > 0 ? claimId : null;
            pageNumber = pageNumber!="NA" ? pageNumber : null;
            var _voucherList = _voucher.GetMediclaimVouchersByParam(voucherNo, null, null, pageNumber, claimId, claimtype);
            return Json(_voucherList);
        }


        [Route("Mediclaim/Voucher/PrintPreview/{voucherId}")]
        [Route("Mediclaim/Voucher/{claimType}/PrintPreview/{voucherId}")]        
        public IActionResult PrintPreview([FromRoute] string claimType, [FromRoute] int voucherId)
        {
            ViewBag.ClaimType = claimType;
            ViewBag.Designation = GetUserDetailFromAspNetUser().Result.Designation;            
            var _voucherModel = _voucher.GetMediclaimVouchersByParam(voucherId, null, null, null, null, null).FirstOrDefault();

            var _model = new ClaimAndVoucherDetailModel() {   
                Voucher = _voucherModel
            };

            if (!string.IsNullOrEmpty(claimType) && claimType.ToLower() == "noncashless") {

                var _nonCashlessModel = _nonCashlessDetail.GetNonCashlessByClaimId(_voucherModel.ClaimId);
                _model.NonCashless = _nonCashlessModel;
            }
            else {
                var _cashlessModel = _cashlessDetail.GetCashlessById(_voucherModel.ClaimId);
                _model.Cashless = _cashlessModel;
            }
            return View(_model);

        }


        [Route("Mediclaim/Voucher/Edit/{voucherId}")]
        [Route("Mediclaim/Voucher/Edit/{claimType?}/{voucherId}")]
        public IActionResult EditVoucher([FromRoute] int voucherId, [FromRoute] string claimType)
        {
            ViewBag.VoucherId = voucherId;
            if (!string.IsNullOrEmpty(claimType) && claimType.ToLower() == "noncashless")
            {
                return View("NonCashlessVoucherForm");
            }
            else
            {
                return View();
            }
        }

        [Route("Mediclaim/Voucher/Edit")]
        [HttpPost]
        public IActionResult EditVoucher([FromBody] MediclaimVoucherModel mediclaimVoucherModel)
        {
            int _rowCount = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    mediclaimVoucherModel.ModifideBy = User.Identity.Name;
                    _rowCount = _voucher.UpdateMediclaimVoucher(mediclaimVoucherModel);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(_rowCount);
        }


        [Route("Mediclaim/Voucher/VoucherDetail/{voucherId}")]
        public IActionResult GetVoucherDetailForEdit([FromRoute] int voucherId)
        {
            var _voucherModel = _voucher.GetMediclaimVouchersByParam(voucherId, null, null, null, null, null);
            return Json(_voucherModel.FirstOrDefault());
        }

        [Route("/Mediclaim/Voucher/Delete/{voucherId}")]
        public IActionResult UpdateVoucherIsDelete([FromRoute] int voucherId)
        {
            var returnValue = 0;
            MediclaimVoucherModel voucherModel = new MediclaimVoucherModel();
            try
            {
                if (ModelState.IsValid)
                {
                    voucherModel.VoucherId = voucherId;
                    voucherModel.ModifideBy = User.Identity.Name;
                    voucherModel.IsDelete = true;
                    returnValue = _voucher.UpdateVoucherDelete(voucherModel);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Json(returnValue > 0 ? "Success" : "Fail");
            //return Redirect("/Mediclaim/Voucher");
        }
 

        [Route("Mediclaim/Voucher/Approve/{voucherId}")]
        [HttpPost]
        public IActionResult ApproveVoucher([FromRoute] int voucherId)
        {
            var _modifiedBy = User.Identity.Name;
            _voucher.ApproveVoucher(voucherId, _modifiedBy, 2);
            return Ok();
        }

        [Route("Mediclaim/Voucher/Detail/{claimType}/{claimNumber}")]
        [HttpGet]
        public IActionResult GetVoucherAndClaimDetail([FromRoute] string claimType, [FromRoute] int claimNumber)
        {
            if (claimType.ToLower() == "cashless")
            {
                var _claimDetail = _voucher.GetCashlessDetailByClaimId(claimNumber);
                return Ok(_claimDetail);
            }
            else if (claimType.ToLower() == "noncashless")
            {
                var _claimDetail = _voucher.GetNonCashlessDetailByClaimId(claimNumber);
                return Ok(_claimDetail);
            }
            else
            {
                return Ok();
            }
        }

        #endregion


        #region Cashless Methods

        [Route("Mediclaim/Voucher/CashlessVoucherList")]
        public IActionResult CashlessVoucherList()
        {
            ViewBag.ClaimType = "cashless";
            return View();
        }

        #endregion


        #region NonCashless Methods

        [Route("Mediclaim/Voucher/NonCashlessVoucherList")]
        public IActionResult NonCashlessVoucherList()
        {
            ViewBag.ClaimType = "noncashless";
            return View();
        }


        #endregion


        public FileResult ExportToExcel()
        {
            IEnumerable<MediclaimVoucherModel> _voucherList;
            DataTable dt = new DataTable("Voucher List");

            _voucherList = _voucher.GetMediclaimVouchersByCreatedBy(User.Identity.Name);
            dt.Load((IDataReader)_voucherList);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application / vnd.openxmlformats - officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }

         
        private async Task<ApplicationUser> GetUserDetailFromAspNetUser()
        {
            var _userdetails = await _userManager.FindByNameAsync(User.Identity.Name);
            return _userdetails;
        }

        public ActionResult NonCashlessVoucherForm()
        {
            return View();
        }
        public ActionResult NonCashlessVoucherView()
        {
            return View();
        }
        public ActionResult NonCaslessVoucherList()
        {
            return View();
        }
    }
}
