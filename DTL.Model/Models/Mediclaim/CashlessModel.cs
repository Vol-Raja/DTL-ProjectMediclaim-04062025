using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTL.Model.Models.Mediclaim
{
    public class CashlessModel : BaseModel
    {
       // public int Id { get; set; }
        public int ClaimId { get; set; }
        public string EMP_NAME { get; set; }
        //public Guid CL_Id { get; set; }
        public string PatientName { get; set; }
        public string PensionerName { get; set; }
        public string Relation { get; set; }
        public int EmployeeNo { get; set; }
        public int PageNo { get; set; }
        public string CategoryOfRoom { get; set; }
        public string NameOfDoctor { get; set; }
        public string PatientNo { get; set; }
        public DateTime IssueDate1 { get; set; }
        public DateTime IssueDate { get; set; }
        public string ResidentAddress { get; set; }
        public string ProvisionalDiagnosis { get; set; }
        public string IstendedTreatment { get; set; }
        public string DurationOfStay { get; set; }
        //public DateTime DateOfAdmission { get; set; }
        //public DateTime DateOfDischargeOrDeath { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string SignatureDischargeTime { get; set; }

        public string email_id { get; set; }
        public string Mobile_no { get; set; }
        public int PPONumber1 { get; set; }
        public string phonenumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public int HospitalUserId { get; set; }

        [Required(ErrorMessage = "Please enter hospital name"), MaxLength(100)]
        [Display(Name = "Name Of Hospital")]
        public string NameOfHospital { get; set; }

        [Required(ErrorMessage = "Please enter hospital phone number"), MaxLength(16)]
        [Display(Name = "Hospital Phone Number")]
        public string HospitalPhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter hospital address"), MaxLength(250)]
        [Display(Name = "Hospital Address")]
        public string HospitalAddress { get; set; }

        [Required(ErrorMessage = "Please enter hospital id"), MaxLength(15)]
        [Display(Name = "Hospital Id")]
        public string HospitalId { get; set; }

        [Required(ErrorMessage = "Please enter email id"), MaxLength(50)]
        [Display(Name = "Email Id")]
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please enter name of patient"), MaxLength(50)]
        [Display(Name = "Name Of Patient")]
        public string NameOfPatient { get; set; }

        [Required(ErrorMessage = "Please patient enter email id"), MaxLength(150)]
        [Display(Name = "Patient Email Id")]
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string PatientEmailId { get; set; }

        [Required(ErrorMessage = "Please enter gender"), MaxLength(6)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter patient phone number"), MaxLength(50)]
        [Display(Name = "Patient Phone Number")]
        public string PatientPhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter patient address"), MaxLength(250)]
        [Display(Name = "Patient Address")]
        public string PatientAddress { get; set; }

        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter age"), MaxLength(5)]
        [Display(Name = "Age")]
        public string Age { get; set; }

        [Required(ErrorMessage = "Please enter medical section page number"), MaxLength(50)]
        [Display(Name = "Medical Section Pagenumber")]
        public string MedicalSectionPageNumber { get; set; }

        [Required(ErrorMessage = "Please enter name of card holder"), MaxLength(80)]
        [Display(Name = "Name Of Card Holder")]
        public string NameOfCardHolder { get; set; }

        [Required(ErrorMessage = "Please enter medical card number"), MaxLength(16)]
        [Display(Name = "Medical Card Number")]
        public string MedicalCardNumber { get; set; }

        [Required(ErrorMessage = "Please enter admission number"), MaxLength(50)]
        [Display(Name = "Admission Number")]
        public string AdmissionNumber { get; set; }

        [Required(ErrorMessage = "Please enter card category"), MaxLength(20)]
        [Display(Name = "Card Category")]
        public string CardCategory { get; set; }

        [Required(ErrorMessage = "Please enter card category"), MaxLength(50)]
        [Display(Name = "Case Type")]
        public string CaseType { get; set; }

        [Required(ErrorMessage = "Please enter type of treatment"), MaxLength(50)]
        [Display(Name = "Type Of Treatment")]
        public string TypeOfTreatment { get; set; }

        [Required(ErrorMessage = "Please enter amount")]
        [Display(Name = "Amount")]
        //[DisplayFormat(DataFormatString = "{0:#.##}")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please enter date of admission")]
        [Display(Name = "Date Of Admission")]
        public DateTime DateOfAdmission { get; set; }

        [Required(ErrorMessage = "Please enter date of discharge or death")]
        [Display(Name = "Date Of Discharge Or Death")]
        public DateTime DateOfDischargeOrDeath { get; set; }


        [Required(ErrorMessage = "Please enter account holder name"), MaxLength(50)]
        [Display(Name = "Acount HolderName")]
        public string AccountHolderName { get; set; }


        [Required(ErrorMessage = "Please enter account number"), MaxLength(20)]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }


        [Required(ErrorMessage = "Please enter bank name"), MaxLength(150)]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Please enter branch name"), MaxLength(150)]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "Please enter bic code"), MaxLength(20)]
        [Display(Name = "BIC Code")]
        public string BICCode { get; set; }

        [Required(ErrorMessage = "Please enter ifsc number"), MaxLength(50)]
        [Display(Name = "IFSC Number")]
        public string IFSCNumber { get; set; }

        [Required(ErrorMessage = "Please enter relationship with retiree"), MaxLength(50)]
        [Display(Name = "relationship with retiree")]
        public string RelationWithRetire { get; set; }

        [Required(ErrorMessage = "Please enter Date Of Birth")]
        [Display(Name = " Date Of Birth ")]
        public DateTime DependantDOB { get; set; }

        [Required(ErrorMessage = "Please enter Age"), MaxLength(50)]
        [Display(Name = "Age")]
        public string DependantAge { get; set; }

        public int Status { get; set; } = 1;
        public int StatusCreditId { get; set; } = 1;
        public int ClaimStatusId { get; set; } = 1;
        public bool PhysicalSubmit { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string RejectReason { get; set; }

        public string CreatedDateString
        {
            get { return CreatedDate.ToString("yyyy-MM-dd"); }
        }
        public string DependantDOBString
        {
            get { return DependantDOB.ToString("yyyy-mm-dd"); }
        }   
        public string DOBString
        {
            get { return DateOfBirth.ToString("yyyy-mm-dd"); }
        }
        //public string DependantDOBString
        //{
        //    get { return DependantDOB.ToString("yyyy-MM-dd"); }
        //}

        public string DateOfAdmissionString
        {
            get { return DateOfAdmission.ToString("yyyy-MM-dd"); }
        }
        public string DateOfDischargeOrDeathString
        {
            get { return DateOfDischargeOrDeath.ToString("yyyy-MM-dd"); }
        }
        public decimal ApprovedAmountInDecimal
        {
            get { return decimal.Round(ApprovedAmount, 2); }
        }

        public int VoucherId { get; set; }
        public string ForwardTo { get; set; }
        public string ApproveRemark { get; set; }        
        public string CMORemark { get; set; }
        public string DealingAssistanceRemark { get; set; }
        public string ASORemark { get; set; }
        public string SORemark { get; set; }
        public string AM_DMRemark { get; set; }
        public string Remark { get; set; }
        public IEnumerable<MediclaimDocumentModel> Documents { get; set; }
        public int PPONumber { get; set; }
        public string Organization { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfRetirement { get; set; }

        public string DateOfRetirementString
        {
            get { return DateOfRetirement.ToString("yyyy-MM-dd"); }
        }
        public bool IsDelete { get; set; } = false;

        public bool Disbursed { get; set; }
        public bool Paid { get; set; }
        public string SerialNo { get; set; }
        public string CASE_TYPE { get; set; }
        public string Doctor_NO { get; set; }

    }
}
