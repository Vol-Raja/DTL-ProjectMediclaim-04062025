using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTL.Model.Models;

namespace DTL.Business.Disbursement
{
    public interface IAddTDSCalculation
    {

        Guid AddEditTDSCalculation(TDSCalculationModel tDSCalculationModel, bool isEdit,TDSInvestmentModel tDSInvestmentModel);

        TDSCalculationModel GetTDSCalculationDetails(Guid EmpRegId, int FinYear);
    }
}
