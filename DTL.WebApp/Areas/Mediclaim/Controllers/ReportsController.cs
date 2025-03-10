using DTL.Business.Mediclaim.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class ReportsController : Controller
    {
        private readonly IReport _report;
        public ReportsController(IReport report)
        {
            _report = report;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProcessedClaims()
        {
            return View();
        }
        public IActionResult NonCashlessProcessedClaims()
        {
            return View();
        }
        public IActionResult NonCashlessPendingClaims()
        {
            return View();
        }
        public IActionResult PendingClaims()
        {
            return View();
        }

        public IActionResult NonCashlessRejectedClaims()
        {
            return View();
        }
        public IActionResult RejectedClaims()
        {
            return View();
        }

        [Route("/Mediclaim/Report/{claimType}/{claimStatus}")]
        [Route("/Mediclaim/Report/{claimType}/{claimStatus}/{claimId}")]
        [Route("/Mediclaim/Report/{claimType}/{claimStatus}/{fromDate}/{toDate}")]
        public IActionResult GetMediclaimReportByParam([FromRoute] string claimType, [FromRoute] string claimStatus, [FromRoute] int? claimId, [FromRoute]DateTime ? fromDate, [FromRoute]DateTime? toDate)
        {
            int _claimStatus = 1;
            if (claimStatus != null)
            {
                switch (claimStatus.ToLower())
                {
                    case "pending": _claimStatus = 1; break;
                    case "processed": _claimStatus = 2; break;
                    case "rejected": _claimStatus = 3;break;
                    default: _claimStatus = 1; break;
                }
            }

            var _mediclaimReportData = _report.GetMedilaimsForReportByParam(claimId, claimType, fromDate, toDate, _claimStatus);
            return Json(_mediclaimReportData);
        }


        [Route("Mediclaim/Report/CashlessPreview/{claimId:int}")]
        public IActionResult CashlessPreview([FromRoute] int claimId)
        {
            var cashlessModel = _report.GetCashlessById(claimId);
            return View(cashlessModel);
        }

        [Route("Mediclaim/Report/NonCashlessPreview/{claimId:int}")]
        public IActionResult NonCashlessPreview([FromRoute] int claimId)
        {
            var nonCashlessModel = _report.GetNonCashlessByClaimId(claimId);
            return View(nonCashlessModel);
        }
    }
}
