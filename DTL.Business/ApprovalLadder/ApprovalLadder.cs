using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.ApprovalLadder;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.ApprovalLadder
{
    public class ApprovalLadder : IApprovalLadder
    {
        private readonly IDapperService _dapper;
        private string _createdBy = "";
        public ApprovalLadder(IDapperService dapper)
        {
            _dapper = dapper;
        }
        public IEnumerable<ApprovalLadderModel> GetApprovalLadderByParam(int? ladderId, int referenceId, bool isDeleted = false)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@LadderId", ladderId, DbType.Int32);
            dbparams.Add("@ReferenceId", referenceId, DbType.Int32);
            dbparams.Add("@IsDeleted", isDeleted, DbType.Boolean);
            return _dapper.GetAll<ApprovalLadderModel>("GetApprovalLadderByParam", dbparams);
        }
        public int SaveApprovalLadder(ApprovalLadderModel approvalLadderModel)
        {
            int outputLadderId = 0;
            _createdBy = approvalLadderModel.CreatedBy;

            var dbparams = new DynamicParameters();
            dbparams.Add("@Module", approvalLadderModel.Module, DbType.String);
            dbparams.Add("@LadderName", approvalLadderModel.LadderName, DbType.String);
            dbparams.Add("@Comment", approvalLadderModel.Comment, DbType.String);
            dbparams.Add("@LadderStatus", approvalLadderModel.LadderStatus, DbType.String);
            dbparams.Add("@ReferenceId", approvalLadderModel.ReferenceId, DbType.Int32);
            dbparams.Add("@CreatedBy", approvalLadderModel.CreatedBy, DbType.String);
            dbparams.Add("@LadderId", outputLadderId, DbType.Int32, direction: ParameterDirection.Output);
            _dapper.Execute("SaveApprovalLadder", dbparams);
            outputLadderId = dbparams.Get<int>("@LadderId");
            return outputLadderId;
        }
        public int UpdateApprovalLadder(ApprovalLadderModel approvalLadderModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@LadderId", approvalLadderModel.LadderId, DbType.Int32);
            dbparams.Add("@Module", approvalLadderModel.Module, DbType.String);
            dbparams.Add("@LadderName", approvalLadderModel.LadderName, DbType.String);
            dbparams.Add("@Comment", approvalLadderModel.Comment, DbType.String);
            dbparams.Add("@LadderStatus", approvalLadderModel.LadderStatus, DbType.String);
            dbparams.Add("@ReferenceId", approvalLadderModel.ReferenceId, DbType.Int32);
            dbparams.Add("@ModifiedBy", approvalLadderModel.ModifiedBy, DbType.String);

            return _dapper.Execute("UpdateApprovalLadder", dbparams);             
        }
        public int DeleteApprovalLadder(int ladderId, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@LadderId", ladderId, DbType.Int32);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            return _dapper.Execute("DeleteApprovalLadder", dbparams);
        }

        public int UpdateForwardTo(ApprovalLadderModel approvalLadderModel)
        {
            string _modifiedBy = approvalLadderModel.CreatedBy;
            string _update = "";

            if (approvalLadderModel.Module.ToLower() == "mediclaimcashless")
            {
                _update = $"UPDATE dbo.MediclaimCashless SET ForwardTo = '{approvalLadderModel.LadderName}', ModifiedBy = '{_modifiedBy}'  WHERE ClaimId = { approvalLadderModel.ReferenceId}";
            }
            else {
                _update = $"UPDATE dbo.MediclaimNonCashless SET ForwardTo = '{approvalLadderModel.LadderName}', ModifiedBy = '{_modifiedBy}' WHERE ClaimId = { approvalLadderModel.ReferenceId}";

            }

            var dbparams = new DynamicParameters();
            return _dapper.Execute(_update, dbparams,CommandType.Text);
        }
    }
}
