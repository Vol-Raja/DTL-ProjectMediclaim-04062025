using DTL.Model.Models.GPF;
using System;
using System.Collections.Generic;

namespace DTL.Business.GPF.Withdrawal
{
    public interface IWithdrawal
    {
        IEnumerable<GPFWithdrawalModel> GetGPFWithdrawalByParam(int? withdrawId, string withdrawType, string accountHolderName, string employeeId, string employer, DateTime? dateOfJoining, DateTime? dateOfApplication, string createdBy);
        int SaveGPFWithdrawal(GPFWithdrawalModel withdrawModel);
        int DeleteGPFWithdrawal(int withdrawalId, string modifiedBy);
        IEnumerable<GPFDocumentModel> GetGPFDocumentsByParam(int? documentId, int? referenceId);
        void SaveDocuments(IEnumerable<GPFDocumentModel> gpfDocuments);
    }
}
