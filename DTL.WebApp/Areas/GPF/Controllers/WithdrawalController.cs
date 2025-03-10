using DTL.Business.Common;
using DTL.Business.GPF.Withdrawal;
using DTL.Business.UserManagement;
using DTL.Model.Models.GPF;
using DTL.Model.Models.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    [Area("GPF")]
    [Authorize(Roles = "DVB Pension Trust,Admin")]
    public class WithdrawalController : Controller
    {
        private readonly IWithdrawal _withdrawal;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;

        public WithdrawalController(IWithdrawal withdrawal, IAssignPermission assignPermission)
        {
            _withdrawal = withdrawal;
            _assignPermission = assignPermission;
        }

        public IActionResult Index()
        {
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList; 
            IEnumerable<AssignPermissionModel> assignPermissionModels = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, null, null); 
            return View(assignPermissionModels); 
        }
        public IActionResult RefundableView() {
            return View();
        }   
        public IActionResult NonRefundableView()
        {
            ViewBag.IsNonRefundable = true;
            return View("RefundableView");
        }
        public IActionResult settlement()
        {
            return View();
        }
        public IActionResult settlementList()
        {
            return View();
        }
        public IActionResult EdlisList()
        {
            return View();
        }
        public IActionResult EdlisForm()
        {
            return View();
        }
        public IActionResult settlementView()
        {
            return View();
        }
        public IActionResult edlisView()
        {
            return View();
        }
        #region Refundable
        /// <summary>
        /// Get GPF list of Refundanle
        /// </summary>
        /// <returns>List</returns>
        public IActionResult RefundableList()
        {
            var _gpfWithdrawalList = _withdrawal.GetGPFWithdrawalByParam(null, "Refundable", null, null, null, null, null, User.Identity.Name);
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "GPF", "Withdrawal Refundable").FirstOrDefault();
            if (permission != null)
            {
                _create = permission.Create;
                _edit = permission.Edit;
                _view = permission.View;
                _delete = permission.Delete;
            }
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;

            return View(_gpfWithdrawalList);
        }

        /// <summary>
        /// Landing page of Add refundable
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GPF/Withdrawal/Save/RefundableGPF")]
        [Route("GPF/Withdrawal/Edit/RefundableGPF/{withdrawId}")]
        public IActionResult AddRefundableGPF([FromRoute] int withdrawId = 0)
        {
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            ViewBag.EditWithdrawId = withdrawId;
            return View();
        }

        /// <summary>
        /// Save Refundabe GPF detail
        /// </summary>
        /// <param name="withdrawalModel"></param>
        /// <returns>INT</returns>
        [HttpPost]
        [Route("GPF/Withdrawal/Save/RefundableGPF")]
        public IActionResult AddRefundableGPF([FromBody] GPFWithdrawalModel withdrawalModel)
        {
            string _withdrawalId = "";
            try
            {
                if (ModelState.IsValid)
                {
                    withdrawalModel.CreatedBy = User.Identity.Name;
                    var id = _withdrawal.SaveGPFWithdrawal(withdrawalModel);
                    if (withdrawalModel.OperationType.ToLower() == "submit") {
                        _withdrawalId = _withdrawal.GetGPFWithdrawalByParam(withdrawalModel.WithdrawId, null, null, null, null, null, null, null).FirstOrDefault().UniqueNumber;
                    }
                    else {
                        _withdrawalId = id.ToString();
                    }
                }
                else
                {
                    IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(allErrors);
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok(_withdrawalId);
        }

        /// <summary>
        /// Refundable View
        /// </summary>
        /// <returns>View with GPF Refundable Data</returns>
        [Route("GPF/Withdrawal/RefundableGPF/{withdrawId}")]
        public IActionResult RefundableView([FromRoute] int withdrawId)
        {
            var _gpfWithdrawalList = _withdrawal.GetGPFWithdrawalByParam(withdrawId, null, null, null, null, null, null, null);
            return View(_gpfWithdrawalList.FirstOrDefault());
        }

        #endregion

        #region Non-Refundable
        /// <summary>
        /// Get GPF list of Non refundanle
        /// </summary>
        /// <returns>List</returns>
        public IActionResult NonRefundableList()
        {
            var _gpfWithdrawalList = _withdrawal.GetGPFWithdrawalByParam(null, "NonRefundable", null, null, null, null, null, User.Identity.Name);
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "GPF", "Withdrawal NonRefundable").FirstOrDefault();
            if (permission != null)
            {
                _create = permission.Create;
                _edit = permission.Edit;
                _view = permission.View;
                _delete = permission.Delete;
            }
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;

            ViewBag.IsNonRefundable = true;

            return View("RefundableList", _gpfWithdrawalList);
        }

        /// <summary>
        /// Landing page of AddNonRefundable
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GPF/Withdrawal/Save/NonRefundableGPF")]
        [Route("GPF/Withdrawal/Edit/NonRefundableGPF/{withdrawId}")]
        public IActionResult AddNonRefundableGPF([FromRoute] int withdrawId = 0)
        {
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            ViewBag.EditWithdrawId = withdrawId;

            ViewBag.IsNonRefundable = true;

            //return View();
            return View("AddRefundableGPF");
        }


        /// <summary>
        /// Save Non-Refundabe GPF detail
        /// </summary>
        /// <param name="withdrawalModel"></param>
        /// <returns>INT</returns>
        [HttpPost]
        [Route("GPF/Withdrawal/Save/NonRefundableGPF")]
        public IActionResult AddNonRefundableGPF([FromBody] GPFWithdrawalModel withdrawalModel)
        {
            string _withdrawalId = "";
            try
            {
                if (ModelState.IsValid)
                {
                    //withdrawalModel.CreatedBy = User.Identity.Name;
                    //_withdrawalId = _withdrawal.SaveGPFWithdrawal(withdrawalModel);
                    withdrawalModel.CreatedBy = User.Identity.Name;
                    var id = _withdrawal.SaveGPFWithdrawal(withdrawalModel);
                    if (withdrawalModel.OperationType.ToLower() == "submit")
                    {
                        _withdrawalId = _withdrawal.GetGPFWithdrawalByParam(withdrawalModel.WithdrawId, null, null, null, null, null, null, null).FirstOrDefault().UniqueNumber;
                    }
                    else
                    {
                        _withdrawalId = id.ToString();
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

            return Ok(_withdrawalId);
        }

        [Route("GPF/Withdrawal/NonRefundableGPF/{withdrawId}")]
        public IActionResult NonRefundableView([FromRoute] int withdrawId)
        {
            var _gpfWithdrawalList = _withdrawal.GetGPFWithdrawalByParam(withdrawId, null, null, null, null, null, null, null);

            ViewBag.IsNonRefundable = true;

            return View("RefundableView", _gpfWithdrawalList.FirstOrDefault());
        }
        #endregion

        /// <summary>
        /// Upload the files
        /// </summary>
        /// <param name="files"></param>
        /// <returns>OK</returns>
        [HttpPost]
        [Route("GPF/Withdrawal/Upload/{applicationSubArea}/{withdrawalId}/{indicator}")]
        public IActionResult FileUpload([FromRoute]string applicationSubArea, [FromRoute]int withdrawalId, [FromRoute]string indicator, IList<IFormFile> files)
        {
            List<GPFDocumentModel> documentList = new List<GPFDocumentModel>();
            var _documentFor = "";
            int _fileSize = 2 * 1024 * 1024;
            try
            {
                var lastFileIndex = files.Count - 1;
                //foreach (var file in files)
                for (int i = 0; i < files.Count; i++)
                {
                    var fileextension = Path.GetExtension(files[i].FileName);

                    if (fileextension != ".pdf")
                    {
                        return BadRequest("Only pdf file can be uploaded");
                    }

                    if (files[i].Length > _fileSize)
                    {
                        return BadRequest("File upto 2Mb of size is allowed");
                    }

                    var filename = Guid.NewGuid().ToString() + fileextension;
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"files/{applicationSubArea}", filename);
                    var dbfilepath = Path.Combine($"files/{applicationSubArea}", filename);
                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        files[i].CopyTo(fs);
                    }

                    switch (indicator)
                    {
                        case "s": _documentFor = "SalarySlip"; break;
                        case "i": _documentFor = "IdCard"; break;
                        case "si": _documentFor = lastFileIndex != i ? "SalarySlip" : "IdCard"; break;
                    }

                    documentList.Add(new GPFDocumentModel
                    {
                        DocumentPath = dbfilepath,
                        ApplicationArea = "GPF",
                        ApplicationSubArea = applicationSubArea,
                        ReferenceId = withdrawalId,
                        //DocumentFor = lastFileIndex != i ? "SalarySlip" : "IdCard",
                        DocumentFor = _documentFor,
                        CreatedBy = User.Identity.Name
                    });
                }
                _withdrawal.SaveDocuments(documentList);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }

        public IActionResult CurrentGPFBalance()
        {
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            return View();
        }
    
        [Route("GPF/Withdrawal/History")]
        public IActionResult WithdrawalHistory()
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "GPF", "Withdrawal History").FirstOrDefault();
            if (permission != null)
            {
                _create = permission.Create;
                _edit = permission.Edit;
                _view = permission.View;
                _delete = permission.Delete;
            }
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;
            var withdrawalHistoryList = _withdrawal.GetGPFWithdrawalByParam(null, null, null, null, null, null, null, User.Identity.Name);
            return View(withdrawalHistoryList);
        }

        [HttpDelete]
        [Route("GPF/Withdrawal/History/Delete/{withdrawalId}")]
        [Route("GPF/Withdrawal/NonRefundable/Delete/{withdrawalId}")]
        [Route("GPF/Withdrawal/Refundable/Delete/{withdrawalId}")]
        public IActionResult DeleteGPFWithdrawal([FromRoute] int withdrawalId)
        {
            try
            {
                var _count = _withdrawal.DeleteGPFWithdrawal(withdrawalId, User.Identity.Name);
                if (_count == 0) {
                    return BadRequest("Deletion filed");
                }
            }
            catch (Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);

            }
            return Ok();
        }
    }
}
