using DTL.Business.Common;
using DTL.Business.GPF.ModifyWithdrawal;
using DTL.Business.UserManagement;
using DTL.Model.Models.GPF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    [Area("GPF")]
    [Authorize(Roles = "DVB Pension Trust,Admin")]
    public class ModifyGPFWithdrawalController : Controller
    {
        private readonly IModifyWithdrawal _modifyWithdrawal;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;

        public ModifyGPFWithdrawalController(IModifyWithdrawal modifyWithdrawal, IAssignPermission assignPermission)
        {
            _modifyWithdrawal = modifyWithdrawal;
            _assignPermission = assignPermission;
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "GPF", "ModifyGPFWithdrawal").FirstOrDefault();
            if (permission != null)
            {
                _create = permission.Create;
                _edit = permission.Edit;
                _view = permission.View;
                _delete = permission.Delete;
            }
        }

        /// <summary>
        /// Landing page of Withdrawal Edit screen
        /// </summary>
        /// <param name="withdrawalType"></param>
        /// <returns>View</returns>
        [Route("/GPF/Modify/{withdrawalType}/{withdrawalId}")]
        public IActionResult Index([FromRoute] string withdrawalType, [FromRoute] int withdrawalId)
        {
            ViewBag.Heading = withdrawalType;
            ViewBag.WithdrawalType = withdrawalType.ToLower();
            ViewBag.WithdrawalId = withdrawalId;
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            return View();
        }

        [HttpGet]
        [Route("Get/GPF/Modify/{withdrawalType}/{withdrawId}")]
        public IActionResult UpdateGPFWithdraw([FromRoute] int withdrawId)
        {
            var _gpfWithdrawal = _modifyWithdrawal.GetGPFWithdrawalByParam(withdrawId, null, null, null, null, null, null, null).FirstOrDefault();
            if (_gpfWithdrawal != null)
            {
                _gpfWithdrawal.GPFDocuments = _modifyWithdrawal.GetGPFDocumentsByParam(null, withdrawId);
            }
            return Json(_gpfWithdrawal);
        }

        /// <summary>
        /// Updates the record in db for GPF Withdrawal
        /// </summary>
        /// <param name="withdrawalType"></param>
        /// <param name="withdrawalModel"></param>
        /// <returns>Id</returns>
        [HttpPost]
        [Route("/GPF/Modify/{withdrawalType}")]
        public IActionResult UpdateGPFWithdrawal([FromRoute] string withdrawalType, [FromBody] GPFWithdrawalModel withdrawalModel)
        {
            int _withdrawalId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    withdrawalModel.ModifideBy = User.Identity.Name;
                    withdrawalModel.ApplicationStatus = 1; //for Modified
                    withdrawalModel.RejectReason = null;
                    _withdrawalId = _modifyWithdrawal.UpdateGPFWithdrawal(withdrawalModel);
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

        /// <summary>
        /// Delete the files from folders location
        /// </summary>
        /// <param name="documentList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/GPF/Withdrawal/Modify/Document/Delete/{documentId}")]
        public IActionResult DeleteFile([FromRoute] int documentId)
        {   
            try
            {
                //foreach (var file in files)
                //for (int i = 0; i < documentList.Count; i++)
                //{ 
                var updateCount = _modifyWithdrawal.UpdateDocumet(documentId, false, User.Identity.Name);
                if (updateCount > 0)
                {
                    var _document = _modifyWithdrawal.GetGPFDocumentsByParam(documentId, null, false).FirstOrDefault();
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _document.DocumentPath);
                    System.IO.File.Delete(filepath);
                }
                else
                {
                    return BadRequest("Document delete failed");
                }
            }

            catch (Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }

        [Route("/GPF/Withdrawal/Modify/Document/{referenceId}")]
        public IActionResult GetGFPDocuments([FromRoute] int referenceId)
        {
            var _documents = _modifyWithdrawal.GetGPFDocumentsByParam(null, referenceId).OrderByDescending(x => x.DocumentFor);
            return Json(_documents);
        }

        /// <summary>
        /// Upload the files
        /// </summary>
        /// <param name="files"></param>
        /// <returns>OK</returns>
        [HttpPost]
        [Route("/GPF/Withdrawal/Modify/Document/{applicationSubArea}/{withdrawalId}/{indicator}")]
        public IActionResult FileUpload([FromRoute] string applicationSubArea, [FromRoute] int withdrawalId, [FromRoute] string indicator, IList<IFormFile> files)
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
                        DocumentFor = _documentFor,
                        CreatedBy = User.Identity.Name
                    });
                }
                _modifyWithdrawal.SaveDocumet(documentList);
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
