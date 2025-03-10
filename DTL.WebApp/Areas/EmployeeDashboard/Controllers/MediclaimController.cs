using DTL.Business.Mediclaim.Detail.Cashless;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.EmployeeDashboard.Controllers
{
    [Area("EmployeeDashboard")]
    public class MediclaimController : Controller
    {
        private readonly ICashlessDetail _cashlessDetail;

        public MediclaimController(ICashlessDetail cashlessDetail)
        {
            _cashlessDetail = cashlessDetail;
        }
        public IActionResult CashlessMediclaim()
        {
            var username = User.Identity;
            return View();
        }

        [Route("EmployeeDashboard/Mediclaim/ViewCashlessMediclaim/{claimId:int}")]
        public IActionResult ViewCashlessMediclaim([FromRoute] int claimId)
        {
            var cashlessModel = _cashlessDetail.GetCashlessById(claimId);
            return View(cashlessModel);
            //return View();
        }
    }
}
