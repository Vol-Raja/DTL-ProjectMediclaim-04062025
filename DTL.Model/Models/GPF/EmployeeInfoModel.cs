using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models.GPF
{
    public class GPFEmployeeInfoModel
    {
        public string OrganisationCode { get; set; }
        public string ExternalCode { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Designation { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime? DOR { get; set; }
        public string RetirementStatus { get; set; }
        public int ppo_no { get; set; } //add by nirbhay

        [NotMapped]
        public string Organisation { get => GPFEmployeeInfoModel.OrganisationNameByCode.ContainsKey(this.OrganisationCode) ? GPFEmployeeInfoModel.OrganisationNameByCode[this.OrganisationCode] : null; }

        [NotMapped]
        public static Dictionary<string, string> OrganisationNameByCode = new Dictionary<string, string>
        {
            { "BRPL/C001", "BRPL - BSES Rajdhani Power Ltd" },
            { "BYPL/C001", "BYPL - BSES Yamuna Power Ltd" },
            { "DTL/C001", "DTL - Delhi Transco Ltd" },
            { "DPCL/C001", "DPCL - Delhi Power Company Limited" },
            { "IPGCL/C001", "IPGCL - Indraprastha Power Generation company Ltd" },
            { "NDPL/C001", "NDPL - North Delhi Power Ltd" },
            { "BSES/C001", "BSES - BSES" },
            { "SC/C001", "SUSPENSE COMPANY" },
        };
    }

    // 

    public class GPFEmployeeDetail
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string OrganisationCode { get; set; }

        [NotMapped]
        public GPFEmployeeDetail_Basic BasicDetails { get; set; }
        [NotMapped]
        public GPFEmployeeDetail_Bank BankDetails { get; set; }
        [NotMapped]
        public GPFEmployeeDetail_Address CurrentAddress { get; set; }
        [NotMapped]
        public GPFEmployeeDetail_Address PermanentAddress { get; set; }
        [NotMapped]
        public GPFEmployeeDetail_Family FamilyDetails { get; set; }
        [NotMapped]
        public GPFEmployeeDetail_Guardian FamilyGuardianDetails { get; set; }
        [NotMapped]
        public GPFEmployeeDetail_Nominee NomineeDetails { get; set; }
        [NotMapped]
        public GPFEmployeeDetail_Guardian NomineeGuardianDetails { get; set; }

        [NotMapped]
        public string Organisation { get => GPFEmployeeInfoModel.OrganisationNameByCode.ContainsKey(this.OrganisationCode) ? GPFEmployeeInfoModel.OrganisationNameByCode[this.OrganisationCode] : null; }
    }
    public class GPFEmployeeDetail_Basic
    {
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string FatherName { get; set; }
        public string ContactNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfLeaving { get; set; }
        public string MaritalStatus { get; set; }
        public string Level { get; set; }
        public decimal BasicPay { get; set; }
        public string BloodGroup { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string PayScale { get; set; }
        public string PhysicalDisability { get; set; }
        public string IdentificationMarks { get; set; }
        public DateTime? DateOfRetirement { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string EmployeeType { get; set; }
        public string GPFNumber { get; set; } 
    }

    public class GPFEmployeeDetail_Bank
    {
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }
        public string Address { get; set; }
    }

    public class GPFEmployeeDetail_Address
    {
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string Nominee { get; set; }
    }

    public class GPFEmployeeDetail_Family
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string MemberName { get; set; }
        public string Relationship { get; set; }
        public string PhysicalDisorder { get; set; }
        public string DateOfBirth { get; set; }
        public string GovtEmployee { get; set; }
        public string Eligible { get; set; }
        public string HNo { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Dis { get; set; }
        public string PinCode { get; set; }
        public string TransferToNominee { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string ContactNo { get; set; }
        public string MobileNo { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string PanNo { get; set; }
        public string Remarks { get; set; }
        public string LifeTimePension { get; set; }
        public string Reason { get; set; }
    }

    public class GPFEmployeeDetail_Guardian
    {
        public string GuardianName { get; set; }
        public string Relation { get; set; }
        public string HNo { get; set; }
        public string Locality { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
    }

    public class GPFEmployeeDetail_Nominee
    {
        public string EmployeeCode { get; set; }
        public string NomineeName { get; set; }
        public string Share { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Relationship { get; set; }
        public string RelationshipOther { get; set; }
        public string NomineeType { get; set; }
        public string Eligible { get; set; }
        public string AsPerShare { get; set; }
        public string HNo { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Dis { get; set; }
        public string PinCode { get; set; }
        public string Reason { get; set; }
    }

}
