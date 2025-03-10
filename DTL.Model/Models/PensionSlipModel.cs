using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class PensionSlipModel : BaseModel
    {
        [Required]
        public Guid EmployeeRegistrationId { get; set; }
        public decimal ABPLast10Months { get; set; }
        public decimal EmolumentsForPension { get; set; }
        public decimal DRPercent { get; set; }
        public decimal DR { get; set; }
        public decimal AdmissiablePension { get; set; }
        public DateTime? AdmissiableDate { get; set; }
        public decimal PensionEnhancedRate { get; set; }
        public DateTime? AdmissibleForFromDate_Enhanced { get; set; }
        public DateTime? AdmissibleForToDate_Enhanced { get; set; }
        public decimal PensionAtNormalRate { get; set; }
        public DateTime? AdmissibleForFromDate_Normal { get; set; }
        public DateTime? AdmissibleForToDate_Normal { get; set; }
        public decimal PSCommutation { get; set; }
        public decimal CommutedPortion { get; set; }
        public string GratuityType { get; set; }
        public decimal Gratuity { get; set; }

        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public DateTime? DOB { get; set; }

        public decimal? LeaveEncashment { get; set; }

        public decimal?	Taxable  { get; set; }

        public decimal? NonTaxable { get; set; }

        public int? LeaveEncashmentDays  { get; set; }

        public decimal? LumsumPayableCommutation { get; set; }

        public decimal? TaxableAmountCommutation { get; set; }

        public String ServicePeriod { get; set; }

        public decimal TaxableLeaveEncashment { get; set; }


    }
}
