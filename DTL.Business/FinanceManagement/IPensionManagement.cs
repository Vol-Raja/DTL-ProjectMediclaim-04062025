using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.FinanceManagement
{
    public interface IPensionManagement
    {
        IEnumerable<PensionManagementModel> GetPensionManagementReport(DateTime dt, string EmployerName, string EmployeeID, string operation = null);
    }
}
