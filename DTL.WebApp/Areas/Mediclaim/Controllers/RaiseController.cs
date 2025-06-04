using DTL.Business.Common;
using DTL.Business.EmployeeRegistration;
using DTL.Business.Mediclaim.MediclaimRaise;
using DTL.Business.UserManagement;
using DTL.Model.CommonModels;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.UserManagement;
using DTL.WebApp.Common.CommonClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class RaiseController : Controller
    {
        private readonly IRaise _raise;
        private readonly IConfiguration _configuration;
        private readonly IAssignPermission _assignPermission;
        private readonly ILogger<RaiseController> _logger;

        public RaiseController(IRaise raise,
            IAddEmployee addEmployeeService,
            IConfiguration configuration,
            IAssignPermission assignPermission,
            ILogger<RaiseController> logger)
        {
            _raise = raise;
            _configuration = configuration;
            _assignPermission = assignPermission;
            _logger = logger;
        }

        /// <summary>
        /// Dashboard
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            IEnumerable<AssignPermissionModel> assignPermissionModels = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "Mediclaim", null);

            return View(assignPermissionModels);
        }

        /// <summary>
        /// Landing page of cashless data entry form
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Cashless()
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
            var masterDocumentList = _raise.GetMasterDocumentList();
            return View(masterDocumentList);
        }
        //New changed by nirbhay
        public IActionResult Creditletter()
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
            var masterDocumentList = _raise.GetDocumentListForCreditLetter();
            return View(masterDocumentList);
        }
       //New end changes
        public IActionResult Cashless_old()
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
            var masterDocumentList = _raise.GetMasterDocumentList();
            return View(masterDocumentList);
        }

        /// <summary>
        /// Landing page of non - cashless data entry form
        /// </summary>
        /// <returns>View</returns>
        public IActionResult NonCashless()
        {
            CommonModel populateList = ReadJson.LoadJson();            
            
            ViewBag.DTLOfficeList = populateList.DTLOfficeList;

            var list = populateList.DTLOfficeList.Select(x =>
                       new SelectListItem {
                           Value = x.DTLOffice,
                           Text = x.DTLOffice
                       });

            ViewBag.DTLOfficeList = list;

            var masterDocumentList = _raise.GetMasterDocumentList();
            return View(masterDocumentList);
        }

        public IActionResult NonCashless_new()
        {
            var masterDocumentList = _raise.GetMasterDocumentList();
            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = populateList.DTLOfficeList;
            return View(masterDocumentList);
        }

        /// <summary>
        /// Create new noncashless claim in system
        /// </summary>
        /// <param name="mediclaimModel"></param>
        /// <returns>OK</returns>
        [HttpPost]
        public IActionResult AddNewMediclaimNonCashless([FromBody] NonCashlessModel noncashlessModel)
        {
            int _mediclaimId = 0;
            try
            {
                if (ModelState.IsValid)
                {   
                    noncashlessModel.CreatedBy = User.Identity.Name;
                    _mediclaimId = _raise.SaveMediclaimNonCashless(noncashlessModel);

                    var smsservice = new MessageService(_configuration);
                    string textmessage = string.Format("Your Mediclaim with id {0} is generated", _mediclaimId);
                    smsservice.SendCredential(noncashlessModel.MobileNumber, textmessage).Wait();
                    //SendEmail.SendAcknowledgment(noncashlessModel.PatientName, noncashlessModel.EmailId, _mediclaimId.ToString(), "Non-Cashless", "created");
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

            return Ok(_mediclaimId);
        }

        /// <summary>
        /// Create new cashless claim in system
        /// </summary>
        /// <param name="mediclaimModel"></param>
        /// <returns>OK</returns>
        [HttpPost]
        [Route("/Mediclaim/Raise/AddNewMediclaimCashless")]
        public IActionResult AddNewMediclaimCashless([FromBody] CashlessModel cashlessModel)
        {
            int _mediclaimId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    cashlessModel.CreatedBy = User.Identity.Name;
                    _mediclaimId = _raise.SaveMediclaimCashless(cashlessModel);

                    var smsservice = new MessageService(_configuration);
                    string textmessage = string.Format("Your Mediclaim with id {0} is generated", _mediclaimId);
                    smsservice.SendCredential(cashlessModel.PatientPhoneNumber, textmessage).Wait();
                    SendEmail.SendAcknowledgment(cashlessModel.NameOfPatient, cashlessModel.EmailId, _mediclaimId.ToString(), "Cashless", "created");
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

            return Ok(_mediclaimId);
        }

        // new changes by nirbhay
        [HttpPost]
        [Route("/Mediclaim/Raise/AddNewAdmission")]
        public IActionResult AddNewAdmission([FromBody] CashlessModel cashlessModel)
        {
            int _mediclaimId = 0;
            try
            {
                    cashlessModel.CreatedBy = User.Identity.Name;
                    _mediclaimId = _raise.SaveAddNewAdmission(cashlessModel);

                var smsservice = new MessageService(_configuration);
                string textmessage = string.Format("Your Mediclaim with id {0} is generated", _mediclaimId);
                smsservice.SendCredential(cashlessModel.PatientPhoneNumber, textmessage).Wait();
                SendEmail.SendAcknowledgment(cashlessModel.NameOfPatient, cashlessModel.EmailId, _mediclaimId.ToString(), "Cashless", "created");
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(_mediclaimId);
        }

        //end by new changes

        [Route("/Mediclaim/Cashless/Delete/{claimId}")]
        public IActionResult UpdateCashlessIsDelete([FromRoute] int claimId)
        {
            var returnValue = 0;
            CashlessModel cashlessModel = new CashlessModel();
            try
            {
                if (ModelState.IsValid)
                {
                    cashlessModel.ClaimId = claimId;
                    cashlessModel.ModifideBy = User.Identity.Name;
                    cashlessModel.IsDelete = true;
                    returnValue = _raise.UpdateCashlessDelete(cashlessModel);
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

            //return Json(returnValue > 0 ? "Success" : "Fail");
            return Redirect("/Mediclaim/Cashless");
        }

        //new changes by nirbhay

        [Route("/Mediclaim/Raise/Searchppo/{PPONumber}")]
        [HttpGet]
        public IActionResult Searchppo([FromRoute] int PPONumber)
        {

            //CashlessModel cashlessModel = new CashlessModel();
            //cashlessModel.PPONumber = PPONumber;
            //returnValue = _raise.Searchppo(cashlessModel);
            var _PPODetail = _raise.Searchppo(PPONumber);
                return Ok(_PPODetail);
        }

        [Route("/Mediclaim/Raise/Loginuser/{PPONumber1}")]
        [HttpGet]
        public IActionResult Loginuser([FromRoute] string PPONumber1)
        {

            //CashlessModel cashlessModel = new CashlessModel();
            //cashlessModel.PPONumber = PPONumber;
            //returnValue = _raise.Searchppo(cashlessModel);
            var _PPODetail = _raise.Loginuser(PPONumber1);
            return Ok(_PPODetail);
        }

        //end new changes
        [Route("/Mediclaim/Raise/Getcreditletterdata/{PPONumber2}")]
        [HttpGet]
        public IActionResult Getcreditletterdata([FromRoute] string PPONumber2)
        {
            var _PPODetail = _raise.Getcreditletterdata(PPONumber2);
            return Ok(_PPODetail);
        }

        //end new changes


        [HttpPost]
        [Route("Mediclaim/NonCashless/Delete")]
        public IActionResult UpdateNonCashlessIsDelete([FromBody] NonCashlessModel nonCashlessModel)
        {
            var returnValue = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    nonCashlessModel.ModifideBy = User.Identity.Name;
                    returnValue = _raise.UpdateNonCashlessDelete(nonCashlessModel);
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

        #region Documents
        [HttpPost]
        [Route("Mediclaim/Raise/UploadFile/{applicationSubArea}/{indicator}")]
        public IActionResult FileUpload([FromRoute] string applicationSubArea, [FromRoute] int indicator, IList<IFormFile> files)
        {
            List<MediclaimDocumentModel> documentList = new List<MediclaimDocumentModel>();
            int _fileSize = 2 * 1024 * 1024;
            try
            {
                var dataJson = ReadJson.LoadJson();
                var cashlistDocumentList = dataJson.MediclaimCashlessDocumentList;

                var _indicator = _raise.GetMasterDocumentList().Where(x => x.DocumentId == indicator).FirstOrDefault();

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
                // Build log details
                var logDetails = new
                {
                    User = User?.Identity?.Name ?? "Unknown",
                    SubArea = applicationSubArea,
                    Indicator = indicator,
                    FileNames = files?.Select(f => f.FileName).ToList(),
                    Exception = error
                };
                var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
                var logFilePath = Path.Combine(logDirectory, $"CreditReportUploadLog_{DateTime.Now:yyyyMMdd}.txt");
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine("------------ Log Entry ------------");
                    writer.WriteLine($"Timestamp      : {logDetails}");
                    writer.WriteLine($"User           : {logDetails.User}");
                    writer.WriteLine($"SubArea        : {logDetails.SubArea}");
                    writer.WriteLine($"Indicator      : {logDetails.Indicator}");
                    writer.WriteLine($"File Names     : {string.Join(", ", logDetails.FileNames ?? new List<string>())}");
                    writer.WriteLine($"Error Message  : {logDetails.Exception}");
                    writer.WriteLine("-----------------------------------");
                    writer.WriteLine();
                }

                _logger.LogError("Error uploading credit report: {@LogDetails}", logDetails);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok(documentList);
        }

        [HttpPost]
        [Route("Mediclaim/Raise/DeleteFile/{applicationSubArea}")]
        public IActionResult DeleteFile([FromBody] string filePath, [FromRoute] string applicationSubArea)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);
                path = path.Trim();
                path = Regex.Replace(path, @"[\x00-\x1F]", ""); // Remove control characters

                System.IO.File.Delete(path);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }

        #endregion
        //New chnage by Rajan 10/01/2025
        [HttpPost]
        [Route("Mediclaim/Raise/Upload_Report/{applicationSubArea}/{indicator}")]
        public IActionResult FileUpload_CreditReport([FromRoute] string applicationSubArea, [FromRoute] int indicator, IList<IFormFile> files)
        {
            List<MediclaimDocumentModel> documentList = new List<MediclaimDocumentModel>();
            int _fileSize = 2 * 1024 * 1024;
            try
            {
                var dataJson = ReadJson.LoadJson();
                var cashlistDocumentList = dataJson.MediclaimCashlessDocumentList;

                var _indicator = _raise.GetDocumentListForCreditLetter().Where(x => x.DocumentId == indicator).FirstOrDefault();

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

                    var filename = Guid.NewGuid().ToString() + _indicator.DocumentName + fileextension;
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
                    var error = "nirbhay";
                    // Build log details
                    var logDetails = new
                    {
                        User = User?.Identity?.Name ?? "Unknown",
                        SubArea = applicationSubArea,
                        Indicator = indicator,
                        FileNames = files?.Select(f => f.FileName).ToList(),
                        Exception = error
                    };
                    var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
                    if (!Directory.Exists(logDirectory))
                    {
                        Directory.CreateDirectory(logDirectory);
                    }
                    var logFilePath = Path.Combine(logDirectory, $"CreditReportUploadLog_{DateTime.Now:yyyyMMdd}.txt");
                    using (StreamWriter writer = new StreamWriter(logFilePath, true))
                    {
                        writer.WriteLine("------------ Log Entry ------------");
                        writer.WriteLine($"Timestamp      : {logDetails}");
                        writer.WriteLine($"User           : {logDetails.User}");
                        writer.WriteLine($"SubArea        : {logDetails.SubArea}");
                        writer.WriteLine($"Indicator      : {logDetails.Indicator}");
                        writer.WriteLine($"File Names     : {string.Join(", ", logDetails.FileNames ?? new List<string>())}");
                        writer.WriteLine($"Error Message  : {logDetails.Exception}");
                        writer.WriteLine("-----------------------------------");
                        writer.WriteLine();
                    }

                    _logger.LogError("Error uploading credit report: {@LogDetails}", logDetails);
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                // Build log details
                var logDetails = new
                {
                    User = User?.Identity?.Name ?? "Unknown",
                    SubArea = applicationSubArea,
                    Indicator = indicator,
                    FileNames = files?.Select(f => f.FileName).ToList(),
                    Exception = error
                };
                var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
                var logFilePath = Path.Combine(logDirectory, $"CreditReportUploadLog_{DateTime.Now:yyyyMMdd}.txt");
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine("------------ Log Entry ------------");
                    writer.WriteLine($"Timestamp      : {logDetails}");
                    writer.WriteLine($"User           : {logDetails.User}");
                    writer.WriteLine($"SubArea        : {logDetails.SubArea}");
                    writer.WriteLine($"Indicator      : {logDetails.Indicator}");
                    writer.WriteLine($"File Names     : {string.Join(", ", logDetails.FileNames ?? new List<string>())}");
                    writer.WriteLine($"Error Message  : {logDetails.Exception}");
                    writer.WriteLine("-----------------------------------");
                    writer.WriteLine();
                }

                _logger.LogError("Error uploading credit report: {@LogDetails}", logDetails);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok(documentList);
        }

        //End change

      

        [HttpPost]
        [Route("Mediclaim/Raise/Upload_CaseSummary/{applicationSubArea}/{indicator}")]
        public IActionResult FileUpload_extended_date([FromRoute] string applicationSubArea, [FromRoute] int indicator, IList<IFormFile> files)
        {
            List<MediclaimDocumentModel> documentList = new List<MediclaimDocumentModel>();
            int _fileSize = 2 * 1024 * 1024;
            try
            {
                var dataJson = ReadJson.LoadJson();
                var cashlistDocumentList = dataJson.MediclaimCashlessDocumentList;

                //var _indicator = _raise.GetDocumentListForCreditLetter().Where(x => x.DocumentId == indicator).FirstOrDefault();
                var _indicator = "Case Summary";

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

                    var filename = Guid.NewGuid().ToString() + _indicator + fileextension;
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
                        DocumentFor = _indicator,
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

        //End change

        //credit letter Document delete
        [HttpPost]
        [Route("Mediclaim/Raise/DeleteFile1/{applicationSubArea}")]
        public IActionResult DeleteFile1([FromBody] string filePath, [FromRoute] string applicationSubArea)
        {
            try
            {
                // var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);
                path = path.Trim();
                path = Regex.Replace(path, @"[\x00-\x1F]", ""); // Remove control characters

                System.IO.File.Delete(path);
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok();
        }

        //end

        //add by rajan 14/04/25

        [Route("/Mediclaim/Raise/GetEmployeeAccount_data/{PPONumber2}")]
        [HttpGet]
        public IActionResult GetEmployeeAccount_data([FromRoute] string PPONumber2)
        {
            var _PPODetail = _raise.GetAccountData(PPONumber2);
            return Ok(_PPODetail);
        }

        //end new changes

    }
}
