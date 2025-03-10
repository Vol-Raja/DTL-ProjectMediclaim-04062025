using DTL.Model.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class DisbursementReportModel
    {
        #region Properties

        public Int64 EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string EmployerName { get; set; }

        public string Bank { get; set; }

        public string Account { get; set; }

        public decimal Pension { get; set; }

        public decimal TDSAmount { get; set; }

        public decimal RecoveryAmount { get; set; }

        public decimal AllowanceAmount { get; set; }

        public decimal AdmissiblePension { get; set; }

        public decimal AQPAmount { get; set; }

        public string Month { get; set; }

        public IEnumerable<DTLOfficeList> DTLOfficeList { get; set; }

        public int PPONo { get; set; }

        #endregion
    }

    
}
