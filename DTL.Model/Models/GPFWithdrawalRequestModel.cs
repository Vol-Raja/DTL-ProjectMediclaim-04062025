using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTL.Model.CommonModels;

namespace DTL.Model.Models
{
  public  class GPFWithdrawalRequestModel : BaseModel
    {
        public  Guid EmployeeRegistrationId { get; set; }
        public string Organization { get; set; }
        public string SubscriberName { get; set; }
        public string FatherName { get; set; }
        public string Designation { get; set; }
        public Int64 EmployeeId { get; set; }
        public decimal MonthlyPay { get; set; }
        public DateTime JoiningDate { get; set; }
        public string PurposeOfAdvance { get; set; }
        public string DependantName { get; set; }
        public int DependantAge { get; set; }
        public string DependantAddress { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string PlaceOfPosting { get; set; }
        public decimal RepaymentInstallment { get; set; }
        public DateTime DateOfCeremony { get; set; }
        public string ReasonForApplication { get; set; }
        public string BankAccountNo { get; set; }
        public string IFSC { get; set; }
        public string Branch { get; set; }
        public string BankName { get; set; }
        public byte[] SalarySlip { get; set; }
        public byte[] IDCard { get; set; }
        public string Submitted { get; set; }
        public string Accepted_Rejected { get; set; }
        public string Rejection_Reason { get; set; }

        public string SalarySlipDocPath { get; set; }

        public string IdCardDocPath { get; set; }
    }
}
