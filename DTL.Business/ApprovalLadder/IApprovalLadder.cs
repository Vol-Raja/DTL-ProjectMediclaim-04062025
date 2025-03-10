using DTL.Model.Models.ApprovalLadder;
using System.Collections.Generic;

namespace DTL.Business.ApprovalLadder
{
    public interface IApprovalLadder
    {
        IEnumerable<ApprovalLadderModel> GetApprovalLadderByParam(int? ladderId, int referenceId, bool isDeleted = false);
        int SaveApprovalLadder(ApprovalLadderModel approvalLadderModel);
        int UpdateApprovalLadder(ApprovalLadderModel approvalLadderModel);
        int DeleteApprovalLadder(int ladderId, string modifiedBy);
        int UpdateForwardTo(ApprovalLadderModel approvalLadderModel);
    }
}
