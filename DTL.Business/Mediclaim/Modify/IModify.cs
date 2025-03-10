using DTL.Model.Models;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.Modify
{
    public interface IModify
    {
        CashlessModel GetCashlessByParam(int claimId);
        int UpdateMediclaimCashless(CashlessModel cashlessModel);
        IEnumerable<MediclaimDocumentModel> GetMediclaimDocumentsByParam(int? documentId, int? referenceId,string applicationSubArea, bool active = true);
        NonCashlessModel GetNonCashlessByParam(int claimId);
        int UpdateNonCashless(NonCashlessModel nonCashlessModel);
        void UpdateOPDCNDDetail(IList<OPDCNDModel> opdcndList, string modifiedBy);
        void SaveDocuments(IEnumerable<MediclaimDocumentModel> medicliamDocuments, int claimId);
        void DeleteDocuments(int documentId);
        IEnumerable<MasterDocumentModel> GetMasterDocumentList();
        void UpdateNonCashlessDependent(DependentInformation dependentInformation);
        DependentInformation GetDependentInformationByParam(int nonCashlessClaimId);

        
    }   
}
