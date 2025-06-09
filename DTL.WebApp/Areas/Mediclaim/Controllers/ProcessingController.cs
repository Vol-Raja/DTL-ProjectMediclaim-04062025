using ClosedXML.Excel;
using Dapper;
using DTL.Business.Common;
using DTL.Business.Mediclaim.Detail.Cashless;
using DTL.Business.Mediclaim.Detail.NonCashless;
using DTL.Business.Mediclaim.Processing;
using DTL.Business.Mediclaim.Voucher;
using DTL.Model.CommonModels;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using DTL.WebApp.Common.CommonClasses;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class ProcessingController : Controller
    {
        private static string connectionString;
        private readonly string _connectionString;
        private readonly IProcessing _processing;         
        private readonly IVoucher _voucher;
        private readonly INonCashlessDetail _nonCashlessDetail;
        private readonly ICashlessDetail _cashlessDetail;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProcessingController(IProcessing processing,
             UserManager<ApplicationUser> userManager,
             IVoucher voucher,
             INonCashlessDetail nonCashlessDetail,
             ICashlessDetail cashlessDetail, IConfiguration configuration)
        {
            _processing = processing;
            _userManager = userManager;
            _voucher = voucher;
            _nonCashlessDetail = nonCashlessDetail;
            _cashlessDetail = cashlessDetail;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OPDProcessingMediclaimList()
        {
            CommonModel populateList = ReadJson.LoadJson();

            ViewBag.DTLOfficeList = populateList.DTLOfficeList;

            var list = populateList.DTLOfficeList.Select(x =>
                       new SelectListItem
                       {
                           Value = x.DTLOffice,
                           Text = x.DTLOffice
                       });

            ViewBag.DTLOfficeList = list;
            var _detail = GetUserDetailFromAspNetUser().Result;
            ViewBag.Designation = _detail.Designation;
            return View("NonCashlessClaims");
        }
        public IActionResult AsoCashlessClaims()
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            ViewBag.Designation = _detail.Designation;
            return View("CashlessClaims");
        }
        public IActionResult AsoNonCashlessClaims()
        {
            CommonModel populateList = ReadJson.LoadJson();

            ViewBag.DTLOfficeList = populateList.DTLOfficeList;

            var list = populateList.DTLOfficeList.Select(x =>
                       new SelectListItem
                       {
                           Value = x.DTLOffice,
                           Text = x.DTLOffice
                       });

            ViewBag.DTLOfficeList = list;
            var _detail = GetUserDetailFromAspNetUser().Result;
            ViewBag.Designation = _detail.Designation;
            return View("NonCashlessClaims");
        }

        #region Cashless

        /// <summary>
        /// Landing page for Cashless processing
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("/Mediclaim/CMO/ProcessingList")]
        [Route("/Mediclaim/IPD/ProcessingList")]
        [Route("/Mediclaim/Processing/CashlessClaims")]
        public IActionResult CashlessClaims()
        {
            CommonModel populateList = ReadJson.LoadJson();

            ViewBag.DTLOfficeList = populateList.DTLOfficeList;

            var list = populateList.DTLOfficeList.Select(x =>
                       new SelectListItem
                       {
                           Value = x.DTLOffice,
                           Text = x.DTLOffice
                       });

            ViewBag.DTLOfficeList = list;
            var _detail = GetUserDetailFromAspNetUser().Result;
            ViewBag.Designation = _detail.Designation;
            return View();
        }

        //New changes by nirbhay
        public IActionResult CashlessCreditletter()
        {
            CommonModel populateList = ReadJson.LoadJson();

            ViewBag.DTLOfficeList = populateList.DTLOfficeList;

            var list = populateList.DTLOfficeList.Select(x =>
                       new SelectListItem
                       {
                           Value = x.DTLOffice,
                           Text = x.DTLOffice
                       });

            ViewBag.DTLOfficeList = list;
            var _detail = GetUserDetailFromAspNetUser().Result;
            ViewBag.Designation = _detail.Designation;
            return View();
        }

        //End new changes

        /// <summary>
        /// Get Pending Cashless claim from DB
        /// </summary>
        /// <returns>List</returns>
        public IActionResult GetCashlessClaims()
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            if (_detail.Designation.ToLower() == "da1" || _detail.Designation.ToLower() == "da2" || _detail.Designation.ToLower() == "da")
            {
                _detail.Designation = "DA";
            }
            var pendingCashlessClaims = _processing.GetCashlessclaimByParam(null, null, null,null, null, null, _detail.Designation);
            return Json(pendingCashlessClaims);
        }

        //New changes by nirbhay
        public IActionResult GetCashlessCreditlatter()
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            if (_detail.Designation.ToLower() == "da1" || _detail.Designation.ToLower() == "da2" || _detail.Designation.ToLower() == "hospitalhser1")
            {
                _detail.Designation = "HospitalUser1";
            }
            var pendingCashlessCreditletter = _processing.GetCashlessCreditlatter(  null, null, null, _detail.Designation);
            return Json(pendingCashlessCreditletter);
        }

        //End changes

        //[Route("Mediclaim/Processing/CashlessClaimsByParam/{claimDate}/{claimId?}/{cardCategory?}/{organization?}")]
        [Route("Mediclaim/Processing/CashlessClaimsByParam/{claimDate}/{claimId?}")]
        public IActionResult GetCashlessClaimsByParam([FromRoute] string claimDate, [FromRoute] int? claimId = null)
        {
            //1%2F1%2F0001
            claimDate = (DateTime.Compare(Convert.ToDateTime(claimDate.Replace("%2F", "-")), DateTime.MinValue)) > 0 ? claimDate.Replace("%2F", "-") : null;
            claimId = claimId > 0 ? claimId : null;
            //cardCategory = cardCategory != "NA" ? cardCategory : null;
            //organization = organization != "NA" ? organization : null;
            var _detail = GetUserDetailFromAspNetUser().Result;
            if (_detail.Designation == "DA1" || _detail.Designation == "DA2")
            {
                _detail.Designation = null;
            }
            else
            {
                _detail.Designation = _detail.Designation.ToLower();
            }

            var cashlessMediclaim = _processing.GetCashlessclaimByParam(claimDate, claimId, null,null,null,null, _detail.Designation);
            return Json(cashlessMediclaim);
        }

        [Route("Mediclaim/CMO/Cashless/ApproveReject/{claimId:int}")]
        [Route("Mediclaim/IPD/Cashless/ApproveReject/{claimId:int}")]
        [Route("Mediclaim/Processing/Cashless/ApproveReject/{claimId:int}")]
        public IActionResult CashlessClaimView([FromRoute] int claimId)
        { 
            var designations = new List<string>() { "da1", "da2" };
            var _detail = GetUserDetailFromAspNetUser().Result;
            ViewBag.Designation = _detail.Designation;
            //if (_detail.Designation.ToLower() != "da1" || _detail.Designation.ToLower() != "da2")
            if (!designations.Contains(_detail.Designation.ToLower()))
            {
                return RedirectToAction("ClaimAndVoucherView", new { claimType = "cashless", claimId = claimId });
            }
            else
            {
                var cashlessMediclaim = _processing.GetCashlessByClaimId(claimId);
                return View(cashlessMediclaim);
            }
        }

        public IActionResult CashlessCreditView([FromRoute] int claimId)
        {
            var designations = new List<string>() { "da1", "da2" };
            var _detail = GetUserDetailFromAspNetUser().Result;
            ViewBag.Designation = _detail.Designation;
            //if (_detail.Designation.ToLower() != "da1" || _detail.Designation.ToLower() != "da2")
            if (!designations.Contains(_detail.Designation.ToLower()))
            {
                return RedirectToAction("ClaimAndVoucherView", new { claimType = "cashless", claimId = claimId });
            }
            else
            {
                var cashlessMediclaim = _processing.GetCashlessByClaimId(claimId);
                return View(cashlessMediclaim);
            }
        }

        /// <summary>
        /// Approve or rejects the claimm
        /// </summary>
        /// <param name="approveRejectModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Mediclaim/Processing/Cashless/ApproveReject")]
        public IActionResult ApproveRejectCashless([FromBody] ApproveRejectModel approveRejectModel)
        {
            var returnValue = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    //var _detail = GetUserDetailFromAspNetUser().Result;
                    //switch (_detail.Designation.ToLower())
                    //{
                    //    case "ag1": approveRejectModel.DealingAssistanceRemark 
                    //}

                    approveRejectModel.ModifiedBy = User.Identity.Name;
                    returnValue = _processing.ApproveRejectCashlessClaim(approveRejectModel);
                    if (returnValue > 0 && approveRejectModel.ClaimStatusId==3)
                    {
                        var cashless = _processing.GetCashlessByClaimId(approveRejectModel.ClaimId);
                        SendEmail.SendAcknowledgment(cashless.NameOfPatient, cashless.PatientEmailId, approveRejectModel.ClaimId.ToString(), "cashless", "reject",cashless.RejectReason);
                    }
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
        //start Changee by rajan
        public ClaimAndVoucherDetailModel GetClaimAndCreditDetail(string claimType, int claimId, string SerialNo)
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
                var _cashlessModel = _cashlessDetail.GetMediclaimCreditLetter(SerialNo);
                var _approved = _cashlessDetail.GetExtend_dateCreditLetter(SerialNo);      // chnage by rajan 22/01/2025 
                _model.approveReject = _approved;
                _model.Cashless = _cashlessModel;
            }

            return _model;

        }
        //public ClaimAndVoucherDetailModel GetClaimAndCreditDetail(string claimType, int claimId, int EmployeeNo)
        //{
        //    var _voucherModel = _voucher.GetMediclaimVouchersByParam(null, null, null, null, claimId, claimType).FirstOrDefault();

        //    var _model = new ClaimAndVoucherDetailModel()
        //    {
        //        Voucher = _voucherModel
        //    };

        //    if (!string.IsNullOrEmpty(claimType) && claimType.ToLower() == "noncashless")
        //    {
        //        var _nonCashlessModel = _nonCashlessDetail.GetNonCashlessByClaimId(claimId);
        //        _model.NonCashless = _nonCashlessModel;
        //    }
        //    else
        //    {
        //        var _cashlessModel = _cashlessDetail.GetMediclaimCreditLetter(EmployeeNo);
        //        _model.Cashless = _cashlessModel;
        //    }

        //    return _model;

        //}
        //End

        //Starte Change by Rajan
        [Route("Mediclaim/Processing/Cashless/ApproveRejectCreditLetter/{SerialNo}")]
        public IActionResult CashlessCreditLetterView([FromRoute] int claimId, string SerialNo)
        {
            var designations = new List<string>() { "da1", "da2" };
            var _detail = GetUserDetailFromAspNetUser().Result;
            ViewBag.Designation = _detail.Designation;
            //if (_detail.Designation.ToLower() != "da1" || _detail.Designation.ToLower() != "da2")
            if (!designations.Contains(_detail.Designation.ToLower()))
            {
                return RedirectToAction("CashlessCreditAndVoucher", new { claimType = "cashless", SerialNo });
            }
            else
            {
                var cashlessMediclaim = _processing.GetCashlessByClaimId(claimId);
                return View(cashlessMediclaim);
            }
        }
        //[Route("Mediclaim/Processing/Cashless/ApproveRejectCreditLetter/{EmployeeNo:int}")]
        //public IActionResult CashlessCreditLetterView([FromRoute] int claimId, int EmployeeNo)
        //{
        //    var designations = new List<string>() { "da1", "da2" };
        //    var _detail = GetUserDetailFromAspNetUser().Result;
        //    ViewBag.Designation = _detail.Designation;
        //    //if (_detail.Designation.ToLower() != "da1" || _detail.Designation.ToLower() != "da2")
        //    if (!designations.Contains(_detail.Designation.ToLower()))
        //    {
        //        return RedirectToAction("CashlessCreditAndVoucher", new { claimType = "cashless", EmployeeNo });
        //    }
        //    else
        //    {
        //        var cashlessMediclaim = _processing.GetCashlessByClaimId(claimId);
        //        return View(cashlessMediclaim);
        //    }
        //}
        //END Change by Rajan
        //Start Change by rajan
        //public IActionResult CashlessCreditAndVoucher(string claimType, int claimId, int EmployeeNo)
        //{
        //    var designation = GetUserDetailFromAspNetUser().Result.Designation;
        //    ViewBag.Designation = designation;
        //    if (claimType.ToLower() == "noncashless")
        //    {
        //        var _model = GetClaimAndCreditDetail(claimType, claimId, EmployeeNo);
        //        return View("NonCashlessClaimAndVoucherView", _model);
        //    }
        //    else
        //    {
        //        var _model = GetClaimAndCreditDetail(claimType, claimId, EmployeeNo);
        //        return View("CreditViewLetter", _model);
        //    }
        //}
        public IActionResult CashlessCreditAndVoucher(string claimType, int claimId, string SerialNo)
        {
            var designation = GetUserDetailFromAspNetUser().Result.Designation;
            ViewBag.Designation = designation;
            if (claimType.ToLower() == "noncashless")
            {
                var _model = GetClaimAndCreditDetail(claimType, claimId, SerialNo);
                return View("NonCashlessClaimAndVoucherView", _model);
            }
            else
            {
                var _model = GetClaimAndCreditDetail(claimType, claimId, SerialNo);
                return View("CreditViewLetter", _model);
            }
        }
        // End new Change by rajan
        //Start Change by Rajan
        [Route("Mediclaim/Processing/Cashless/ApproveRejectCredit")]
        public IActionResult ApproveRejectCashlessCredit([FromBody] ApproveRejectModel approveRejectcredit)
        {
            var returnValue = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    //var _detail = GetUserDetailFromAspNetUser().Result;
                    //switch (_detail.Designation.ToLower())
                    //{
                    //    case "ag1": approveRejectModel.DealingAssistanceRemark 
                    //}

                    approveRejectcredit.ModifiedBy = User.Identity.Name;
                    returnValue = _processing.ApproveRejectCashlessCredit(approveRejectcredit);
                    if (returnValue > 0 && approveRejectcredit.StatusCreditId == 3)
                    {
                        //var cashless = _processing.GetCashlessCreditByEmp(approveRejectcredit.EmployeeNo);
                        var cashless = _processing.GetCashlessCreditByEmp(approveRejectcredit.SerialNo);
                        //SendEmail.SendAcknowledgment(cashless.NameOfPatient, cashless.PatientEmailId, approveRejectModel.ClaimId.ToString(), "cashless", "reject", cashless.RejectReason);
                    }
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


        //End

        [HttpPost]
        [Route("Mediclaim/Processing/Cashless/PhysicalSubmit")]
        public IActionResult UpdatePhysicalSubmitCashless([FromBody] PhysicalSubmitModel physicalSubmitModel)
        {
            var returnValue = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    physicalSubmitModel.ModifiedBy = User.Identity.Name;
                    returnValue = _processing.UpdateCashlessPhysicalSubmit(physicalSubmitModel);
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

        #endregion

        #region NonCashless

        /// <summary>
        /// Landing page for Noncashless processing
        /// </summary>
        /// <returns></returns>
        public IActionResult NonCashlessClaims()
        {
            CommonModel populateList = ReadJson.LoadJson();

            ViewBag.DTLOfficeList = populateList.DTLOfficeList;

            var list = populateList.DTLOfficeList.Select(x =>
                       new SelectListItem
                       {
                           Value = x.DTLOffice,
                           Text = x.DTLOffice
                       });

            ViewBag.DTLOfficeList = list;
            var _detail = GetUserDetailFromAspNetUser().Result;
            ViewBag.Designation = _detail.Designation;
            return View();
        }

        /// <summary>
        /// Get Pending Noncashless claim from DB
        /// </summary>
        /// <returns>List</returns>
        public IActionResult GetNonCashlessClaims()
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            if (_detail.Designation.ToLower() == "da1" || _detail.Designation.ToLower() == "da2") {
                _detail.Designation = null;
            }
            var pendingNonCashlessClaims = _processing.GetNonCashlessclaimByParam(null,null,null,null,null,null,_detail.Designation);            
            return Json(pendingNonCashlessClaims);
        }

        [Route("Mediclaim/Processing/NonCashlessClaimsByParam/{claimDate}/{claimId?}/{cardCategory?}/{organization?}")]
        public IActionResult GetNonCashlessClaimsByParam([FromRoute] string claimDate, [FromRoute] int? claimId = null, [FromRoute] string cardCategory = null, string organization = null)
        {
            //1%2F1%2F0001
            claimDate = (DateTime.Compare(Convert.ToDateTime(claimDate.Replace("%2F", "-")), DateTime.MinValue)) > 0 ? claimDate.Replace("%2F", "-") : null;
            claimId = claimId > 0 ? claimId : null;
            cardCategory = cardCategory != "NA" ? cardCategory : null;
            organization = organization != "NA" ? organization : null;
            var _detail = GetUserDetailFromAspNetUser().Result;
            if (_detail.Designation == "DA1" || _detail.Designation == "DA2") {
                _detail.Designation = null;
            }
            else {
                _detail.Designation = _detail.Designation.ToLower();
            }

            var noncashlessMediclaim = _processing.GetNonCashlessclaimByParam(claimDate, claimId, null,null,cardCategory,organization, _detail.Designation);
            return Json(noncashlessMediclaim);
        }

        /// <summary>
        /// Gets Claim detials by claim Id
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        [Route("Mediclaim/Processing/NonCashless/ApproveReject/{claimId:int}")]
        public IActionResult NonCashlessClaimView([FromRoute] int claimId)
        {
            var designations = new List<string>() { "da1", "da2" };
            var _detail = GetUserDetailFromAspNetUser().Result;
            ViewBag.Designation = _detail.Designation;
            //if (_detail.Designation.ToLower() != "da1" || _detail.Designation.ToLower() != "da2")
            if(!designations.Contains(_detail.Designation.ToLower()))
            {
                return RedirectToAction("ClaimAndVoucherView", new { claimType = "noncashless", claimId = claimId });
            }
            else
            {
                var noncashlessMediclaim = _processing.GetNonCashlessByClaimId(claimId);
                return View(noncashlessMediclaim);
            }
        }

        public IActionResult ClaimAndVoucherView(string claimType, int claimId)
        {
            var designation = GetUserDetailFromAspNetUser().Result.Designation;
            ViewBag.Designation = designation;
            if (claimType.ToLower() == "noncashless")
            {
                var _model = GetClaimAndVoucherDetail(claimType, claimId);
                return View("NonCashlessClaimAndVoucherView", _model);
            }
            else {
                var _model = GetClaimAndVoucherDetail(claimType, claimId);
                return View("CashlessClaimAndVoucherView", _model);
            }
        }

        public ClaimAndVoucherDetailModel GetClaimAndVoucherDetail(string claimType, int claimId)
        {
           
            var _voucherModel = _voucher.GetMediclaimVouchersByParam(null, null, null, null, claimId, claimType).FirstOrDefault();

            var _model = new ClaimAndVoucherDetailModel() {
                Voucher = _voucherModel
            };

            if (!string.IsNullOrEmpty(claimType) && claimType.ToLower() == "noncashless") {
                var _nonCashlessModel = _nonCashlessDetail.GetNonCashlessByClaimId(claimId);
                _model.NonCashless = _nonCashlessModel;
            }
            else  {
                var _cashlessModel = _cashlessDetail.GetCashlessById(claimId);
                _model.Cashless = _cashlessModel;
            }

            return _model;
        }


        /// <summary>
        /// Approve or rejects the claimm
        /// </summary>
        /// <param name="approveRejectModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Mediclaim/Processing/Noncashless/ApproveReject")]
        public IActionResult ApproveRejectNonCashless([FromBody] ApproveRejectModel approveRejectModel)
        {
            var returnValue = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    //if (!string.IsNullOrEmpty(approveRejectModel.Designation))
                    //{
                    //    switch (approveRejectModel.Designation.ToLower())
                    //    {
                    //        //case "da1": approveRejectModel.DealingAssistanceRemark = approveRejectModel.Remark!=null ? approveRejectModel.Remark : approveRejectModel.RejectReason; break;
                    //        //case "da2": approveRejectModel.DealingAssistanceRemark = approveRejectModel.Remark != null ? approveRejectModel.Remark : approveRejectModel.RejectReason; break;
                    //        //case "aso": approveRejectModel.ASORemark = approveRejectModel.Remark != null ? approveRejectModel.Remark : approveRejectModel.RejectReason; break;
                    //        //case "so": approveRejectModel.SORemark= approveRejectModel.Remark != null ? approveRejectModel.Remark : approveRejectModel.RejectReason; break;
                    //        //default:  break;
                    //    }
                    //}

                    approveRejectModel.ModifiedBy = User.Identity.Name;
                    returnValue = _processing.ApproveRejectNonCashlessClaim(approveRejectModel);
                    if (returnValue > 0 && approveRejectModel.ClaimStatusId == 3)
                    {
                        var noncashless = _processing.GetNonCashlessByClaimId(approveRejectModel.ClaimId);
                        SendEmail.SendAcknowledgment(noncashless.PatientName, noncashless.EmailId, approveRejectModel.ClaimId.ToString(), "noncashless", "reject", noncashless.RejectReason);
                    }
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

        [HttpPost]
        [Route("Mediclaim/Processing/Noncashless/PhysicalSubmit")]
        public IActionResult UpdatePhysicalSubmit([FromBody] PhysicalSubmitModel physicalSubmitModel)
        {
            var returnValue = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    physicalSubmitModel.ModifiedBy = User.Identity.Name;
                    returnValue = _processing.UpdateNonCashlessPhysicalSubmit(physicalSubmitModel);
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


        #endregion

        [HttpPost]
        [Route("/Mediclaim/ChangeApprover/{claimType}/{claimId}/{forwardTo}")]
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

        //changed by nirbhay

        [HttpPost]
        [Route("/Mediclaim/ChangeApproverHospital/{claimType}/{employeeNo}/{forwardTo}")]
        public IActionResult ChangeApproverHospital([FromRoute] string claimType, [FromRoute] int employeeNo, [FromRoute] string forwardTo)
        {
            try
            {
                _processing.UpdateForwardToHospital(forwardTo, User.Identity.Name, employeeNo, claimType);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }
        //end changed 

        private async Task<ApplicationUser> GetUserDetailFromAspNetUser()
        {
            var _userdetails = await _userManager.FindByNameAsync(User.Identity.Name);
            return _userdetails;
        }

        //start chnage by rajan19/02/2025
        public IActionResult DashboardCashlessClaims()
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            var pendingCashlessClaims = _processing.GetCashlessclaimByParam(null, null, null, null, null, null, _detail.Designation);
            return Json(pendingCashlessClaims);
        }

        // add by rajan 26/02/2025
        public IActionResult GetMediclaimcashlessByPPO(string ppo)
        {
            var _detail = GetUserDetailFromAspNetUser().Result;
            var data = _processing.GetCashlessByPPO(_detail.UserName);
            return Json(data);
        }
        //End
        // add by nirbhay ExportToExcel 05/30/2025
        public ActionResult ExportToExcel(DateTime startDate, DateTime endDate)
        {
            var _detail = GetUserDetailFromAspNetUser().Result;

            // Declare data only once
            List<NonCashlessModel> data;

            if (_detail.UserName == "mediclaimASO")
            {
                data = (List<NonCashlessModel>)_processing.GetClaimsByASODateRange(startDate, endDate);
            }
            else if (_detail.UserName == "OPDdealingassistant")
            {
                data = (List<NonCashlessModel>)_processing.GetClaimsBymediclaimOPDDADateRange(startDate, endDate);
            }
            else if (_detail.UserName == "mediamdm")
            {
                data = (List<NonCashlessModel>)_processing.GetClaimsBymediclaimAMDMDateRange(startDate, endDate);
            }
            else if (_detail.UserName == "mediclaimdisbus")
            {
                data = (List<NonCashlessModel>)_processing.GetClaimsBymediclaimMediDisbusDateRange(startDate, endDate);
            }
            else
            {
                // Default fallback if needed
                data = new List<NonCashlessModel>();
            }

            var cleanedData = data.Select(d => new
            {
                Date = (d.CreatedDate != DateTime.MinValue) ? d.CreatedDate : (DateTime?)null,
                SerialNo = d.ClaimNumber,
                ClaimNo = d.ClaimId,
                PPONo = d.PPONumber,
                TypeOfClaim = d.ClaimType,
                Organization = d.Organization,
                CardCategory = d.CardCategory,
                PatientName = d.PatientName,
                ClaimStatus = d.ClaimStatusId,
                ChangeStatus = d.ForwardTo,
                Remark = d.Remark,
                PhysicalSubmit = d.PhysicalSubmit
            }).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Claims");
                worksheet.Cell(1, 1).InsertTable(cleanedData);

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return File(stream.ToArray(),
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "ClaimsReport.xlsx");
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SaveDeductedAmounts(List<OPDCNDModel> Deductions)
        {
            if (Deductions != null && Deductions.Any())
            {
                _processing.UpdateDeductedAmounts(Deductions);
            }

            return RedirectToAction("NonCashlessClaimAndVoucherView"); // Replace with actual view name
        }
        //end
       
        [HttpPost]
        public IActionResult SaveDeductedAmountsAjax(List<OPDCNDModel> deductedList)
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                return Json(new { success = false, message = "Connection string is missing." });
            }

            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in deductedList)
                        {
                            string query = @"UPDATE OPDCND 
                                         SET DeductedAmount = @DeductedAmount 
                                         WHERE OPDCNDID = @OPDCNDID";

                            db.Execute(query, new
                            {
                                DeductedAmount = item.DeductedAmount,
                                OPDCNDID = item.OPDCNDId
                            }, transaction: transaction);
                        }

                        transaction.Commit();
                        return Json(new { success = true });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Json(new { success = false, message = "Error occurred while saving data.", error = ex.Message });
                    }
                }
            }
        }


        [HttpGet]
        public IActionResult GetUpdatedDeductions(int opdcndId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var data = db.Query<OPDCNDModel>(
                    "SELECT OPDCNDID, DeductedAmount FROM OPDCND WHERE OPDCNDID = @OPDCNDID",
                    new { OPDCNDID = opdcndId }).FirstOrDefault();

                if (data == null)
                {
                    return Json(new { success = false, message = "Record not found." });
                }

                //return Json(new
                //{
                //    success = true,
                //    opdcndId = data.OPDCNDId,
                //    deductedAmount = data.DeductedAmount
                //});
                return View(data);
            }
        }


    }
}
