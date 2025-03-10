using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTL.Model.Models;

namespace DTL.Business.Disbursement
{
    public interface IAddTDSInvestment
    {

        string AddEditTDSInvestment(TDSInvestmentModel tDSInvestmentModel, bool isEdit);

        TDSInvestmentModel GetTDSInvestmentDetails(Guid TDSCalculationId);
    }
}
