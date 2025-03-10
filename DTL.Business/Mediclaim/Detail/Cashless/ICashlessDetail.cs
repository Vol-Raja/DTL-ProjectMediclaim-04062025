using DTL.Model.Models.Mediclaim;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.Detail.Cashless
{
    public interface ICashlessDetail
    {
        IEnumerable<CashlessModel> GetCashlessList();
        IEnumerable<CashlessModel> GetCashlessListByStatus(int claimStatus);
        IEnumerable<CashlessModel> GetCashlessListByCreatedBy(string createdBy);
        IEnumerable<CashlessModel> GetCreditListByCreatedBy(string createdBy);
        CashlessModel GetCashlessById(int ClaimId);
        CashlessModel GetMediclaimCreditLetter(string SerialNo);
        IEnumerable<MediclaimDocumentModel> GetMediclaimDocumentsByParam(int? documentId, int? referenceId, bool active = true);
        IEnumerable<MediclaimDocumentModel> GetCredit_Report_DocumentsByParam(int? documentId, int? referenceId, string SerialNO, bool active = true);
        //Add by rajan 22/01/2025 for Extend date
        IEnumerable<MediclaimDocumentModel> GetExtend_Report_DocumentsByParam(int? documentId, int? referenceId, string ExtendDate, bool active = true);
        ApproveRejectModel GetExtend_dateCreditLetter(string SerialNo);    //add by rajan 22/01/25 
        //end

    }
}
