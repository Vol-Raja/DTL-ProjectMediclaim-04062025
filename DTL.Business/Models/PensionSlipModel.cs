using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Models
{
    public class PensionSlipModel
    {
        public int ABPLast10Months { get; set; }
        public int EmolumentsForPension { get; set; }
        public DateTime AdmissiableDate { get; set; }
        public int PensionEnhancedRate { get; set; }
        public DateTime AdmissibleForFromDate_Enhanced { get; set; }
        public DateTime AdmissibleForToDate_Enhanced { get; set; }
        public int PensionAtNormalRate { get; set; }
        public DateTime AdmissibleForFromDate_Normal { get; set; }
        public DateTime AdmissibleForToDate_Normal { get; set; }
        public int Commutation { get; set; }
        public int CommutedPortion { get; set; }
        public string GratuityType { get; set; }
        public int Gratuity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifideDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifideBy { get; set; }
    }
}
