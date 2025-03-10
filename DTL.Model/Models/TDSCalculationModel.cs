using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{

    public class TDSCalculationModel : BaseModel
    {
        [Required]
        public Guid EmployeeRegistrationId { get; set; }
        public int FinancialYear { get; set; }
        public string Pensioner { get; set; }
        public decimal Pension { get; set; }
        public decimal Gratuity { get; set; }
        public decimal Commutation { get; set; }
        public decimal LeaveEncashment { get; set; }

        public decimal AQP { get; set; }
        public decimal DA { get; set; }
        public decimal HRA { get; set; }
        public decimal Travel_Allowance { get; set; }
        public decimal Medical_Allowance { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal ExemptedAmount { get; set; }
        public decimal Ex80DD { get; set; }
        public decimal Inv80D { get; set; }
        public decimal Inv80DD { get; set; }
        public decimal Inv80DDB { get; set; }
        public byte[] InvFile80D { get; set; }
        public byte[] InvFile80DD { get; set; }
        public byte[] InvFile80DDB { get; set; }

        public string InvFilePath80D { get; set; }
        public string InvFilePath80DD { get; set; }
        public string InvFilePath80DDB { get; set; }


        public decimal Ex80C { get; set; }
        public decimal Mediclaim { get; set; }
        public decimal HomeLoan { get; set; }
        public decimal IntOnEduLoan { get; set; }
        public decimal Donation { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal StandardDeduction { get; set; }
        public decimal NetTaxableAmount { get; set; }
        public decimal TaxPayable { get; set; }

        public decimal Cess { get; set; }
        public decimal MonthlyTDSAmount { get; set; }

        public string Regime { get; set; }
    }
}
