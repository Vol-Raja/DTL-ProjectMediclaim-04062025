using System;

namespace DTL.Model.Models.Mediclaim
{
    public class MedicalCardModel:BaseModel
    {
        public int MedicalCardId { get; set; }
        public string CardNo { get; set; }
        public string EmployeeNo { get; set; }
        public string PPONo { get; set; }
        public string NameOfCardHolder { get; set; }
        public string Employer { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int MedicalSectionPageNo { get; set; }
        public string CardCategory { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string MedicalHistory { get; set; }
        public string BankName { get; set; }
        public string BICCode { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNumber { get; set; }
        public bool IsDelete { get; set; }
        public string NameOfDependent { get; set; }
        public string RelationWithRetiree { get; set; }
        public DateTime DependentDob { get; set; }

        public string DateOfBirthYYYYMMDD
        {
            get { return DateOfBirth.ToString("yyyy-MM-dd"); }
        }

        public string DependentDobYYYYMMDD
        {
            get { return DependentDob.ToString("yyyy-MM-dd"); }
        }
    }
}
