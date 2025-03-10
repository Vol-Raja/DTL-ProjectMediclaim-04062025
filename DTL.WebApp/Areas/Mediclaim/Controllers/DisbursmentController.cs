using DTL.Business.GPF.Processing;
using DTL.Business.Mediclaim.Detail.Cashless;
using DTL.Business.Mediclaim.Detail.NonCashless;
using DTL.Business.Mediclaim.Disbursement;
using DTL.Business.Mediclaim.Voucher;
using DTL.Model.Models.Mediclaim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    public class DisbursmentController : Controller
    {
        private readonly IDisbursment _disbursment;        
        private readonly IVoucher _voucher;
        private readonly INonCashlessDetail _nonCashlessDetail;
        private readonly ICashlessDetail _cashlessDetail;
        public DisbursmentController(IDisbursment disbursment, 
            IVoucher voucher,
            INonCashlessDetail nonCashlessDetail,
            ICashlessDetail cashlessDetail)
        {
            _disbursment = disbursment;
            _voucher = voucher;
            _nonCashlessDetail = nonCashlessDetail;
            _cashlessDetail = cashlessDetail;
        }

        // GET: DisbursmentController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProcessingDashboard() {
            return View();
        }

        public ActionResult SbiIndex()
        {
            return View();
        }
        public ActionResult FullFinalSettlementIndex()
        {
            return View();
        }
        public ActionResult RequestForReleaseFullFinalIndex()
        {
            return View();
        }
        public ActionResult PnbIndex1()
        {
            return View();
        }
        public ActionResult viewPnbIndex()
        {
            return View("PnbIndex");
        }
        public ActionResult Cheque()
        {
            return View();
        }

        #region Cashless
        public ActionResult CashlessProcessingList()
        {
            var cashless = _disbursment.GetDisbursedCashlessClaims(true, null);
            return View(cashless);
        }

        public ActionResult CashLessAppovalReject()
        {
            return View();
        }

        [HttpPost]
        [Route("Mediclaim/Disbusment/Cashless/Pay/{claimId}")]
        public ActionResult PayCashlessClaim([FromRoute] int claimId) 
        {           
            try
            {                
                var returnValue = _disbursment.PayCashlessClaim(claimId, true, User.Identity.Name);
                if (returnValue > 0)
                {
                    return Ok("Success");
                }
                else
                {
                    return Ok("Fail");
                }

            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
        #endregion

        #region NonCashless
        public ActionResult NonCashlessProcessingList()
        {
            var noncashless = _disbursment.GetDisbursedNonCashlessClaims(true, null);
            return View(noncashless);
        }

        public ActionResult NonCashlessClaimAndVoucherView()
        {
            return View();
        }

        [HttpPost]
        [Route("Mediclaim/Disbusment/Noncashless/Pay/{claimId}")]
        public ActionResult PayNonCashlessClaim([FromRoute] int claimId)
        {
            try
            {
                var returnValue = _disbursment.PayNonCashlessClaim(claimId, true, User.Identity.Name);
                if (returnValue > 0)
                {
                    return Ok("Success");
                }
                else
                {
                    return Ok("Fail");
                }

            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        #endregion

        [HttpGet]
        [Route("Mediclaim/Disbursment/{claimType}/View/{claimId}")]
        public IActionResult ClaimAndVoucherView(string claimType, int claimId)
        { 
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

        private ClaimAndVoucherDetailModel GetClaimAndVoucherDetail(string claimType, int claimId)
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
    }
}
