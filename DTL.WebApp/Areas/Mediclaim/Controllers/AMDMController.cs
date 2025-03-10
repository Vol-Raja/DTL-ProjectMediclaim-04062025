using DTL.Business.Mediclaim.Detail.Cashless;
using DTL.Business.Mediclaim.Detail.NonCashless;
using DTL.Business.Mediclaim.Processing;
using DTL.Business.Mediclaim.Voucher;
using DTL.Model.Models.Mediclaim;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    public class AMDMController : Controller
    {
        // GET: AMDMController

        private readonly IProcessing _processing;
        private readonly IVoucher _voucher;
        private readonly INonCashlessDetail _nonCashlessDetail;
        private readonly ICashlessDetail _cashlessDetail;
        private readonly UserManager<ApplicationUser> _userManager;
        public AMDMController(IProcessing processing,
             UserManager<ApplicationUser> userManager,
             IVoucher voucher,
             INonCashlessDetail nonCashlessDetail,
             ICashlessDetail cashlessDetail)

        {
            _processing = processing;
            _userManager = userManager;
            _voucher = voucher;
            _nonCashlessDetail = nonCashlessDetail;
            _cashlessDetail = cashlessDetail;
        }

        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult ClaimForwardDashboard()
        {
            return View();
        }

        public ActionResult ProcessingDashbord()
        {
            return View();
        }

        #region Cashless
        public ActionResult ClaimForwordCashless()
        {
            var model = _processing.GetCashlessclaimByParam(null, null, 2, null, null, null, "AM_DM").Where(x => x.Disbursed == true);
            return View(model);
        }

        [HttpGet]
        [Route("Mediclaim/AMDM/Cashless")]
        public ActionResult CashlessProcessingList()
        {
            var model = _processing.GetCashlessclaimByParam(null, null, 2, null, null, null, "AM_DM").Where(x => x.Disbursed == false);
            return View(model);
        }

        #endregion

        #region Noncashless
        public ActionResult ClaimForwordNoncashless()
        {
            var model = _processing.GetNonCashlessclaimByParam(null, null, 2, null, null, null, "AM_DM").Where(x => x.Disbursed == true);
            return View(model);
        }
        [HttpGet]
        [Route("Mediclaim/AMDM/NonCashless")]
        public ActionResult NonCashlessProcessingList()
        {
            var model = _processing.GetNonCashlessclaimByParam(null, null, 2, null, null, null, "AM_DM").Where(x => x.Disbursed == false);
            return View(model);
        }
        #endregion

        #region Common Methods
        [HttpGet]
        [Route("Mediclaim/AMDM/{claimType}/View/{claimId}")]
        public IActionResult ClaimAndVoucherView(string claimType, int claimId)
        {
            var designation = GetUserDetailFromAspNetUser().Result.Designation;
            ViewBag.Designation = designation;
            if (claimType.ToLower() == "noncashless")
            {
                var _model = GetClaimAndVoucherDetail(claimType, claimId);
                return View("NonCashlessClaimAndVoucherView", _model);
            }
            else
            {
                var _model = GetClaimAndVoucherDetail(claimType, claimId);
                return View("CashlessClaimAndVoucherView", _model);
            }
        }

        public ClaimAndVoucherDetailModel GetClaimAndVoucherDetail(string claimType, int claimId)
        {

            var _voucherModel = _voucher.GetMediclaimVouchersByParam(null, null, null, null, claimId, claimType).FirstOrDefault();

            var _model = new ClaimAndVoucherDetailModel()
            {
                Voucher = _voucherModel
            };

            if (!string.IsNullOrEmpty(claimType) && claimType.ToLower() == "noncashless")
            {
                var _nonCashlessModel = _nonCashlessDetail.GetNonCashlessByClaimId(claimId);
                _model.NonCashless = _nonCashlessModel;
            }
            else
            {
                var _cashlessModel = _cashlessDetail.GetCashlessById(claimId);
                _model.Cashless = _cashlessModel;
            }

            return _model;
        }

        [HttpPost]
        [Route("Medicliam/{claimType}/Disburse")]
        public ActionResult DisbursedFlag([FromRoute] string claimType, [FromBody] List<int> claimIds)
        {
            try
            {
                if (!claimIds.Any()) { return BadRequest("No claim recieved for disbursement"); }

                if (claimType.ToLower() == "noncashless")
                {
                    foreach (var claimId in claimIds)
                    {
                        _processing.DisburseNonCashlessClaim(claimId, User.Identity.Name);
                    }
                }

                if (claimType.ToLower() == "cashless")
                {
                    foreach (var claimId in claimIds)
                    {
                        _processing.DisburseCashlessClaim(claimId, User.Identity.Name);
                    }
                }


                return Ok("Claim pushed for Disbursment");
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
        private async Task<ApplicationUser> GetUserDetailFromAspNetUser()
        {
            var _userdetails = await _userManager.FindByNameAsync(User.Identity.Name);
            return _userdetails;
        }
        #endregion

        [HttpPost]
        [Route("/Mediclaim/AMDM/{claimType}/{claimId}/{forwardTo}")]
        public IActionResult ChangeApprover([FromRoute] string claimType, [FromRoute] int claimId, [FromRoute] string forwardTo)
        {
            try
            {
                _processing.UpdateForwardTo(forwardTo, User.Identity.Name, claimId, claimType);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }
    }
}
