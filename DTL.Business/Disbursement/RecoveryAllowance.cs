using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Disbursement
{
    public class RecoveryAllowance : IRecoveryAllowance
    {
        private readonly IDapperService _dapper;

        public RecoveryAllowance(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }

        public string InsertUpdateRecoveryAllowanceDetails(RecoveryAllowanceModel recoveryAllowanceModel, bool isUpdate)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", (isUpdate ? recoveryAllowanceModel.ID : null), DbType.Guid);
            dbparams.Add("@EmployeeRegistrationId", recoveryAllowanceModel.EmployeeRegistrationId, DbType.Guid);
            dbparams.Add("@PensionerName", recoveryAllowanceModel.PensionerName, DbType.String);
            dbparams.Add("@EmployeeID", recoveryAllowanceModel.EmployeeID, DbType.Int64);
            dbparams.Add("@EmployerName", recoveryAllowanceModel.EmployerName, DbType.String);
            dbparams.Add("@ChangeType", recoveryAllowanceModel.ChangeType, DbType.String);
            dbparams.Add("@Reason", recoveryAllowanceModel.Reason, DbType.String);
            dbparams.Add("@TypeOfRecovery", recoveryAllowanceModel.TypeOfRecovery, DbType.String);
            dbparams.Add("@RecoveryAmount", recoveryAllowanceModel.RecoveryAmount, DbType.Decimal);
            dbparams.Add("@RecoveryOption", recoveryAllowanceModel.RecoveryOption, DbType.String);
            dbparams.Add("@MonthlyPension", recoveryAllowanceModel.MonthlyPension, DbType.Decimal);
            dbparams.Add("@ApplicableAmount", recoveryAllowanceModel.ApplicableAmount, DbType.Decimal);
            dbparams.Add("@MonthlyPensionAfter", recoveryAllowanceModel.MonthlyPensionAfter, DbType.Decimal);
            dbparams.Add("@FromDate", recoveryAllowanceModel.FromDate, DbType.Date);
            dbparams.Add("@ToDate", recoveryAllowanceModel.ToDate, DbType.Date);
            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            if (!isUpdate)
                dbparams.Add("@CreatedBy", recoveryAllowanceModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifideBy", recoveryAllowanceModel.ModifideBy, DbType.String);
            _dapper.Execute("usp_InsertUpdateRecoveryAllowanceDetails", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }

        public async Task<IEnumerable<RecoveryAllowanceModel>> GetPensionersAllAsync()
        {
            var dbparams = new DynamicParameters();
            return await _dapper.GetAllAsync<RecoveryAllowanceModel>("usp_Pensioner_RecoveryAllowance_GetAll", dbparams);
        }

    }
}
