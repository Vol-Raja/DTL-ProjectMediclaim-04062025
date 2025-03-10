using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.PensionSlip
{
    public interface IPensionSlipService
    {
        FinancialPensionModel GetPensionDataByFinanacialYear(Guid EmpRegId, int FinYear);
    }

}
