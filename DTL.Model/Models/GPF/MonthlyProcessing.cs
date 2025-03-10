using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models.GPF
{
    public class MonthlyProcessingExcelSummary
    {
        public bool Status { get; set; } = true;
        public List<string> MissingColumns { get; set; }
        public int Success { get; set; }
        public int Failure { get; set; }
        public byte[] ProcessedExcelBytes { get; set; }
    }

    public class MonthlyGPFProcessedEntry
    {
        public DateTime ProcessingDate { get; set; }
        public string EmpCode { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string OrganisationCode { get; set; }
        public string Designation { get; set; }
        public string EmpType { get; set; }
        public decimal GPFContribution { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MemberInterest { get; set; }
        public DateTime Month_Year { get; set; }
        public decimal MonthlyGPFAmount { get; set; }
        public decimal GPFAccountBalance { get; set; }

        public string Organisation { get => GPFEmployeeInfoModel.OrganisationNameByCode.ContainsKey(this.OrganisationCode) ? GPFEmployeeInfoModel.OrganisationNameByCode[this.OrganisationCode] : null; }
    }

    public class MonthlyGPFProcessData
    {
        public MonthlyProcessingExcelSummary FileSummary { get; set; }
        public List<MonthlyGPFProcessedEntry> Entries { get; set; }
    }

    public class MonthlyGPFProcessModel
    {
        [Key]
        public int GPFProcessingId { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Employer { get; set; }
        public string Designation { get; set; }
        public decimal MemberContribution { get; set; }
        public decimal MemberInterest { get; set; }
        public decimal GPFAmount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string EmployeeType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal LoanAmount { get; set; }

        public static MonthlyGPFProcessModel FromExcelData(MonthlyGPFProcessedEntry entry, string createdBy)
        {
            return new MonthlyGPFProcessModel
            {
                EmployeeNumber =  entry.EmpCode,
                EmployeeName = entry.EmpName,
                Employer = entry.OrganisationCode,
                Designation = entry.Designation,
                MemberContribution = Convert.ToDecimal(entry.GPFContribution),
                MemberInterest = Convert.ToDecimal(entry.MemberInterest),
                GPFAmount = Convert.ToDecimal(entry.MonthlyGPFAmount),
                Month = entry.Month_Year.Month,
                Year = entry.Month_Year.Year,
                EmployeeType = entry.EmpType,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                ModifiedBy = createdBy,
                ModifiedDate = DateTime.Now,
                LoanAmount = 0m
            };
        }
    }

    public class MonthlyGPFProcessDataModel : MonthlyGPFProcessModel
    {
        public string EmployeeCode { get; set; }
        public decimal GPFSum { get; set; }

        [NotMapped]
        public string Organisation { get => GPFEmployeeInfoModel.OrganisationNameByCode.ContainsKey(this.Employer) ? GPFEmployeeInfoModel.OrganisationNameByCode[this.Employer] : null; }
    }

    public class GPFSummary
    {
        public string OrganisationCode { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal MemberContribution { get; set; }
        public decimal MemberInterest { get; set; }

        [NotMapped]
        public string Organisation { get => GPFEmployeeInfoModel.OrganisationNameByCode.ContainsKey(this.OrganisationCode) ? GPFEmployeeInfoModel.OrganisationNameByCode[this.OrganisationCode] : null; }
    }

    public class CurrentGPFBalance
    {
        public string OrganisationCode { get; set; }
        public string FinancialYear { get; set; }
        public string AccountNo { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime? DOJ { get; set; }
        public decimal GPFBalance { get; set; }

        [NotMapped]
        public string Organisation { get => GPFEmployeeInfoModel.OrganisationNameByCode.ContainsKey(this.OrganisationCode) ? GPFEmployeeInfoModel.OrganisationNameByCode[this.OrganisationCode] : null; }
    }

    public class GPFStatement
    {
        public string OrganisationCode { get; set; }
        public string FinancialYear { get; set; }
        public string AccountNo { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? DOJ { get; set; }
        public decimal Opening { get; set; }
        public decimal Closing { get; set; }
        public decimal GPFBalance { get; set; }
        public IEnumerable<MonthlyGPFProcessDataModel> MonthlyData { get; set; }

        [NotMapped]
        public string Organisation { get => GPFEmployeeInfoModel.OrganisationNameByCode.ContainsKey(this.OrganisationCode) ? GPFEmployeeInfoModel.OrganisationNameByCode[this.OrganisationCode] : null; }
    }
}
