using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class GeneratePensionModel
    {
        public int PPOOrderId { get; set; }
        public string EmployeeName { get; set; }
        public string DTLOffice { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public decimal AdmissiablePension { get; set; }
        //public int Year { get; set; }
        //public int Month { get; set; }
        public DateTime AdmissibleForFromDate_Normal { get; set; }
        public string AdmissibleForFromDateMMYYYY 
        { 
            get { return AdmissibleForFromDate_Normal.ToString("MM/yyyy"); } 
        }
    }
}
