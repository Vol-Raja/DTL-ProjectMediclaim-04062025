
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTL.Model.Models;
namespace DTL.Business.FinanceManagement
{
  public  interface IAddInvestmentDeclaration
    {
        string AddEditInvestmentDeclaration(InvestmentDeclarationModel investmentDeclarationModel, bool isEdit);

        Task<IEnumerable<InvestmentDeclarationModel>> GetAllInvestment(String FinancialYear, String ReferenceNo, String InvestmentType);

        InvestmentDeclarationModel GetInvestmentDetailsByInvestmentId(Guid InvestmentId);

        int DeleteInvestment(Guid InvestmentId);

        int CloseInvestment(Guid InvestmentId,decimal ActualReturnOnInvestment);
    }
}
