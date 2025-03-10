using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.Detail.NonCashless
{
    public interface INonCashlessDetail
    {
        IEnumerable<NonCashlessModel> GetNonCashlessList();
        IEnumerable<NonCashlessModel> GetNonCashlessListByStatus(int claimStatus);
        IEnumerable<NonCashlessModel> GetNonCashlessListByCreatedBy(string createdBy);
        NonCashlessModel GetNonCashlessByClaimId(int ClaimId);
        IEnumerable<DispensaryModel> GetDispensaries(int claimId);
        IEnumerable<IPDModel> GetIPDList(int claimId);
        IEnumerable<OPDCNDModel> GetOPDCNDList(int claimId);
        IEnumerable<MediclaimDocumentModel> GetMediclaimDocumentsByParam(int? documentId, int? referenceId, bool active = true);
        DependentInformation GetDependentInformationByParam(int nonCashlessClaimId);
        int UpdateCashlessDelete(int claimId, bool deleteFlag, string modifiedBy);
    }
}
