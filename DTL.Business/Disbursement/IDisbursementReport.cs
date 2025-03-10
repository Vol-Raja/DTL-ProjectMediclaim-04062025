using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Disbursement
{
    public interface IDisbursementReport
    {
        IEnumerable<DisbursementReportModel> GetDisbursementReportData(DateTime dt, string EmployerName, string EmployeeID);
    }
}
