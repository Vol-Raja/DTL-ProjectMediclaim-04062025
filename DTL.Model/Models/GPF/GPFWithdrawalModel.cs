using System;
using System.Collections;
using System.Collections.Generic;

namespace DTL.Model.Models.GPF
{
    public class GPFWithdrawalModel : BaseModel
    {
		public int WithdrawId { get; set; }
		public string WithdrawType { get; set; }
		public string AccountHolderName { get; set; }
		public string EmployeId { get; set; }
		public string Designation { get; set; }
		public string Employer { get; set; }
		public decimal MonthlyGPFPay { get; set; }
		public DateTime DateOfJoining { get; set; }
		public string PurposeOfWithdrawal { get; set; }
		public string MobileNumber { get; set; }
		public string DependantsName { get; set; }
		public float DependentsAge { get; set; }
		public string DependentsAddress { get; set; }
		//Non Refundable--
		public decimal TotalGPFContribution { get; set; }
		public decimal TotalWithdrawalAmount { get; set; }
		public decimal RemainingContribution { get; set; }
		//Refundable--
		public decimal TotalAdvancedAmount { get; set; }
		public int NoOfEMI { get; set; }
		public int MonthlyDeduction { get; set; }
		public string PurposeOfRefundable { get; set; }
		public string PostingLastThreeYear { get; set; }
		public DateTime DateOfApplication { get; set; }
		public string ReasonOfAdvance { get; set; }
		public string BankAccountNo { get; set; }
		public string IFSCNo { get; set; }
		public string BranchName { get; set; }
		public string BC { get; set; }
		public string BankName { get; set; }
		public bool PhysicalSubmit { get; set; }
		public int ApplicationStatus { get; set; }
		public string RejectReason { get; set; }
		public string UniqueNumber { get; set; }
		public string OperationType { get; set; }
		public string DateOfApplicationString
		{
			get { return DateOfApplication.ToShortDateString(); }
		}

		public string DateOfApplicationYYYYMMDD
		{
			get { return DateOfApplication.ToString("yyyy-MM-dd"); }
		}
		public string DateOfJoiningYYYYMMDD
		{
			get { return DateOfJoining.ToString("yyyy-MM-dd"); }
		}
		public decimal ApprovedAmount { get; set; }

		public IEnumerable<GPFDocumentModel> GPFDocuments { get; set; }

	}
}
