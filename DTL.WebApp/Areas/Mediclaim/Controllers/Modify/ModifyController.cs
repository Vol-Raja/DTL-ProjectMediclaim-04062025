using DTL.Business.Common;
using DTL.Business.Mediclaim.Modify;
using DTL.Model.CommonModels;
using DTL.Model.Models;
using DTL.Model.Models.Mediclaim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers.Modify
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class ModifyController : Controller
    {
        private readonly IModify _modify;
        public ModifyController(IModify modify)
        {
            _modify = modify;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Mediclaim/Modify/EditCashless/{claimId}")]
        public IActionResult EditCashless([FromRoute] int claimId)
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
            ViewBag.ClaimId = claimId;
            //var cashlessMediclaim = _modify.GetCashlessByParam(claimId);
            var masterDocuments = GetMasterDocumentList(); 
            return View(masterDocuments);
        }

        [HttpGet]
        [Route("/Mediclaim/Modify/Cashless/{claimId}")]
        public IActionResult LoadCashlessDataByClaimId([FromRoute] int claimId)
        {
            var cashlessMediclaim = _modify.GetCashlessByParam(claimId);
            return Json(cashlessMediclaim);
        }

        [HttpPost]
        [Route("/Mediclaim/Modify/UpdateCashless")]
        public IActionResult UpdateCashless([FromBody] CashlessModel cashless)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cashless.ModifideBy = User.Identity.Name;
                    _modify.UpdateMediclaimCashless(cashless);
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
            return Ok();
        }


        [Route("/Mediclaim/Modify/EditNonCashless/{claimId}")]
        public IActionResult EditNonCashless([FromRoute]int claimId)
        {
            ViewBag.ClaimId = claimId;
            CommonModel populateList = ReadJson.LoadJson();

            ViewBag.DTLOfficeList = populateList.DTLOfficeList;

            var list = populateList.DTLOfficeList.Select(x =>
                       new SelectListItem
                       {
                           Value = x.DTLOffice,
                           Text = x.DTLOffice
                       });

            ViewBag.DTLOfficeList = list;
            var masterDocuments = GetMasterDocumentList();
            return View(masterDocuments);
        }

        [Route("/Mediclaim/Modify/NonCashless/{claimId}")]
        public IActionResult LoadNonCashlessDataByClaimId([FromRoute]int claimId)
        {
            var nonCashlessMediclaim = _modify.GetNonCashlessByParam(claimId);
            return Json(nonCashlessMediclaim);
        }

        [HttpPost]
        [Route("/Mediclaim/Modify/NonCashless")]
        public IActionResult UpdateNonCashlesMedicalim([FromBody] NonCashlessModel nonCashless)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    nonCashless.ModifideBy= User.Identity.Name;
                    nonCashless.ClaimStatusId = 1;
                    _modify.UpdateNonCashless(nonCashless);
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

            return Ok();
        }


        #region Documents
        [HttpPost]
        [Route("Mediclaim/Modify/UploadFile/{applicationSubArea}/{indicator}")]
        public IActionResult FileUpload([FromRoute] string applicationSubArea, [FromRoute] int indicator, IList<IFormFile> files)
        {
            List<MediclaimDocumentModel> documentList = new List<MediclaimDocumentModel>();
            int _fileSize = 2 * 1024 * 1024;
            try
            {
                var dataJson = ReadJson.LoadJson();
                var cashlistDocumentList = dataJson.MediclaimCashlessDocumentList;
                var _indicator = _modify.GetMasterDocumentList().Where(x => x.DocumentId == indicator).FirstOrDefault();

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

                    documentList.Add(new MediclaimDocumentModel
                    {
                        FileName = files[i].FileName,
                        DocumentPath = dbfilepath,
                        ApplicationArea = "Mediclaim",
                        ApplicationSubArea = applicationSubArea,
                        ReferenceId = -1,
                        //DocumentFor = lastFileIndex != i ? "SalarySlip" : "IdCard",
                        DocumentFor = _indicator.DocumentName,
                        CreatedBy = User.Identity.Name
                    });
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok(documentList);
        }

        [HttpPost]
        [Route("Mediclaim/Modify/DeleteFile/{applicationSubArea}/{documentId}")]
        public IActionResult DeleteFile([FromBody]string filePath,[FromRoute] int documentId)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);
                System.IO.File.Delete(path);
                if (documentId > 0) {
                    _modify.DeleteDocuments(documentId);
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }

        #endregion

        private IEnumerable<MasterDocumentModel> GetMasterDocumentList()
            => _modify.GetMasterDocumentList();
    }
}
