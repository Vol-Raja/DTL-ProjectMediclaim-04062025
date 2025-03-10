using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models.GPF
{
    public class GPF_EDLIS
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public string Organization { get; set; }
        public string EmployeeExternalCode { get; set; }
        public string EDLISStatus { get; set; }
        public decimal? EDLISAmount { get; set; }
        public DateTime? SettlementDate { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public DateTime? DateOfRetirement { get; set; }
        public bool? TransferToNominee { get; set; }
        public string? TransferToNomineeReason { get; set; }
        public decimal? FinalSettlementAmount { get; set; }
        public string? ReasonOfDeath { get; set; }
        public bool PaymentDone { get; set; }
        public string? Remark { get; set; }
        public string? AG2Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        [NotMapped]
        public IEnumerable<GPF_EDLIS_Nominee> Nominees { get; set; }
        [NotMapped]
        public IEnumerable<GPF_EDLIS_Family> Family { get; set; }
        [NotMapped]
        public GPF_EDLIS_BankDetails BankDetails { get; set; }
        [NotMapped]
        public IEnumerable<GPF_EDLIS_Documents> Documents { get; set; }
        [NotMapped]
        public GPFEmployeeDetail Employee { get; set; }
    }

    public class GPF_EDLIS_View : GPF_EDLIS
    {
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string GPFAccountNumber { get; set; }
        [NotMapped]
        public string OrganisationName { get => GPFEmployeeInfoModel.OrganisationNameByCode.ContainsKey(this.Organization) ? GPFEmployeeInfoModel.OrganisationNameByCode[this.Organization] : null; }
    }

    public class GPF_EDLIS_View_Disburs : GPF_EDLIS_View
    {
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNumber { get; set; }
        public string BIS { get; set; }
        public string IFSC { get; set; }
    }

    public class GPF_EDLIS_BankDetails
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BIS { get; set; }
        public string IFSC { get; set; }
    }

    public class GPF_EDLIS_Nominee
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Relationship { get; set; }
        public decimal? Share { get; set; }
        public decimal? AsPerShare { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string NomineeType { get; set; }
        public string GuardianName { get; set; }
        public string GuardianRelationship { get; set; }
        public string GuardianContact { get; set; }
        public string GuardianAddress { get; set; }
    }

    public class GPF_EDLIS_Family
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Relationship { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Pincode { get; set; }
    }

    public class GPF_EDLIS_Documents
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public string DocumentType { get; set; }
        public byte[] DocumentData { get; set; }
    }
}
