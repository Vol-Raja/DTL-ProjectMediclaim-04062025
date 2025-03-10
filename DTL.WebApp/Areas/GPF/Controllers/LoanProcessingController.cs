using DTL.Business.Common;
using DTL.Business.GPF.LoanProcessing;
using DTL.Business.UserManagement;
using DTL.Model.Models.GPF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    [Area("GPF")]
    [Authorize(Roles = "DVB Pension Trust,Admin")]
    public class LoanProcessingController : Controller
    {
        private readonly ILoanProcessing _loanProcessing;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;
        public LoanProcessingController(ILoanProcessing loanProcessing, IAssignPermission assignPermission)
        {
            _loanProcessing = loanProcessing; 
            _assignPermission = assignPermission;          
        }

        /// <summary>
        /// Landing Page for Loan processing view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            GetPermisisions();
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;
            return View();
        }
        public IActionResult LoanAppovalList()
        {
          
            return View();
        }
        public IActionResult LoanAppovalView()
        {
            return View();
        }
        /// <summary>
        /// Gets the pending status record
        /// from GPF withdraw
        /// </summary>
        /// <returns>List</returns>
        [Route("/GPF/LoanProcessing/GetPendingGPFWithdrawals")]
        public IActionResult GetPendingWithdrawal()
        {
            var gpfWithdrawalList = _loanProcessing.GetGPFWithdrawalByParam(null, null, null, null, null, 1, null,null).ToList();
            return Json(gpfWithdrawalList);
        }

        /// <summary>
        /// Used to search gpf details by parameters
        /// </summary>
        /// <param name="processingDate"></param>
        /// <param name="employer"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Route("/GPF/LoanProcessing/GetGPFWithdrawal/{processingDate?}/{employer?}/{employeeId?}/{statusId?}/{applicationNumber?}")]
        public IActionResult GetGPFWithdrawalByParam([FromRoute] string processingDate, [FromRoute] string employer = null, [FromRoute] string employeeId = null, [FromRoute] int? statusId = null,[FromRoute] string applicationNumber = null)
        {
            employer = employer != "NA" ? employer : null;
            var month = !string.IsNullOrEmpty(processingDate) ? (int.TryParse(processingDate.Split('-')[0].Trim(), out var i) ? (int?)i : null) : null;
            var year = !string.IsNullOrEmpty(processingDate) ? (int.TryParse(processingDate.Split('-')[1].Trim(), out var j) ? (int?)j : null) : null;
            employeeId = employeeId != "0" ? employeeId : null;
            applicationNumber = applicationNumber != "NA" ? applicationNumber : null;
            statusId = statusId != 0 ? statusId : null;

            var gpfProcessingModel = _loanProcessing.GetGPFWithdrawalByParam(null,employer, month, year, employeeId,statusId, applicationNumber, null);
            return Json(gpfProcessingModel);
        }

            
        /// <summary>
        /// Loam Processing View
        /// </summary>
        /// <param name="withdrawId"></param>
        /// <returns>GPFWitdraw detail</returns>
        [Route("/GPF/LoanProcessing/View/{withdrawId?}")]
        public IActionResult LoanProcessingView([FromRoute] int? withdrawId)
        {
            var gpfWithdrawalModel = _loanProcessing.GetGPFWithdrawalByParam(withdrawId, null, null, null, null,null, null,null).FirstOrDefault();
            if (gpfWithdrawalModel != null) {
                gpfWithdrawalModel.GPFDocuments = _loanProcessing.GetDocumentsByParam(gpfWithdrawalModel.WithdrawId);
            }
            return View(gpfWithdrawalModel);
        }

        /// <summary>
        /// Updates the Application status 
        /// of GPF Withdraw table
        /// </summary>
        /// <param name="approveRejectModel"></param>
        /// <returns>Sucess/Failure</returns>
        [HttpPost]
        [Route("/GPF/LoanProcessing/ApproveReject")]
        public IActionResult ApproveRejectGPFWithdrawal([FromBody] ApproveRejectModel approveRejectModel)
        {
            var returnValue = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    approveRejectModel.ModifiedBy = User.Identity.Name;
                    returnValue = _loanProcessing.ApproveRejectGPFApplication(approveRejectModel);
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
        }

        /// <summary>
        /// Updates PhysicalSubmit flag  
        /// of GPF Withdraw table
        /// </summary>
        /// <param name="approveRejectModel"></param>
        /// <returns>Sucess/Failure</returns>
        [HttpPost]
        [Route("/GPF/LoanProcessing/PhysicalSubmit")]
        public IActionResult UpdateGPFPhysicalSubmit([FromBody] PhysicalSubmitModel physicalSubmitModel)
        {
            var returnValue = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    physicalSubmitModel.ModifiedBy = User.Identity.Name;
                    returnValue = _loanProcessing.UpdatePhysicalSubmit(physicalSubmitModel);
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
        }

        private void GetPermisisions()
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "GPF", "Loan Processing").FirstOrDefault();
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
