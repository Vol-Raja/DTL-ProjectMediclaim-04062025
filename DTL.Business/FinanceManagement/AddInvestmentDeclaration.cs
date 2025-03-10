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
    public class AddInvestmentDeclaration : IAddInvestmentDeclaration
    {
        private readonly IDapperService _dapper;

        public AddInvestmentDeclaration(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }
        public string AddEditInvestmentDeclaration(InvestmentDeclarationModel investmentDeclarationModel, bool isEdit)
        {
            var dbparam = new DynamicParameters();
            dbparam.Add("@ID", (isEdit ? investmentDeclarationModel.ID : null), DbType.Guid);
            dbparam.Add("@FinancialYear", investmentDeclarationModel.FinancialYear, DbType.String);
            dbparam.Add("@InvestmentType", investmentDeclarationModel.InvestmentType, DbType.String);
            dbparam.Add("@InvestmentTitle", investmentDeclarationModel.InvestmentTitle, DbType.String);
            dbparam.Add("@ReferenceNo", investmentDeclarationModel.ReferenceNo, DbType.String);
            dbparam.Add("@InvestedAmount", investmentDeclarationModel.InvestedAmount, DbType.Decimal);
            dbparam.Add("@ClosingPeriod", investmentDeclarationModel.ClosingPeriod, DbType.String);
            dbparam.Add("@FromDate", investmentDeclarationModel.FromDate, DbType.Date);
            dbparam.Add("@ToDate", investmentDeclarationModel.ToDate, DbType.Date);
            dbparam.Add("@ExpectedProfitMargin", investmentDeclarationModel.ExpectedProfitMargin, DbType.Decimal);
            dbparam.Add("@ExpectedROI", investmentDeclarationModel.ExpectedROI, DbType.Decimal);
            dbparam.Add("@ExpectedProfitAmount", investmentDeclarationModel.ExpectedProfitAmount, DbType.Decimal);
            if (!isEdit)
                dbparam.Add("@CreatedBy", investmentDeclarationModel.CreatedBy, DbType.String);
           else
            dbparam.Add("@ModifideBy", investmentDeclarationModel.ModifideBy, DbType.String);
            dbparam.Add("@ReturnMsg", "", DbType.String, ParameterDirection.Output);
            _dapper.Execute("sp_AddEditInvestmentDeclaration", dbparam);
            var result = dbparam.Get<String>("@ReturnMsg");
            return result;

        }

        public async Task<IEnumerable<InvestmentDeclarationModel>> GetAllInvestment(String FinancialYear,String ReferenceNo,String InvestmentType)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@FinancialYear", FinancialYear, DbType.String);
            dbparams.Add("@ReferenceNo", ReferenceNo, DbType.String);
            dbparams.Add("@InvestmentType", InvestmentType, DbType.String);
            return await _dapper.GetAllAsync<InvestmentDeclarationModel>("sp_GetInvestmentDeclaration", dbparams);
        }

        public InvestmentDeclarationModel GetInvestmentDetailsByInvestmentId(Guid InvestmentId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@InvestmentId", InvestmentId, DbType.Guid);
           var data= _dapper.Get<InvestmentDeclarationModel>("sp_GetInvestmentDetailsByInvestmentId", dbparams);
            return data;
        }


        public int DeleteInvestment(Guid InvestmentId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@InvestmentId", InvestmentId, DbType.Guid);
            return _dapper.Execute("sp_DeleteInvestment", dbparams);
        }


        public int CloseInvestment(Guid InvestmentId, decimal ActualReturnOnInvestment)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@InvestmentId", InvestmentId, DbType.Guid);
            dbparams.Add("@ActualReturnOnInvestment", ActualReturnOnInvestment, DbType.Decimal);
            return _dapper.Execute("sp_CloseInvestment", dbparams);
        }


    }
}
