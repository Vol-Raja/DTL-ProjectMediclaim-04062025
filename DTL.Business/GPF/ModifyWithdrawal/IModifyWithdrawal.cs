using DTL.Model.Models.GPF;
using System;
using System.Collections.Generic;

namespace DTL.Business.GPF.ModifyWithdrawal
{
    public interface IModifyWithdrawal
    {
        IEnumerable<GPFWithdrawalModel> GetGPFWithdrawalByParam(int? withdrawId, string withdrawType, string accountHolderName, string employeId, string employer, DateTime? dateOfJoining, DateTime? dateOfApplication, string createdBy);
        int UpdateGPFWithdrawal(GPFWithdrawalModel withdrawalModel);

        IEnumerable<GPFDocumentModel> GetGPFDocumentsByParam(int? documentId, int? referenceId, bool active = true);

        int UpdateDocumet(int documentId, bool active, string modifiedBy);
        void SaveDocumet(List<GPFDocumentModel> gpfDocumentModels);
    }
}
