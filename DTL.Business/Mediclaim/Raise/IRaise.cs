using DTL.Model;
using DTL.Model.Models;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.MediclaimRaise
{
    public interface IRaise
    {
        int SaveMediclaimNonCashless(NonCashlessModel nonCashless); 
        void SaveOPDCNDDetail(IList<OPDCNDModel> opdcndList, int nonCashlessclaimId);
        void SaveNonCashlessDependent(DependentInformation dependentInformation); 
        int SaveMediclaimCashless(CashlessModel cashless);
        int SaveAddNewAdmission(CashlessModel cashless);
        int UpdateCashlessDelete(CashlessModel cashlessModel);
        //new changes by nirbhay
        //int Searchppo(CashlessModel cashlessModel);
        CashlessModel Searchppo(int PPONumber); 
        CashlessModel Loginuser(string PPONumber1);
        CashlessModel Getcreditletterdata(string PPONumber2);
        //end new changes
        int UpdateNonCashlessDelete(NonCashlessModel nonCashlessModel);
        IEnumerable<MediclaimDocumentModel> GetMediclaimDocumentsByParam(int? documentId, int? referenceId);
        void SaveDocuments(IEnumerable<MediclaimDocumentModel> medicliamDocuments, int claimId);
        IEnumerable<MasterDocumentModel> GetMasterDocumentList();
        // Add by rajan 15/01/2025
        IEnumerable<MasterDocumentModel> GetDocumentListForCreditLetter();
        void SaveCreditLetterDocuments(IEnumerable<MediclaimDocumentModel> Creditletter_Documents, int ID);
        //end

        //add by rajan 14/04/25
        NonCashlessModel GetAccountData(string PPONumber2);
        //End
    }
}
