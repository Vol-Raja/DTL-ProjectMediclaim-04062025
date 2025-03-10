using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;


namespace DTL.Business.FinanceManagement
{
   public class AddBudgetDeclaration : IAddBudgetDeclaration
    {
        private readonly IDapperService _dapper;

        public AddBudgetDeclaration(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditBudgetDeclaration(BudgetDeclarationModel budgetDeclarationModel, bool isEdit)
        {
            var dbparam = new DynamicParameters();
            dbparam.Add("@ID", (isEdit ? budgetDeclarationModel.ID : null), DbType.Guid);
            
            dbparam.Add("@FinancialYear", budgetDeclarationModel.FinancialYear, DbType.String);
            dbparam.Add("@AllocationProgram", budgetDeclarationModel.AllocationProgram, DbType.String);
            dbparam.Add("@AllocatedFunds", budgetDeclarationModel.AllocatedFunds, DbType.Decimal);
            dbparam.Add("@DisbursementPeriod", budgetDeclarationModel.DisbursementPeriod, DbType.String);
            dbparam.Add("@DisbursementAuthority", budgetDeclarationModel.DisbursementAuthority, DbType.String);
            if (!isEdit)
                dbparam.Add("@CreatedBy", budgetDeclarationModel.CreatedBy, DbType.String);
           // else
                dbparam.Add("@ModifideBy", budgetDeclarationModel.ModifideBy, DbType.String);

            dbparam.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("sp_AddEditBudgetDeclaration", dbparam);
            var result = dbparam.Get<String>("@ReturnMsg");
            return result;
        }
      public IEnumerable<BudgetDeclarationModel> GetBudgetDeclarationByFinancialYear(String FinancialYear)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@FinancialYear", FinancialYear, DbType.String);
            var data = _dapper.GetAll<BudgetDeclarationModel>("sp_GetBudgetByFinancialYear", dbparams);
            return data.AsEnumerable();

        }
        public int  DeleteBudgetByFinancialYear(String FinancialYear)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@FinancialYear", FinancialYear, DbType.String);
            var data = _dapper.Get<int>("sp_DeleteBudgetByFinancialYear", dbparams);
            return data;
        }


        public int GetBudgetId()
        {
            var dbparam = new DynamicParameters();
            dbparam.Add("@ReturnId", 0, DbType.Int32, direction: ParameterDirection.Output);
            _dapper.Execute("sp_GetBudgetId", dbparam);
            var result = dbparam.Get<Int32>("@ReturnId");
            return result;
        }

        public IEnumerable<BudgetDeclarationModel> GetBudgetReport(string FinancialYear)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@FinancialYear", FinancialYear, DbType.String);
            var data = _dapper.GetAll<BudgetDeclarationModel>("usp_BudgetReport_GetAll", dbparams);
            return data.AsEnumerable();
        }
    }
}
