using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;

namespace DTL.Business.Disbursement
{
   public class AddTDSCalculation : IAddTDSCalculation
    {
        private readonly IDapperService _dapper;

        public AddTDSCalculation(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }
        public Guid AddEditTDSCalculation(TDSCalculationModel tDSCalculationModel,bool isEdit,TDSInvestmentModel tDSInvestmentModel)
        {
            var dbparam = new DynamicParameters();
            dbparam.Add("@ID", (isEdit ? tDSCalculationModel.ID : null), DbType.Guid);
            dbparam.Add("@EmployeeRegistrationId", tDSCalculationModel.EmployeeRegistrationId, DbType.Guid);
            dbparam.Add("@FinancialYear", tDSCalculationModel.FinancialYear, DbType.Int32);
            dbparam.Add("@Pensioner", tDSCalculationModel.Pensioner, DbType.String);
            dbparam.Add("@Pension", tDSCalculationModel.Pension, DbType.Decimal);
            dbparam.Add("@Gratuity", tDSCalculationModel.Gratuity, DbType.Decimal);
            dbparam.Add("@Commutation", tDSCalculationModel.Commutation, DbType.Decimal);
            dbparam.Add("@LeaveEncashment", tDSCalculationModel.LeaveEncashment, DbType.Decimal);
            dbparam.Add("@AQP", tDSCalculationModel.AQP, DbType.Decimal);
            dbparam.Add("@DA", tDSCalculationModel.DA, DbType.Decimal);
            dbparam.Add("@HRA", tDSCalculationModel.HRA, DbType.Decimal);
            dbparam.Add("@Travel_Allowance", tDSCalculationModel.Travel_Allowance, DbType.Decimal);
            dbparam.Add("@Medical_Allowance", tDSCalculationModel.Medical_Allowance, DbType.Decimal);
            dbparam.Add("@OtherIncome", tDSCalculationModel.OtherIncome, DbType.Decimal);
            dbparam.Add("@ExemptedAmount", tDSCalculationModel.ExemptedAmount, DbType.Decimal);
            dbparam.Add("@Ex80DD", tDSCalculationModel.Ex80DD, DbType.Decimal);
            dbparam.Add("@Ex80C", tDSCalculationModel.Ex80C, DbType.Decimal);
            dbparam.Add("@Mediclaim", tDSCalculationModel.Mediclaim, DbType.Decimal);
            dbparam.Add("@HomeLoan", tDSCalculationModel.HomeLoan, DbType.Decimal);
            dbparam.Add("@IntOnEduLoan", tDSCalculationModel.IntOnEduLoan, DbType.Decimal);
            dbparam.Add("@Donation", tDSCalculationModel.Donation, DbType.Decimal);
            dbparam.Add("@TaxableAmount", tDSCalculationModel.TaxableAmount, DbType.Decimal);
            dbparam.Add("@StandardDeduction", tDSCalculationModel.StandardDeduction, DbType.Decimal);
            dbparam.Add("@NetTaxableAmount", tDSCalculationModel.NetTaxableAmount, DbType.Decimal);
            dbparam.Add("@TaxPayable", tDSCalculationModel.TaxPayable, DbType.Decimal);
            dbparam.Add("@Cess", tDSCalculationModel.Cess, DbType.Decimal);
            dbparam.Add("@MonthlyTDSAmount", tDSCalculationModel.MonthlyTDSAmount, DbType.Decimal);
            dbparam.Add("@Regime", tDSCalculationModel.Regime, DbType.String);
            if (!isEdit)
                dbparam.Add("@CreatedBy", tDSCalculationModel.CreatedBy, DbType.String);
            else
                dbparam.Add("@ModifideBy", tDSCalculationModel.ModifideBy, DbType.String);

            dbparam.Add("@ReturnId", "", DbType.Guid, direction: ParameterDirection.Output);
            _dapper.Execute("sp_AddEditTDSCalculation", dbparam);
            var result = dbparam.Get<Guid>("@ReturnId");
            return result;
        }
        public TDSCalculationModel GetTDSCalculationDetails(Guid EmpRegId, int FinYear)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@EmpRegId", EmpRegId, DbType.Guid);
            dbparams.Add("@FinYear", FinYear, DbType.Int32);
            var data = _dapper.Get<TDSCalculationModel>("sp_GetTDSCalculationDetails", dbparams);
            return data;
        }
    }
}
