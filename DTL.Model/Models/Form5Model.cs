using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class Form5Model : BaseModel
    {
        [Required]
        public Guid EmployeeRegistrationId { get; set; }
        public string Aadhar { get; set; }
        public string PAN { get; set; }

        public string BankAccountNumber { get; set; }
        public string Bank { get; set; }

        public string BankAccountName { get; set; }
        public string BankAddress { get; set; }
        public string IFSCCode { get; set; }
        public string BICCode { get; set; }
        public string IdentificationMark { get; set; }
        public string PanDocPath { get; set; }
        public string AadharDocPath { get; set; }
        public byte[] SpecimenSignature1 { get; set; }
        public byte[] SpecimenSignature2 { get; set; }
        public byte[] SpecimenSignature3 { get; set; }
        public byte[] ThumbImpression1 { get; set; }
        public byte[] ThumbImpression2 { get; set; }
        public byte[] AadharCard { get; set; }
        //public byte[] AadharBackSideImage { get; set; }
        public byte[] PanCard { get; set; }
    }
}
