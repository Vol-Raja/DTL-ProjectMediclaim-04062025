using DTL.Business.Common;
using DTL.Business.Disbursement;
using DTL.Model.CommonModels;
using DTL.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{
    [Authorize]
    public class AgeQuantamPayController : Controller
    {
        private static IAddQtmPayment _addQtmPaymentService;

        public AgeQuantamPayController(IAddQtmPayment addQtmPaymentService)
        {
            _addQtmPaymentService = addQtmPaymentService;
        }
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
