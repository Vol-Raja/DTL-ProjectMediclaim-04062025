using DTL.Model.Models.Mediclaim.Hospitalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTL.Model.Models.Mediclaim
{
    public class NonCashlessModel : BaseModel
    {
        public int ClaimId { get; set; }

        [Required(ErrorMessage = "Please enter employee number")]
        [Display(Name = "Employee Number")]
        public string EmployeeNumber { get; set; }

        [Required(ErrorMessage = "Please enter ppo number")]
        [Display(Name = "PPO Number")]
        public string PPONumber { get; set; }

        //[Required(ErrorMessage = "Please enter medical section page number")]
        [Display(Name = "Medical Section Page Number")]
        public string MedicalSectionPageNumber { get; set; }

        [Required(ErrorMessage = "Please enter medical card holder name")]
        [Display(Name = "Medical Card Holder Name")]
        public string MedicalCardHolderName { get; set; }

        //[Required(ErrorMessage = "Please enter medical card no.")]
        [Display(Name = "Medical Card No.")]
        public string MedicalCardNo { get; set; }

        //[Required(ErrorMessage = "Please enter designation")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Please enter patient name")]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Please enter gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        //[Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; } = new DateTime(1753, 1, 1);

        //[Required(ErrorMessage = "Please enter age")]
        [Display(Name = "Age")]
        public int Age { get; set; } = 0;

        [Required(ErrorMessage = "Please enter claim for")]
        [Display(Name = "Claim For")]
        public string ClaimFor { get; set; }

        [Display(Name = "Relation With Retire")]
        public string RelationWithRetire { get; set; }

        [Required(ErrorMessage = "Please enter mobile number")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Please enter card category")]
        [Display(Name = "Card Category")]
        public string CardCategory { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter claim type")]
        [Display(Name = "Claim Type")]
        public string ClaimType { get; set; }


        //[Required(ErrorMessage = "Please enter email id"), MaxLength(60)]
        [Display(Name = "Email Id")]
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please enter Organization")]
        [Display(Name = "Organization")]
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Organization { get; set; }

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

        //[Required(ErrorMessage = "Please enter bic code"), MaxLength(20)]  //chnage by rajan 14/04/25
        [Display(Name = "BIC Code")]
        public string BICCode { get; set; }

        //[Required(ErrorMessage = "Please enter ifsc number"), MaxLength(50)]  // chnage by rajan 14/04/25
        [Display(Name = "IFSC Number")]
        public string IFSCNumber { get; set; }
        public DependentInformation Dependent { get; set; }
        public bool MedicalCardPhotoCopy { get; set; }
        public bool PrescriptionDetailPhotoCopy { get; set; }
        public bool OriginalBill { get; set; }
        public bool CashMemo { get; set; }
        public int ClaimStatusId { get; set; }
        public bool PhysicalSubmit { get; set; }

        //[DisplayFormat(DataFormatString = "{0:#.##}")]
        public decimal ApprovedAmount { get; set; }
        public string RejectReason { get; set; } 
        public IList<OPDCNDModel> OPDCNDList { get; set; }

        public string DateOfBirthYYYYMMDD
        {
            get { return DateOfBirth.ToString("yyyy-MM-dd"); }
        }

        public decimal ApprovedAmountInDecimal
        {
            get { return decimal.Round(ApprovedAmount, 2); }
        }

        public bool IsDelete { get; set; } = false;

        public string CreatedDateString
        {
            get { return CreatedDate.ToString("yyyy-MM-dd"); }
        }
        public int VoucherId { get; set; }
        public string ForwardTo { get; set; }
        public string Remark { get; set; }
        public string DealingAssistanceRemark { get; set; }
        public string ASORemark { get; set; }
        public string SORemark { get; set; }
        public string AM_DMRemark { get; set; }
        public bool Disbursed { get; set; }
        public bool Paid { get; set; }

        public string ClaimNumber { get; set; }  // add by rajan 16/04/25 like (OPD/Dispensary/2025/April/0002)

        //OPDCND AMount
        public decimal TotalAmount { get; set; }
        public IEnumerable<MediclaimDocumentModel> Documents { get; set; }
        public DateTime startDate { get; set; } // add by nirbhay ExportToExcel 05/30/2025
        public DateTime endDate { get; set; }// add by nirbhay ExportToExcel 05/30/2025

    }
}
