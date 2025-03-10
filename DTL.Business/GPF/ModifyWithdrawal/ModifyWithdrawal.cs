using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.GPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.GPF.ModifyWithdrawal
{
    public class ModifyWithdrawal : IModifyWithdrawal
    {
        private readonly IDapperService _dapper;
        public ModifyWithdrawal(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public IEnumerable<GPFWithdrawalModel> GetGPFWithdrawalByParam(int? withdrawId, string withdrawType, string accountHolderName, string employeId, string employer, DateTime? dateOfJoining, DateTime? dateOfApplication, string createdBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@WithdrawId", withdrawId, DbType.Int32);
            dbparams.Add("@WithdrawType", withdrawType, DbType.String);
            dbparams.Add("@AccountHolderName", accountHolderName, DbType.String);
            dbparams.Add("@EmployeId", employeId, DbType.String);
            dbparams.Add("@Employer", employer, DbType.String);
            dbparams.Add("@DateOfJoining", dateOfJoining, DbType.Date);
            dbparams.Add("@DateOfApplication", dateOfApplication, DbType.Date);
            dbparams.Add("@CreatedBy", createdBy, DbType.String);
            dbparams.Add("@Month", null, DbType.Int32);
            dbparams.Add("@Year", null, DbType.Int32);
            return _dapper.GetAll<GPFWithdrawalModel>("GetGPFWithdrawalByParam", dbparams);
        }
        public int UpdateGPFWithdrawal(GPFWithdrawalModel withdrawModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@WithdrawType", withdrawModel.WithdrawType, DbType.String);
            dbparams.Add("@AccountHolderName", withdrawModel.AccountHolderName, DbType.String);
            dbparams.Add("@EmployeId", withdrawModel.EmployeId, DbType.String);
            dbparams.Add("@Designation", withdrawModel.Designation, DbType.String);
            dbparams.Add("@Employer", withdrawModel.Employer, DbType.String);
            dbparams.Add("@MonthlyGPFPay", withdrawModel.MonthlyGPFPay, DbType.Decimal);
            dbparams.Add("@DateOfJoining", withdrawModel.DateOfJoining, DbType.Date);
            dbparams.Add("@PurposeOfWithdrawal", withdrawModel.PurposeOfWithdrawal, DbType.String);
            dbparams.Add("@MobileNumber", withdrawModel.MobileNumber, DbType.String);
            dbparams.Add("@DependantsName", withdrawModel.DependantsName, DbType.String);
            dbparams.Add("@DependentsAge", withdrawModel.DependentsAge, DbType.Decimal);
            dbparams.Add("@DependentsAddress", withdrawModel.DependentsAddress, DbType.String);
            dbparams.Add("@TotalGPFContribution", withdrawModel.TotalGPFContribution, DbType.Decimal);
            dbparams.Add("@TotalWithdrawalAmount", withdrawModel.TotalWithdrawalAmount, DbType.Decimal);
            dbparams.Add("@RemainingContribution", withdrawModel.RemainingContribution, DbType.Decimal);
            dbparams.Add("@TotalAdvancedAmount", withdrawModel.TotalAdvancedAmount, DbType.Decimal);
            dbparams.Add("@NoOfEMI", withdrawModel.NoOfEMI, DbType.Int32);
            dbparams.Add("@MonthlyDeduction", withdrawModel.MonthlyDeduction, DbType.Decimal);
            dbparams.Add("@PurposeOfRefundable", withdrawModel.PurposeOfRefundable, DbType.String);
            dbparams.Add("@PostingLastThreeYear", withdrawModel.PostingLastThreeYear, DbType.String);
            dbparams.Add("@DateOfApplication", withdrawModel.DateOfApplication, DbType.DateTime);
            dbparams.Add("@ReasonOfAdvance", withdrawModel.ReasonOfAdvance, DbType.String);
            dbparams.Add("@ApplicationStatus", withdrawModel.ApplicationStatus, DbType.Int32);

            dbparams.Add("@BankAccountNo", withdrawModel.BankAccountNo, DbType.String);
            dbparams.Add("@IFSCNo", withdrawModel.IFSCNo, DbType.String);
            dbparams.Add("@BranchName", withdrawModel.BranchName, DbType.String);
            dbparams.Add("@BC", withdrawModel.BC, DbType.String);
            dbparams.Add("@BankName", withdrawModel.BankName, DbType.String);

            //dbparams.Add("@SalarySlipFilePath", withdrawModel.SalarySlipFilePath, DbType.String);
            //dbparams.Add("@IdCardFilePath", withdrawModel.IdCardFilePath, DbType.String);

            dbparams.Add("@ModifiedBy", withdrawModel.ModifideBy, DbType.String);
            dbparams.Add("@WithdrawId", withdrawModel.WithdrawId, DbType.Int32);

            return _dapper.Execute("UpdateGPFWithdrawal", dbparams);
        }
        public IEnumerable<GPFDocumentModel> GetGPFDocumentsByParam(int? documentId, int? referenceId, bool active = true)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", documentId, DbType.Int32);
            dbparams.Add("@ReferenceId", referenceId, DbType.Int32);
            dbparams.Add("@Active", active, DbType.Boolean);
            return _dapper.GetAll<GPFDocumentModel>("GetDocumentsByParam", dbparams);
        }
        public void SaveDocumet(List<GPFDocumentModel> gpfDocumentModels)
        {
            var dbparams = new DynamicParameters();
            int _documentId = 0;
            foreach (var document in gpfDocumentModels)
            {
                dbparams.Add("@DocumentPath", document.DocumentPath, DbType.String);
                dbparams.Add("@ApplicationArea", document.ApplicationArea, DbType.String);
                dbparams.Add("@ApplicationSubArea", document.ApplicationSubArea, DbType.String);
                dbparams.Add("@ReferenceId", document.ReferenceId, DbType.Int32);
                dbparams.Add("@DocumentFor", document.DocumentFor, DbType.String);
                dbparams.Add("@CreatedBy", document.CreatedBy, DbType.String);
                dbparams.Add("@DocumentId", _documentId, DbType.Int32, direction: ParameterDirection.Output);
                _dapper.Execute("SaveDocument", dbparams);
                _documentId = dbparams.Get<int>("@DocumentId");
            }
        }
        public int UpdateDocumet(int documentId, bool active, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", documentId, DbType.Int32);
            dbparams.Add("@Active", active, DbType.Int32);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            return _dapper.Execute("UpdateDocument", dbparams);
        }

    }
}
