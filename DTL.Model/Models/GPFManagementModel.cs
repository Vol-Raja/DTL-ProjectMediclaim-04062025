using DTL.Model.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class GPFManagementModel :BaseModel
    {
        #region Properties

        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string Employer { get; set; }

        public string Month { get; set; }

        public decimal GPFWithdrawal { get; set; }

        public decimal GPFContribution { get; set; }

        public IEnumerable<DTLOfficeList> DTLOfficeList { get; set; }

        #endregion
    }
}
