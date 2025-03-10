using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.GPF;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.GPF.GeneratePayment
{
    public class GeneratePayment : IGeneratePayment
    {
        private readonly IDapperService _dapper;
        public GeneratePayment(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public IEnumerable<GPFGeneratePaymentModel> GetGeneratePaymentByParam(int? paymentId, string applicationId, string loanType)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PaymentId", paymentId, DbType.Int32);
            dbparams.Add("@ApplicationId", applicationId, DbType.String);
            dbparams.Add("@LoanType", loanType, DbType.String);

            return _dapper.GetAll<GPFGeneratePaymentModel>("GetGPFGeneratePaymentByParam", dbparams);
        }

        public IEnumerable<GPFWithdrawalModel> GetGPFWithdrawalByParam(string applicationID)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@WithdrawId", null, DbType.Int32);
            dbparams.Add("@WithdrawType", null, DbType.String);
            dbparams.Add("@AccountHolderName", null, DbType.String);
            dbparams.Add("@EmployeId", null, DbType.String);
            dbparams.Add("@Employer", null, DbType.String);
            dbparams.Add("@DateOfJoining", null, DbType.Date);
            dbparams.Add("@DateOfApplication", null, DbType.Date);
            dbparams.Add("@CreatedBy", null, DbType.String);
            dbparams.Add("@Month", null, DbType.Int32);
            dbparams.Add("@Year", null, DbType.Int32);
            dbparams.Add("@UniqueNumber", applicationID, DbType.String);
            return _dapper.GetAll<GPFWithdrawalModel>("GetGPFWithdrawalByParam", dbparams);
        }

        public IEnumerable<GPFGeneratePaymentModel> GetArchiveGeneratePaymentByParam(int? paymentId, string applicationId, string loanType)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PaymentId", paymentId, DbType.Int32);
            dbparams.Add("@ApplicationId", applicationId, DbType.String);
            dbparams.Add("@LoanType", loanType, DbType.String);

            return _dapper.GetAll<GPFGeneratePaymentModel>("GetArchivedGPFGeneratePaymentByParam", dbparams);
        }

        public int SaveGeneratePayment(GPFGeneratePaymentModel generatePaymentModel) 
        {
            int outputPaymentId = 0;
            var dbparams = new DynamicParameters();
            dbparams.Add("@ApplicationId", generatePaymentModel.ApplicationId, DbType.String);
            dbparams.Add("@NameOfEmployee", generatePaymentModel.NameOfEmployee, DbType.String);
            dbparams.Add("@ContactNo", generatePaymentModel.ContactNo, DbType.String);
            dbparams.Add("@LoanType", generatePaymentModel.LoanType, DbType.String);
            dbparams.Add("@NoOfEMI", generatePaymentModel.NoOfEMI, DbType.Int32);
            dbparams.Add("@ApprovedAmount", generatePaymentModel.ApprovedAmount, DbType.Decimal);
            dbparams.Add("@AccountNumber", generatePaymentModel.AccountNumber, DbType.String);
            dbparams.Add("@IFSCCode", generatePaymentModel.IFSCCode, DbType.String);
            dbparams.Add("@BICCode", generatePaymentModel.BICCode, DbType.String);
            dbparams.Add("@CreatedBy", generatePaymentModel.CreatedBy, DbType.String);
            dbparams.Add("@PaymentId", outputPaymentId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("SaveGPFGeneratePayment", dbparams);

            outputPaymentId = dbparams.Get<int>("@PaymentId");

            return outputPaymentId;
        }
        public int DeleteGeneratePayment(int paymentId, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PaymentId", paymentId, DbType.Int32);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            return _dapper.Execute("DeleteGPFGeneratePayment", dbparams);
        }
        
    }
}
