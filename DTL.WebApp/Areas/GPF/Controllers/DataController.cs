using DTL.Business.GPF.EDLIS;
using DTL.Business.GPF.EmployeeInfo;
using DTL.Business.GPF.GeneratePayment;
using DTL.Business.GPF.LoanProcessing;
using DTL.Business.GPF.Settlement;
using DTL.Business.UserManagement;
using DTL.Model.Models.GPF;
using DTL.Model.Models.UserManagement;
using DTL.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    [Area("GPF")]
    [Authorize(Roles = "DVB Pension Trust,Admin")]
    public class DataController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeInfo _empInfo;
        private readonly IMonthlyProcessing _monthlyProcess;
        private readonly ILoanProcessing _loanProcess;
        private readonly ISettlement _settlement;
        private readonly IEDLIS _edlis;

        public DataController(UserManager<ApplicationUser> userManager, IEmployeeInfo empInfo, IMonthlyProcessing monthlyProcessing, ILoanProcessing loanProcessing, ISettlement settlement, IEDLIS edlis)
        {
            _userManager = userManager;
            _empInfo = empInfo;
            _monthlyProcess = monthlyProcessing;
            _loanProcess = loanProcessing;
            _settlement = settlement;
            _edlis = edlis;
        }

        #region Employee Info

        public IActionResult GetEmployeeData(string orgCode = "all", string retirementStatus = "all")
        {
            IEnumerable<GPFEmployeeInfoModel> data = _empInfo.Get(orgCode, retirementStatus);
            return Ok(data);
        }

        public IActionResult GetEmployeeDetails(string externalCode)
        {
            GPFEmployeeDetail data = _empInfo.GetEmployee(externalCode);
            return Ok(data);
        }

        public IActionResult EmployeeAutocomplete(string term = "", string orgCode = "all")
        {
            var res = _empInfo.EmployeeAutocomplete(orgCode, term);
            return Ok(res);
        }

        #endregion

        #region Monthly GPF Processing

        public IActionResult ValidateExcelData()
        {
            byte[] fileBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                var file = Request.Form.Files.FirstOrDefault();
                if (file == null) return Ok(new { Status = false, Message = "File not attached" });
                file.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                fileBytes = ms.ToArray();
            }
            try
            {
                MonthlyProcessingExcelSummary res = _monthlyProcess.ValidateExcel(fileBytes).FileSummary;
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(new { Status = false, Message = "Failed to parse Excel File.", Error = ex.ToString() });
            }
        }

        public IActionResult ExcelSample(string orgCode)
        {
            if (string.IsNullOrEmpty(orgCode)) return Ok(new { Status = false });
            var xlBytes = _monthlyProcess.SampleExcel(orgCode);
            return Ok(new { Status = true, Data = Convert.ToBase64String(xlBytes) });
        }

        public IActionResult UploadExcelData()
        {
            byte[] fileBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                var file = Request.Form.Files.FirstOrDefault();
                if (file == null) return Ok(new { Status = false, Message = "File not attached" });
                file.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                fileBytes = ms.ToArray();
            }
            Func<string, object> err = (string msg) => { return new { Status = false, Message = msg }; };
            string organizationCode, employeeType;
            DateTime processingDate;
            decimal interestRate;
            try { organizationCode = Request.Form["organizationCode"].ToString(); } catch { return Ok(err("Invalid Organization Code")); }
            try { employeeType = Request.Form["employeeType"].ToString(); } catch { return Ok(err("Invalid Employee Type")); }
            try { processingDate = DateTime.Parse(Request.Form["processingDate"].ToString()); } catch { return Ok(err("Invalid Processing Date")); }
            try { interestRate = decimal.Parse(Request.Form["interestRate"].ToString()); } catch { return Ok(err("Invalid Interest Rate")); }
            try
            {
                MonthlyGPFProcessData res = _monthlyProcess.ValidateExcel(fileBytes);
                for (int i = 0; i < res.Entries.Count; i++)
                {
                    res.Entries[i] = _monthlyProcess.ProcessEntry(res.Entries[i], processingDate, interestRate, employeeType);
                    _monthlyProcess.SaveEntry(MonthlyGPFProcessModel.FromExcelData(res.Entries[i], User.Identity.Name));
                    res.Entries[i].GPFAccountBalance = _monthlyProcess.GetGPFSum(res.Entries[i].OrganisationCode, res.Entries[i].EmpCode);
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(new { Status = false, Message = "Failed to parse Excel File.", Error = ex.ToString() });
            }
        }

        #endregion

        #region GPF Report

        public IActionResult GetData(string orgCode = "all", string empCode = "all", DateTime? dtFrom = null, DateTime? dtTo = null)
        {
            var data = _monthlyProcess.GetData(orgCode, empCode, dtFrom, dtTo);
            return Ok(data);
        }

        #endregion

        #region GPF Summary

        public IActionResult GetSummary(string orgCode = "all", DateTime? dtFrom = null, DateTime? dtTo = null)
        {
            var data = _monthlyProcess.GetSummary(orgCode, dtFrom, dtTo);
            return Ok(data);
        }

        public IActionResult GetCurrentBalance(string orgCode, string accNo, int year)
        {
            var data = _monthlyProcess.GetCurrentBalance(orgCode, accNo, year);
            return Ok(data);
        }

        #endregion

        #region GPF Statement

        public IActionResult GetStatement(string orgCode, string accNo, int year)
        {
            var data = _monthlyProcess.GetStatement(orgCode, accNo, year);
            return Ok(data);
        }

        #endregion

        #region GPF Loan

        public IActionResult SaveLoan(GPF_Loan obj)
        {
            var json = "";
            using (StreamReader sr = new StreamReader(Request.Body))
                json = sr.ReadToEndAsync().Result;
            obj = JsonConvert.DeserializeObject<GPF_Loan>(json);
            obj.CreatedBy = User.Identity.Name;
            var data = _loanProcess.SaveLoanApplication(obj);
            if (data != null)
            {
                return Ok(new { Status = true, Data = data });
            }
            else
            {
                return Ok(new { Status = false });
            }
        }

        public IActionResult SaveLoanDocument()
        {
            var file = Request.Form.Files[0];
            var fileName = Request.Form["FileName"].ToString();
            var applicationNo = Request.Form["ApplicationNumber"].ToString();
            byte[] fileBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                fileBytes = ms.ToArray();
            }

            var data = _loanProcess.SaveLoanApplicationDoc(new GPF_Loan_Documents
            {
                ApplicationNumber = applicationNo,
                DocumentType = fileName,
                DocumentData = fileBytes
            });
            if (data > 0)
                return Ok(new { Status = true });
            else
                return Ok(new { Status = false });
        }

        public IActionResult GetLoanApplications(string type, string status = "all")
        {
            var data = _loanProcess.GetLoanApplications(type, status);
            return Ok(data);
        }

        public IActionResult GetLoanApplicationsByEmployee(string externalCode, string type = "all", string status = "all")
        {
            var data = _loanProcess.GetLoanApplicationsByEmployee(externalCode, type, status);
            return Ok(data);
        }

        public IActionResult GetLoanApplicationsDisburs(string type)
        {
            var data = _loanProcess.GetLoanApplicationsDisburs(type);
            return Ok(data);
        }

        public IActionResult GetLoanApplication(string applicationNumber)
        {
            var data = _loanProcess.GetLoanApplication(applicationNumber);
            return Ok(data);
        }

        public IActionResult UpdateLoanWithdrawal(string applicationNumber, bool approve, string remarks)
        {
            var data = _loanProcess.UpdateLoanApplicationStatus(new GPF_Loan
            {
                ApplicationNumber = applicationNumber,
                Remark = remarks,
                ApplicationStatus = approve ? "Pending with AG II" : "Rejected",
                LoanStatus = approve ? "Pending" : "Rejected"
            });
            return Ok(new { Status = data > 0 });
        }

        public IActionResult UpdateLoanAG2(string applicationNumber, bool approve, string remarks)
        {
            var data = _loanProcess.UpdateLoanApplicationStatus(new GPF_Loan
            {
                ApplicationNumber = applicationNumber,
                AG2Remark = remarks,
                ApplicationStatus = approve ? "Pending with AM / DM" : "Rejected",
                LoanStatus = approve ? "Pending" : "Rejected"
            });
            return Ok(new { Status = data > 0 });
        }

        public IActionResult UpdateLoanAMDM(string[] applicationNumber)
        {
            int i = 0;
            foreach (var application in applicationNumber)
            {
                i += _loanProcess.UpdateLoanApplicationStatus(new GPF_Loan
                {
                    ApplicationNumber = application,
                    ApplicationStatus = "Pending with Disbursement",
                    LoanStatus = "Pending"
                });
            }
            return Ok(new { Status = i > 0 });
        }

        public IActionResult UpdateLoanDisburs(string applicationNumber)
        {
            int i = _loanProcess.UpdateLoanApplicationPaid(new GPF_Loan
            {
                ApplicationNumber = applicationNumber,
            });
            return Ok(new { Status = i > 0 });
        }


        #endregion

        #region GPF Settlement

        public IActionResult SaveSettlement(GPF_Settlement obj)
        {
            var json = "";
            using (StreamReader sr = new StreamReader(Request.Body))
                json = sr.ReadToEndAsync().Result;
            obj = JsonConvert.DeserializeObject<GPF_Settlement>(json);
            obj.CreatedBy = User.Identity.Name;
            var data = _settlement.SaveSettlementApplication(obj);
            if (data != null)
            {
                return Ok(new { Status = true, Data = data });
            }
            else
            {
                return Ok(new { Status = false });
            }
        }

        public IActionResult SaveSettlementDocument()
        {
            var file = Request.Form.Files[0];
            var fileName = Request.Form["FileName"].ToString();
            var applicationNo = Request.Form["ApplicationNumber"].ToString();
            byte[] fileBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                fileBytes = ms.ToArray();
            }

            var data = _settlement.SaveSettlementApplicationDoc(new GPF_Settlement_Documents
            {
                ApplicationNumber = applicationNo,
                DocumentType = fileName,
                DocumentData = fileBytes
            });
            if (data > 0)
                return Ok(new { Status = true });
            else
                return Ok(new { Status = false });
        }

        public IActionResult GetSettlementApplications(string status = "all", string applicationNumber = "", string employeeCode = "", string organization = "all")
        {
            var data = _settlement.GetSettlementApplications(status, applicationNumber, employeeCode, organization);
            return Ok(data);
        }

        public IActionResult GetSettlementApplication(string applicationNumber)
        {
            var data = _settlement.GetSettlementApplication(applicationNumber);
            return Ok(data);
        }

        public IActionResult GetSettlementApplicationsDisburs()
        {
            var data = _settlement.GetSettlementApplicationsDisburs();
            return Ok(data);
        }

        public IActionResult UpdateSettlementWithdrawal(string applicationNumber, decimal? finalSettlementAmount, bool approve, string remarks)
        {
            var data = _settlement.UpdateSettlementApplicationStatus(new GPF_Settlement
            {
                ApplicationNumber = applicationNumber,
                FinalSettlementAmount = finalSettlementAmount,
                Remark = remarks, 
                SettlementStatus = approve ? "Pending with AG II" : "Rejected"
            });
            return Ok(new { Status = data > 0 });
        }

        public IActionResult UpdateSettlementAG2(string applicationNumber, bool approve, string remarks)
        {
            var data = _settlement.UpdateSettlementApplicationStatus(new GPF_Settlement
            {
                ApplicationNumber = applicationNumber,
                AG2Remark = remarks,
                SettlementStatus = approve ? "Pending with AM / DM" : "Rejected"
            });
            return Ok(new { Status = data > 0 });
        }

        public IActionResult UpdateSettlementAMDM(string[] applicationNumber)
        {
            int i = 0;
            foreach (var application in applicationNumber)
            {
                i += _settlement.UpdateSettlementApplicationStatus(new GPF_Settlement
                {
                    ApplicationNumber = application,
                    SettlementStatus = "Pending with Disbursement",
                });
            }
            return Ok(new { Status = i > 0 });
        }
        
        public IActionResult UpdateSettlementDisburs(string applicationNumber)
        {
            int i = _settlement.UpdateSettlementApplicationPaid(new GPF_Settlement
            {
                ApplicationNumber = applicationNumber,
            });
            return Ok(new { Status = i > 0 });
        }

        #endregion

        #region GPF EDLIS

        public IActionResult SaveEDLIS(GPF_EDLIS obj)
        {
            var json = "";
            using (StreamReader sr = new StreamReader(Request.Body))
                json = sr.ReadToEndAsync().Result;
            obj = JsonConvert.DeserializeObject<GPF_EDLIS>(json);
            obj.CreatedBy = User.Identity.Name;
            var data = _edlis.SaveEDLISApplication(obj);
            if (data != null)
            {
                return Ok(new { Status = true, Data = data });
            }
            else
            {
                return Ok(new { Status = false });
            }
        }

        public IActionResult SaveEDLISDocument()
        {
            var file = Request.Form.Files[0];
            var fileName = Request.Form["FileName"].ToString();
            var applicationNo = Request.Form["ApplicationNumber"].ToString();
            byte[] fileBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                fileBytes = ms.ToArray();
            }

            var data = _edlis.SaveEDLISApplicationDoc(new GPF_EDLIS_Documents
            {
                ApplicationNumber = applicationNo,
                DocumentType = fileName,
                DocumentData = fileBytes
            });
            if (data > 0)
                return Ok(new { Status = true });
            else
                return Ok(new { Status = false });
        }

        public IActionResult GetEDLISApplications(string status = "all", string applicationNumber = "", string employeeCode = "", string organization = "all")
        {
            var data = _edlis.GetEDLISApplications(status, applicationNumber, employeeCode, organization);
            return Ok(data);
        }

        public IActionResult GetEDLISApplication(string applicationNumber)
        {
            var data = _edlis.GetEDLISApplication(applicationNumber);
            return Ok(data);
        }

        public IActionResult GetEDLISApplicationsDisburs()
        {
            var data = _edlis.GetEDLISApplicationsDisburs();
            return Ok(data);
        }

        public IActionResult UpdateEDLISWithdrawal(string applicationNumber, decimal? finalSettlementAmount, bool approve, string remarks)
        {
            var data = _edlis.UpdateEDLISApplicationStatus(new GPF_EDLIS
            {
                ApplicationNumber = applicationNumber,
                FinalSettlementAmount = finalSettlementAmount,
                Remark = remarks, 
                EDLISStatus = approve ? "Pending with AG II" : "Rejected"
            });
            return Ok(new { Status = data > 0 });
        }

        public IActionResult UpdateEDLISAG2(string applicationNumber, bool approve, string remarks)
        {
            var data = _edlis.UpdateEDLISApplicationStatus(new GPF_EDLIS
            {
                ApplicationNumber = applicationNumber,
                AG2Remark = remarks,
                EDLISStatus = approve ? "Pending with AM / DM" : "Rejected"
            });
            return Ok(new { Status = data > 0 });
        }

        public IActionResult UpdateEDLISAMDM(string[] applicationNumber)
        {
            int i = 0;
            foreach (var application in applicationNumber)
            {
                i += _edlis.UpdateEDLISApplicationStatus(new GPF_EDLIS
                {
                    ApplicationNumber = application,
                    EDLISStatus = "Pending with Disbursement",
                });
            }
            return Ok(new { Status = i > 0 });
        }
        
        public IActionResult UpdateEDLISDisburs(string applicationNumber)
        {
            int i = _edlis.UpdateEDLISApplicationPaid(new GPF_EDLIS
            {
                ApplicationNumber = applicationNumber,
            });
            return Ok(new { Status = i > 0 });
        }

        #endregion
    }
}
