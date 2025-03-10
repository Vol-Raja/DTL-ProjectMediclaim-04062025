using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models.GPF
{
    public class GPF_Loan
    {
        [Key]
        public int Id { get; set; }
        public string LoanType { get; set; }
        public string ApplicationNumber { get; set; }
        public string Organization { get; set; }
        public string EmployeeExternalCode { get; set; }
        public string ApplicationStatus { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal EligibleAmount { get; set; }
        public string LoanStatus { get; set; }
        public string Remark { get; set; }
        public string AG2Remark { get; set; }
        public string AMDMRemark { get; set; }
        public bool PaymentDone { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        [NotMapped]
        public GPF_Loan_Details Details { get; set; }
        [NotMapped]
        public GPF_Loan_BankDetails BankDetails { get; set; }
        [NotMapped]
        public IEnumerable<GPF_Loan_Documents> Documents { get; set; }
        [NotMapped]
        public GPFEmployeeDetail Employee { get; set; }
    }

    public class GPF_Loan_View : GPF_Loan
    {
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        [NotMapped]
        public string OrganisationName { get => GPFEmployeeInfoModel.OrganisationNameByCode.ContainsKey(this.Organization) ? GPFEmployeeInfoModel.OrganisationNameByCode[this.Organization] : null; }
    }

    public class GPF_Loan_View_Disburs : GPF_Loan_View
    {
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BIC { get; set; }
        public string IFSC { get; set; }
    }

    public class GPF_Loan_Details
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public string AccountNumber { get; set; }
        public decimal BasicPay { get; set; }
        public decimal Contribution { get; set; }
        public string LoanPurpose { get; set; }
        public decimal RateOfInterest { get; set; }
        public int NumEMI { get; set; }
        public DateTime? EMIStart { get; set; }
        public DateTime? EMIEnd { get; set; }
        public string PostingPlace { get; set; }
        public string Description { get; set; }
        public bool PreviousLoan { get; set; }
        public decimal? Prev_Amount { get; set; }
        public DateTime? Prev_DateOfWithdrawal { get; set; }
        public DateTime? Prev_DateOfRepayment { get; set; }
        public decimal? Prev_BalanceAmount { get; set; }
    }

    public class GPF_Loan_BankDetails
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BIC { get; set; }
        public string IFSC { get; set; }
    }

    public class GPF_Loan_Documents
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public string DocumentType { get; set; }
        public byte[] DocumentData { get; set; }
    }
}
