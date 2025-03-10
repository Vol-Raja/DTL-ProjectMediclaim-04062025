using DTL.Business.Mediclaim.Detail.Cashless;
using DTL.Business.Mediclaim.Detail.NonCashless;
using DTL.Business.Mediclaim.Disbursement;
using DTL.Business.Mediclaim.Voucher;
using DTL.Model.Models.Mediclaim;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.EmployeeDashboard.Controllers
{
    [Area("EmployeeDashboard")]
    public class EmployeeDisbursementController : Controller
    {
        private readonly IDisbursment _disbursment;
        private readonly IVoucher _voucher;
        private readonly INonCashlessDetail _nonCashlessDetail;
        private readonly ICashlessDetail _cashlessDetail;
        private readonly UserManager<ApplicationUser> _userManager;
        public EmployeeDisbursementController(IDisbursment disbursment,
            IVoucher voucher,
            INonCashlessDetail nonCashlessDetail,
            ICashlessDetail cashlessDetail,
            UserManager<ApplicationUser> userManager)
           
        {
            _disbursment = disbursment;
            _voucher = voucher;
            _nonCashlessDetail = nonCashlessDetail;
            _cashlessDetail = cashlessDetail;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult CashlessDisbursementList()
        {
            var cashless = _disbursment.GetDisbursedCashlessClaims(true, null);
            return View(cashless);
        }
        // add by nirbhay 27/02/2025
        private async Task<ApplicationUser> GetUserDetailFromAspNetUser()
        {
            var _userdetails = await _userManager.FindByNameAsync(User.Identity.Name);
            return _userdetails;
        }
       
        public IActionResult GetDisbursmentcashlessByPPO(string ppo)
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            var data = _disbursment.GetDisbusCashlessByPPO(_detail.UserName,true,true);
            return Json(data);
        }
        //End

        private ClaimAndVoucherDetailModel GetClaimAndVoucherDetail(string claimType, int claimId)
        {
            var _model = new ClaimAndVoucherDetailModel();

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

        [HttpGet]
        [Route("EmployeeDashboard/EmployeeDisbursement/{claimType}/EmployeeCashlessDisbursment/{claimId:int}")]
        public IActionResult EmployeeCashlessDisbursment(string claimType, int claimId)
        {
            if (claimType.ToLower() == "noncashless")
            {
                var _model = GetClaimAndVoucherDetail(claimType, claimId);
                return View("NonCashlessClaimAndVoucherView", _model);
            }
            else
            {
                var _model = GetClaimAndVoucherDetail(claimType, claimId);
                return View("CashlessEmployeeDisbursmentView", _model);
            }
        }
    }
}
