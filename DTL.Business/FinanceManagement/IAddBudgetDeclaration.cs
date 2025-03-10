using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTL.Model.Models;

namespace DTL.Business.FinanceManagement
{
   public interface IAddBudgetDeclaration
    {
        string AddEditBudgetDeclaration(BudgetDeclarationModel budgetDeclaration, bool isEdit);

        IEnumerable<BudgetDeclarationModel> GetBudgetDeclarationByFinancialYear(String FinancialYear);

        int DeleteBudgetByFinancialYear(String FinancialYear);

        int GetBudgetId();

        IEnumerable<BudgetDeclarationModel> GetBudgetReport(string FinancialYear);
    }
}
