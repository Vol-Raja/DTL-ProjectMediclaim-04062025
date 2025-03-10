using Dapper;
using DocumentFormat.OpenXml.EMMA;
using DTL.Business.Dapper;
using DTL.Business.GPF.EmployeeInfo;
using DTL.Model.Models.GPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DTL.Business.GPF.LoanProcessing
{
    public class LoanProcessing : ILoanProcessing
    {
        private readonly IDapperService _dapper;
        private readonly IEmployeeInfo _empInfo;

        public LoanProcessing(IDapperService dapper, IEmployeeInfo empInfo)
        {
            _dapper = dapper;
            _empInfo = empInfo;
        }
        public int ApproveRejectGPFApplication(ApproveRejectModel approveRejectModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@WithdrawId", approveRejectModel.WithdrawId, DbType.Int32);
            dbparams.Add("@ApplicationStatusId", approveRejectModel.ApplicationStatusId, DbType.Int32);
            dbparams.Add("@RejectReason", approveRejectModel.RejectReason, DbType.String);
            dbparams.Add("@ApprovedAmount", approveRejectModel.ApprovedAmount, DbType.Decimal);
            dbparams.Add("@ModifiedBy", approveRejectModel.ModifiedBy, DbType.String);
            return _dapper.Execute("ApproveRejectGPFWithdrawal", dbparams);
        }
        public IEnumerable<GPFWithdrawalModel> GetGPFWithdrawalByParam(int? withdrawId, string employer, int? month, int? year, string employeeId, int? applicationStatusId, string applicationNumber, string createdBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@WithdrawId", withdrawId, DbType.Int32);
            dbparams.Add("@WithdrawType", null, DbType.String);
            dbparams.Add("@AccountHolderName", null, DbType.String);
            dbparams.Add("@EmployeId", employeeId, DbType.String);
            dbparams.Add("@Employer", employer, DbType.String);
            dbparams.Add("@DateOfJoining", null, DbType.Date);
            dbparams.Add("@DateOfApplication", null, DbType.Date);
            dbparams.Add("@Month", month, DbType.Int32);
            dbparams.Add("@Year", year, DbType.Int32);
            dbparams.Add("@ApplicationStatusId", applicationStatusId, DbType.Int32);
            dbparams.Add("@UniqueNumber", applicationNumber, DbType.String);
            dbparams.Add("@CreatedBy", createdBy, DbType.String);

            return _dapper.GetAll<GPFWithdrawalModel>("GetGPFWithdrawalByParam", dbparams);
        }
        public int UpdatePhysicalSubmit(PhysicalSubmitModel physicalSubmitModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@WithdrawId", physicalSubmitModel.WithdrawId, DbType.Int32);
            dbparams.Add("@PhysicalSubmit", physicalSubmitModel.PhysicalSubmit, DbType.Boolean);
            dbparams.Add("@ModifiedBy", physicalSubmitModel.ModifiedBy, DbType.String);
            return _dapper.Execute("UpdateGPFWithdrawalPhysicalSubmit", dbparams);
        }
        public IEnumerable<GPFDocumentModel> GetDocumentsByParam(int withdrawalId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", null, DbType.Int32);
            dbparams.Add("@ReferenceId", withdrawalId, DbType.Int32);
            return _dapper.GetAll<GPFDocumentModel>("GetDocumentsByParam", dbparams);
        }

        public GPF_Loan SaveLoanApplication(GPF_Loan obj)
        {
            obj.ApplicationNumber = GenerateLoanApplicationNumber(obj.Organization);
            obj.CreatedOn = DateTime.Now;
            obj.ApplicationStatus = "Pending";
            obj.LoanStatus = "Pending";

            var dParams = _dapper.GetDynamicParams(obj, "Id", new List<string> { "Details", "BankDetails", "Documents", "Employee" });
            var id = _dapper.Execute("Save_GPF_Loan", dParams);

            if (id > 0)
            {
                obj.Details.ApplicationNumber = obj.ApplicationNumber;
                obj.BankDetails.ApplicationNumber = obj.ApplicationNumber;
                dParams = _dapper.GetDynamicParams(obj.Details, "Id", new List<string> { });
                var i = _dapper.Execute("Save_GPF_Loan_Details", dParams);
                dParams = _dapper.GetDynamicParams(obj.BankDetails, "Id", new List<string> { });
                i = _dapper.Execute("Save_GPF_Loan_BankDetails", dParams);
            }

            return obj;
        }
        public int SaveLoanApplicationDoc(GPF_Loan_Documents obj)
        {
            var dParams = _dapper.GetDynamicParams(obj, "Id", new List<string> { });
            var id = _dapper.Execute("Save_GPF_Loan_Documents", dParams);
            return id;
        }

        public IEnumerable<GPF_Loan_View> GetLoanApplications(string type, string status)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@type", type.ToLower().Trim(), DbType.String);
            dParams.Add("@status", status.ToLower().Trim(), DbType.String);
            var sql = $@"
                select a.*, b.EMP_NAME 'EmployeeName', EMP_CODE 'EmployeeCode'
                from GPF_Loan a
                left join EMPLOYEE_DEFINATION b on a.EmployeeExternalCode = b.EXTERNAL_CODE
                where LOWER(LoanType) = @type and (@status = 'all' or LOWER(ApplicationStatus) = @status)
                order by a.CreatedOn desc
            ";
            var ds = _dapper.GetAll<GPF_Loan_View>(sql, dParams, CommandType.Text);
            return ds;
        }

        public IEnumerable<GPF_Loan_View> GetLoanApplicationsByEmployee(string externalCode, string type = "all", string status = "all")
        {
            var dParams = new DynamicParameters();
            dParams.Add("@empCode", externalCode.ToLower().Trim(), DbType.String);
            dParams.Add("@type", type.ToLower().Trim(), DbType.String);
            dParams.Add("@status", status.ToLower().Trim(), DbType.String);
            var sql = $@"
                select a.*, b.EMP_NAME 'EmployeeName', EMP_CODE 'EmployeeCode'
                from GPF_Loan a
                left join EMPLOYEE_DEFINATION b on a.EmployeeExternalCode = b.EXTERNAL_CODE
                where LOWER(EmployeeExternalCode) = @empCode 
                    and (@type = 'all' or LOWER(LoanType) = @type)
                    and (@status = 'all' or LOWER(ApplicationStatus) = @status)
                order by a.CreatedOn desc
            ";
            var ds = _dapper.GetAll<GPF_Loan_View>(sql, dParams, CommandType.Text);
            sql = $@"select * from GPF_Loan_Details where LOWER(ApplicationNumber) in ('" + string.Join("','", ds.Select(q => q.ApplicationNumber)) + "')";
            var ds2 = _dapper.GetAll<GPF_Loan_Details>(sql, dParams, CommandType.Text);
            foreach (var d in ds)
                d.Details = ds2.FirstOrDefault(q => q.ApplicationNumber == d.ApplicationNumber);
            return ds;
        }

        public IEnumerable<GPF_Loan_View_Disburs> GetLoanApplicationsDisburs(string type)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@type", type.ToLower().Trim(), DbType.String);
            var sql = $@"
                select a.*
                , b.AccountNumber, b.BankName, b.BranchName, b.BIC, b.IFSC
                , c.EMP_NAME 'EmployeeName', c.EMP_CODE 'EmployeeCode'
                from GPF_Loan a
                left join GPF_Loan_BankDetails b on a.ApplicationNumber = b.ApplicationNumber
                left join EMPLOYEE_DEFINATION c on a.EmployeeExternalCode = c.EXTERNAL_CODE
                where LOWER(LoanType) = @type and LOWER(ApplicationStatus) in ('Pending with Disbursement', 'Completed')
                order by a.CreatedOn desc
            ";
            var ds = _dapper.GetAll<GPF_Loan_View_Disburs>(sql, dParams, CommandType.Text);
            return ds;
        }

        public GPF_Loan GetLoanApplication(string applicationNumber)
        {
            GPF_Loan res;
            var dParams = new DynamicParameters();
            dParams.Add("@appNo", applicationNumber.ToLower().Trim(), DbType.String);

            var sql = $@"select top 1 * from GPF_Loan where LOWER(ApplicationNumber) = @appNo";
            res = _dapper.Get<GPF_Loan>(sql, dParams, CommandType.Text);

            sql = $@"select top 1 * from GPF_Loan_Details where LOWER(ApplicationNumber) = @appNo";
            res.Details = _dapper.Get<GPF_Loan_Details>(sql, dParams, CommandType.Text);

            sql = $@"select top 1 * from GPF_Loan_BankDetails where LOWER(ApplicationNumber) = @appNo";
            res.BankDetails = _dapper.Get<GPF_Loan_BankDetails>(sql, dParams, CommandType.Text);

            sql = $@"select * from GPF_Loan_Documents where LOWER(ApplicationNumber) = @appNo";
            res.Documents = _dapper.GetAll<GPF_Loan_Documents>(sql, dParams, CommandType.Text);

            res.Employee = _empInfo.GetEmployee(res.EmployeeExternalCode);

            return res;
        }

        public int UpdateLoanApplicationStatus(GPF_Loan loan)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@applicationNumber", loan.ApplicationNumber);
            dParams.Add("@applicationStatus", loan.ApplicationStatus);
            dParams.Add("@loanStatus", loan.LoanStatus);
            if (!string.IsNullOrWhiteSpace(loan.Remark)) dParams.Add("@remark", loan.Remark);
            if (!string.IsNullOrWhiteSpace(loan.AG2Remark)) dParams.Add("@ag2Remark", loan.AG2Remark);
            if (!string.IsNullOrWhiteSpace(loan.AMDMRemark)) dParams.Add("@amdmRemark", loan.AMDMRemark);

            var sql = "update GPF_Loan set " +
                ((string.IsNullOrWhiteSpace(loan.Remark)) ? "" : "Remark = @remark, ") +
                ((string.IsNullOrWhiteSpace(loan.AG2Remark)) ? "" : "AG2Remark = @ag2Remark, ") +
                ((string.IsNullOrWhiteSpace(loan.AMDMRemark)) ? "" : "AMDMRemark = @amdmRemark, ") +
                "ApplicationStatus = @applicationStatus, " +
                "LoanStatus = @loanStatus " +
                "where ApplicationNumber = @applicationNumber";

            var i = _dapper.Execute(sql, dParams, CommandType.Text);
            return i;
        }

        private string GenerateLoanApplicationNumber(string orgCode)
        {
            var pre = $@"{orgCode}/{DateTime.Now.ToString("yyyyMM")}/";
            var list = new List<string>();
            for (int i = 1; i <= 99; i++)
                list.Add(pre + i.ToString().PadLeft(2, '0'));
            var sql = $@"select top 1 * from ( VALUES {string.Join(",", list.Select(q => "('" + q + "')"))} ) as t(a) where a not in (select distinct ApplicationNumber from GPF_Loan)";
            var res = _dapper.GetDataSet(sql, new Dictionary<string, object> { }, CommandType.Text);
            return res.Tables[0].Rows[0][0].ToString();
        }

        public int UpdateLoanApplicationPaid(GPF_Loan loan)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@applicationNumber", loan.ApplicationNumber);

            var sql = "update GPF_Loan set " +
                "LoanStatus = 'Completed', " +
                "ApplicationStatus = 'Completed', " +
                "PaymentDone = 1 " +
                "where ApplicationNumber = @applicationNumber";

            var i = _dapper.Execute(sql, dParams, CommandType.Text);
            return i;
        }

    }
}
