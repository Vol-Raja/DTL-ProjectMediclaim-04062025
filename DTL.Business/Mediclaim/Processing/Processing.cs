using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DTL.Business.Mediclaim.Processing
{
    public class Processing : IProcessing
    {
        private readonly IDapperService _dapper;
        public Processing(IDapperService dapper)
        {
            _dapper = dapper;
        }

        #region NonCashless
        public int ApproveRejectNonCashlessClaim(ApproveRejectModel approveRejectModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", approveRejectModel.ClaimId, DbType.Int32);
            dbparams.Add("@ClaimStatusId", approveRejectModel.ClaimStatusId, DbType.Int32);
            dbparams.Add("@RejectReason", approveRejectModel.RejectReason, DbType.String);
            dbparams.Add("@ApprovedAmount", approveRejectModel.ApprovedAmount, DbType.Decimal);
            dbparams.Add("@DealingAssistanceRemark", approveRejectModel.DealingAssistanceRemark, DbType.String);
            dbparams.Add("@SORemark", approveRejectModel.SORemark, DbType.String);
            dbparams.Add("@ASORemark", approveRejectModel.ASORemark, DbType.String);
            dbparams.Add("@Remark", approveRejectModel.Remark, DbType.String);
            dbparams.Add("@ModifiedBy", approveRejectModel.ModifiedBy, DbType.String);
            return _dapper.Execute("ApproveRejectNonCashlessClaim", dbparams);
        }

        public IEnumerable<NonCashlessModel> GetNonCashlessclaimByParam(string claimDate, int? claimId, int? claimStatusId, int? pageNumber,string cardCategory = null,string organization = null, string forwardTo = null)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Date", claimDate, DbType.Date);
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            dbparams.Add("@ClaimStatusId", claimStatusId, DbType.Int32);
            dbparams.Add("@PageNumber", pageNumber, DbType.String);
            dbparams.Add("@CardCategory", cardCategory, DbType.String);
            dbparams.Add("@Organization", organization, DbType.String);
            dbparams.Add("@ForwardTo", forwardTo, DbType.String);   
            return _dapper.GetAll<NonCashlessModel>("GetNonCashlessClaimByParam", dbparams);
        }

        public IEnumerable<NonCashlessModel> GetPendingNonCashlessMediclaims(string forwardTo = null)
        {
            var dbparams = new DynamicParameters();
            //return _dapper.GetAll<NonCashlessModel>("GetPendingNonCashlessClaims", dbparams);
            //@ForwardTo NVARCHAR(20)= NULL
            dbparams.Add("@ForwardTo", forwardTo, DbType.String);
            return _dapper.GetAll<NonCashlessModel>("GetNonCashlessClaimByParam", dbparams);
        }

        public NonCashlessModel GetNonCashlessByClaimId(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            var nonCashlessDetail = _dapper.Get<NonCashlessModel>("GetNonCashlessClaimByParam", dbparams);
            nonCashlessDetail.OPDCNDList = GetOPDCNDList(claimId).AsList();
            nonCashlessDetail.Documents = GetMediclaimDocumentsByParam(null, nonCashlessDetail.ClaimId, "noncashless");
            nonCashlessDetail.Dependent = GetDependentInformationByParam(claimId);

            return nonCashlessDetail;
        }

        public int UpdateNonCashlessPhysicalSubmit(PhysicalSubmitModel physicalSubmitModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", physicalSubmitModel.ClaimId, DbType.Int32);
            dbparams.Add("@PhysicalSubmit", physicalSubmitModel.PhysicalSubmit, DbType.Boolean);
            dbparams.Add("@ModifiedBy", physicalSubmitModel.ModifiedBy, DbType.String);
            return _dapper.Execute("UpdateNonCashlessPhysicalSubmit", dbparams);
        }
        
        private IEnumerable<OPDCNDModel> GetOPDCNDList(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            return _dapper.GetAll<OPDCNDModel>("GetOPDCNDByClaimId", dbparams);
        }

        public IEnumerable<NonCashlessModel> GetNonCashlessDisbursedClaim()
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Disbursed", true, DbType.Boolean);
            dbparams.Add("@Paid", false, DbType.Boolean);
            var nonCashlessDetail = _dapper.GetAll<NonCashlessModel>("GetDisbursedClaimByParam", dbparams);
            return nonCashlessDetail;
        }
        public int DisburseNonCashlessClaim(int claimId, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Disbursed", true, DbType.Boolean);
            dbparams.Add("@ClaimId", claimId, DbType.Int64);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            return _dapper.Execute("UpdateNonCashlessDisbursedFlag", dbparams);
        }

        public DependentInformation GetDependentInformationByParam(int nonCashlessClaimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@NonCashlessClaimId", nonCashlessClaimId, DbType.Int32);
            return _dapper.Get<DependentInformation>("GetDependentInformationByParam", dbparams);
        }

        #endregion


        #region Cashless
        //For Cashless
        public int ApproveRejectCashlessClaim(ApproveRejectModel approveRejectModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", approveRejectModel.ClaimId, DbType.Int32);
            dbparams.Add("@ClaimStatusId", approveRejectModel.ClaimStatusId, DbType.Int32);
            dbparams.Add("@ApprovedAmount", approveRejectModel.ApprovedAmount, DbType.Decimal);
            dbparams.Add("@RejectReason", approveRejectModel.RejectReason, DbType.String);
            dbparams.Add("@ModifiedBy", approveRejectModel.ModifiedBy, DbType.String);
            dbparams.Add("@ApproveRemark", approveRejectModel.Remark, DbType.String);
            dbparams.Add("@CMORemark", approveRejectModel.CMORemark, DbType.String);
            dbparams.Add("@DealingAssistanceRemark", approveRejectModel.DealingAssistanceRemark, DbType.String);
            dbparams.Add("@ASORemark", approveRejectModel.ASORemark, DbType.String);
            dbparams.Add("@SORemark", approveRejectModel.SORemark, DbType.String);
            dbparams.Add("@Remark", approveRejectModel.Remark, DbType.String);
            dbparams.Add("@ModifiedBy", approveRejectModel.ModifiedBy, DbType.String);
            
            return _dapper.Execute("ApproveRejectCashlessClaim", dbparams);
        }

        public IEnumerable<CashlessModel> GetPendingCashlessMediclaims()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<CashlessModel>("GetPendingCashlessClaims", dbparams);
        }
        //changed by rajan
        public int ApproveRejectCashlessCredit(ApproveRejectModel approveRejectcredit)
        {
            int outputId = 0;
            var dbparams = new DynamicParameters();
            dbparams.Add("@EmployeeNo", approveRejectcredit.EmployeeNo, DbType.Int32);
            dbparams.Add("@Status", approveRejectcredit.StatusCreditId, DbType.Int32);
            dbparams.Add("@ApprovedAmount", approveRejectcredit.ApprovedAmount, DbType.Decimal);
            dbparams.Add("@RejectReason", approveRejectcredit.RejectReason, DbType.String);
            dbparams.Add("@Extended_time", approveRejectcredit.Extended_Stay, DbType.Int32);
            dbparams.Add("@ApproveRemark", approveRejectcredit.Remark, DbType.String);
            dbparams.Add("@CMORemark", approveRejectcredit.CMORemark, DbType.String);
            dbparams.Add("@Remark", approveRejectcredit.Remark, DbType.String);
            dbparams.Add("@serialNo", approveRejectcredit.SerialNo, DbType.String);
            //dbparams.Add("@Id", outputId, DbType.Int32, direction: ParameterDirection.Output);

            //_dapper.Execute("ApproveRejectCashlessCreditletter", dbparams);
            ////outputId = dbparams.Get<int>("@Id");
            //outputId = approveRejectcredit.Extended_Stay;


            //if (outputId > 0)
            //{
            //    SaveCreditLetterDocuments(approveRejectcredit.Documents, outputId);
            //}
            //return outputId;
            dbparams.Add("@Id", outputId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("ApproveRejectCashlessCreditletter", dbparams);
            outputId = dbparams.Get<int>("@Id");
            //outputId = approveRejectcredit.Extended_Stay;

            if (outputId > 0 && approveRejectcredit.Extended_Stay > 0)
            {
                SaveCreditLetterDocuments(approveRejectcredit.Documents, outputId);
            }
            return outputId;
        }
        public void SaveCreditLetterDocuments(IEnumerable<MediclaimDocumentModel> Creditletter_Documents, int ID)
        {
            var dbparams = new DynamicParameters();
            int _documentId = 0;
            foreach (var document in Creditletter_Documents)
            {
                dbparams.Add("@DocumentPath", document.DocumentPath, DbType.String);
                dbparams.Add("@ApplicationArea", document.ApplicationArea, DbType.String);
                dbparams.Add("@ApplicationSubArea", document.ApplicationSubArea, DbType.String);
                dbparams.Add("@ReferenceId", ID, DbType.Int32);
                dbparams.Add("@DocumentFor", document.DocumentFor, DbType.String);
                dbparams.Add("@CreatedBy", document.CreatedBy, DbType.String);
                dbparams.Add("@FileName", document.FileName, DbType.String);
                dbparams.Add("@DocumentId", _documentId, DbType.Int32, direction: ParameterDirection.Output);
                _dapper.Execute("SaveCreditLetter_Document", dbparams);
                _documentId = dbparams.Get<int>("@DocumentId");
            }
        }
        //public int ApproveRejectCashlessCredit(ApproveRejectModel approveRejectcredit)
        //{
        //    var dbparams = new DynamicParameters();
        //    dbparams.Add("@EmployeeNo", approveRejectcredit.EmployeeNo, DbType.Int32);
        //    dbparams.Add("@Status", approveRejectcredit.StatusCreditId, DbType.Int32);
        //    dbparams.Add("@ApprovedAmount", approveRejectcredit.ApprovedAmount, DbType.Decimal);
        //    dbparams.Add("@RejectReason", approveRejectcredit.RejectReason, DbType.String);

        //    dbparams.Add("@ApproveRemark", approveRejectcredit.Remark, DbType.String);
        //    dbparams.Add("@CMORemark", approveRejectcredit.CMORemark, DbType.String);
        //    dbparams.Add("@Remark", approveRejectcredit.Remark, DbType.String);

        //    return _dapper.Execute("ApproveRejectCashlessCreditletter", dbparams);
        //}

        public CashlessModel GetCashlessCreditByEmp(string SerialNo)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@serialNo", SerialNo, DbType.Int32);
            var CashlessCredit = _dapper.Get<CashlessModel>("GetCreditLetterById", dbparams);
            return CashlessCredit;
            //throw new NotImplementedException();
        }
        //end

        public IEnumerable<CashlessModel> GetCashlessclaimByParam(string claimDate, int? claimId, int? claimStatusId, int? pageNumber, string cardCategory = null, string organization = null, string forwardTo = null)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Date", claimDate, DbType.Date);
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            dbparams.Add("@PageNumber", pageNumber, DbType.Int32);
            dbparams.Add("@ClaimStatusId", claimStatusId, DbType.Int32);
            dbparams.Add("@PageNumber", pageNumber, DbType.String);
            dbparams.Add("@CardCategory", cardCategory, DbType.String);
            dbparams.Add("@Organization", organization, DbType.String);
            dbparams.Add("@ForwardTo", forwardTo, DbType.String);
            return _dapper.GetAll<CashlessModel>("GetCashlessClaimByParam", dbparams);
        }
//New changes by nirbhay
        public IEnumerable<CashlessModel> GetCashlessCreditlatter(string claimDate,int? claimStatusId, string cardCategory = null, string forwardTo = null)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Date", claimDate, DbType.Date);
            //dbparams.Add("@ClaimId", claimId, DbType.Int32);
            //dbparams.Add("@PageNumber", pageNumber, DbType.Int32);
            dbparams.Add("@ClaimStatusId", claimStatusId, DbType.Int32);
            //dbparams.Add("@PageNumber", pageNumber, DbType.String);
            dbparams.Add("@CardCategory", cardCategory, DbType.String);
            //dbparams.Add("@Organization", organization, DbType.String);
            dbparams.Add("@ForwardTo", forwardTo, DbType.String);
            return _dapper.GetAll<CashlessModel>("GetCashlessCeditletter", dbparams);
        }
// End new changes
        public CashlessModel GetCashlessByClaimId(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            var cashlessDetail = _dapper.Get<CashlessModel>("GetCashlessListByClaimId", dbparams);
            cashlessDetail.Documents = GetMediclaimDocumentsByParam(null, claimId, "cashless");
            return cashlessDetail;
        }

        public int UpdateCashlessPhysicalSubmit(PhysicalSubmitModel physicalSubmitModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", physicalSubmitModel.ClaimId, DbType.Int32);
            dbparams.Add("@PhysicalSubmit", physicalSubmitModel.PhysicalSubmit, DbType.Boolean);
            dbparams.Add("@ModifiedBy", physicalSubmitModel.ModifiedBy, DbType.String);
            return _dapper.Execute("UpdateCashlessPhysicalSubmit", dbparams);
        }

        public int DisburseCashlessClaim(int claimId, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Disbursed", true, DbType.Boolean);
            dbparams.Add("@ClaimId", claimId, DbType.Int64);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            return _dapper.Execute("UpdateCashlessDisbursedFlag", dbparams);
        }

        #endregion

        public IEnumerable<MediclaimDocumentModel> GetMediclaimDocumentsByParam(int? documentId, int? referenceId, string applicationSubArea, bool isActive = true)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", documentId, DbType.Int32);
            dbparams.Add("@ReferenceId", referenceId, DbType.Int32);
            dbparams.Add("@ApplicationSubArea", applicationSubArea, DbType.String);
            return _dapper.GetAll<MediclaimDocumentModel>("GetDocumentsByParam", dbparams);
        }

        public int UpdateForwardTo(string forwardTo, string modifiedBy, int claimId, string claimType)
        {
            string _update = "";

            if (claimType.ToLower() == "cashless")
            {
                _update = $"UPDATE dbo.MediclaimCashless SET ForwardTo = '{forwardTo}', ModifiedBy = '{modifiedBy}'  WHERE ClaimId = { claimId}";
            }
            else
            {
                _update = $"UPDATE dbo.MediclaimNonCashless SET ForwardTo = '{forwardTo}', ModifiedBy = '{modifiedBy}' WHERE ClaimId = { claimId}";

            }

            var dbparams = new DynamicParameters();
            return _dapper.Execute(_update, dbparams, CommandType.Text);
        }
        //changed by nirbhay

        public int UpdateForwardToHospital(string forwardTo, string modifiedBy, int employeeNo, string claimType)
        {
            string _update = "";

            if (claimType.ToLower() == "cashless")
            {
                _update = $"UPDATE dbo.Credit_letter SET ForwardTo = '{forwardTo}', ModifiedBy = '{modifiedBy}'  WHERE EmployeeNo = { employeeNo}";
            }
            else
            {
                _update = $"UPDATE dbo.Credit_letter SET ForwardTo = '{forwardTo}', ModifiedBy = '{modifiedBy}' WHERE EmployeeNo = { employeeNo}";

            }

            var dbparams = new DynamicParameters();
            return _dapper.Execute(_update, dbparams, CommandType.Text);
        }

        public IEnumerable<CashlessModel> GetCashlessByPPO(string PPO)
        {
            if (string.IsNullOrWhiteSpace(PPO))
            {
                throw new ArgumentException("PPO number cannot be null or empty", nameof(PPO));
            }

            var dbparams = new DynamicParameters();
            dbparams.Add("@ppo", PPO, DbType.String);

            string sql = "SELECT * FROM MediclaimCashless WHERE PPONumber = @ppo";

            return _dapper.GetAll<CashlessModel>(sql, dbparams, CommandType.Text);
        }
        // add by nirbhay ExportToExcel 05/30/2025
        public IEnumerable<NonCashlessModel> GetClaimsByASODateRange(DateTime startDate, DateTime endDate)
        {
            string query = "SELECT * FROM mediclaimNonCashless WHERE Isdelete =0 and forwardto='ASO' and createddate BETWEEN @StartDate AND @EndDate";
            var dbparams = new DynamicParameters();
            dbparams.Add("@startDate", startDate, DbType.DateTime);
            dbparams.Add("@endDate", endDate, DbType.DateTime);

            return _dapper.GetAll<NonCashlessModel>(query, dbparams, CommandType.Text);
        }
        //end changes
        // add by nirbhay ExportToExcel 05/30/2025
        public IEnumerable<NonCashlessModel> GetClaimsBymediclaimOPDDADateRange(DateTime startDate, DateTime endDate)
        {
            string query = "SELECT * FROM mediclaimNonCashless WHERE Isdelete =0 and  createddate BETWEEN @StartDate AND @EndDate";
            var dbparams = new DynamicParameters();
            dbparams.Add("@startDate", startDate, DbType.DateTime);
            dbparams.Add("@endDate", endDate, DbType.DateTime);

            return _dapper.GetAll<NonCashlessModel>(query, dbparams, CommandType.Text);
        }
        //end changes
        // add by nirbhay ExportToExcel 05/30/2025
        public IEnumerable<NonCashlessModel> GetClaimsBymediclaimAMDMDateRange(DateTime startDate, DateTime endDate)
        {
            string query = "SELECT * FROM mediclaimNonCashless WHERE Isdelete = 0 AND forwardto = 'AM_DM' AND Disbursed IS NULL AND Paid IS NULL and createddate BETWEEN @StartDate AND @EndDate";
            var dbparams = new DynamicParameters();
            dbparams.Add("@startDate", startDate, DbType.DateTime);
            dbparams.Add("@endDate", endDate, DbType.DateTime);

            return _dapper.GetAll<NonCashlessModel>(query, dbparams, CommandType.Text);
        }
        //end changes  
        // add by nirbhay ExportToExcel 05/30/2025
        public IEnumerable<NonCashlessModel> GetClaimsBymediclaimMediDisbusDateRange(DateTime startDate, DateTime endDate)
        {
            string query = "SELECT * FROM mediclaimNonCashless WHERE Isdelete = 0 AND Disbursed=1 AND Paid=1 and createddate BETWEEN @StartDate AND @EndDate";
            var dbparams = new DynamicParameters();
            dbparams.Add("@startDate", startDate, DbType.DateTime);
            dbparams.Add("@endDate", endDate, DbType.DateTime);

            return _dapper.GetAll<NonCashlessModel>(query, dbparams, CommandType.Text);
        }
        //end changes


        public int UpdateDeductedAmounts(List<OPDCNDModel> deductions)
        {
            int rowsAffected = 0;

            foreach (var item in deductions)
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@OPDCNDId", item.OPDCNDId, DbType.Int32);
                dbparams.Add("@DeductedAmount", item.DeductedAmount, DbType.Decimal);

                rowsAffected += _dapper.Execute("Update_OPDCND_DeductedAmount", dbparams, commandType: CommandType.StoredProcedure);
            }

            return rowsAffected;
        }

    }
}
