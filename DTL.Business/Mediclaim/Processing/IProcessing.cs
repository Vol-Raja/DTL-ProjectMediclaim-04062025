using DTL.Model.Models;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.Processing
{
    public interface IProcessing
    {
        /// <summary>
        ///     Processing for Cashless
        /// </summary>
        /// <returns></returns>
        IEnumerable<CashlessModel> GetPendingCashlessMediclaims();
        IEnumerable<CashlessModel> GetCashlessclaimByParam(string claimDate, int? claimId, int? claimStatusId, int? pageNumber, string cardCategory = null, string organization = null, string forwardTo = null);
        int ApproveRejectCashlessClaim(ApproveRejectModel approveRejectModel);
        CashlessModel GetCashlessByClaimId(int ClaimId);
        int UpdateCashlessPhysicalSubmit(PhysicalSubmitModel physicalSubmitModel);
        int DisburseCashlessClaim(int claimId, string modifiedBy);
        IEnumerable<CashlessModel> GetCashlessCreditlatter(string claimDate,  int? claimStatusId,string cardCategory = null, string forwardTo = null);

        /// <summary>
        ///     Processing for NonCashless
        /// </summary>        
        IEnumerable<NonCashlessModel> GetPendingNonCashlessMediclaims(string forwardTo=null);
        IEnumerable<NonCashlessModel> GetNonCashlessclaimByParam(string claimDate, int? claimId, int? claimStatusId, int? pageNumber, string cardCategory = null, string organization = null, string forwardTo = null);
        int ApproveRejectNonCashlessClaim(ApproveRejectModel approveRejectModel);
        NonCashlessModel GetNonCashlessByClaimId(int ClaimId);
        int UpdateNonCashlessPhysicalSubmit(PhysicalSubmitModel physicalSubmitModel);
        IEnumerable<NonCashlessModel> GetNonCashlessDisbursedClaim();
        int DisburseNonCashlessClaim(int claimId, string modifiedBy);

        /// <summary>
        ///     Update Forward To
        /// </summary>
        /// //Add New by rajan
        int ApproveRejectCashlessCredit(ApproveRejectModel approveRejectcredit);
        CashlessModel GetCashlessCreditByEmp(string SerialNo);
        IEnumerable<CashlessModel> GetCashlessByPPO(string PPO);  // add by rajan 26/02/2025
        void SaveCreditLetterDocuments(IEnumerable<MediclaimDocumentModel> Creditletter_Documents, int ID);


        //End
        int UpdateForwardTo(string forwardTo, string modifiedBy, int claimId, string claimType);

        //changed by nirbhay

        int UpdateForwardToHospital(string forwardTo, string modifiedBy, int employeeNo, string claimType);
        //end new changed

        IEnumerable<MediclaimDocumentModel> GetMediclaimDocumentsByParam(int? documentId, int? referenceId, string applicationSubArea, bool isActive = true);
        IEnumerable<NonCashlessModel> GetClaimsByASODateRange(DateTime startDate, DateTime endDate);  // add by nirbhay ExportToExcel 05/30/2025

        IEnumerable<NonCashlessModel> GetClaimsBymediclaimAMDMDateRange(DateTime startDate, DateTime endDate);  // add by nirbhay ExportToExcel 05/30/2025
        IEnumerable<NonCashlessModel> GetClaimsBymediclaimMediDisbusDateRange(DateTime startDate, DateTime endDate);  // add by nirbhay ExportToExcel 05/30/2025

        IEnumerable<NonCashlessModel> GetClaimsBymediclaimOPDDADateRange(DateTime startDate, DateTime endDate);  // add by nirbhay ExportToExcel 05/30/2025

        int UpdateDeductedAmounts(List<OPDCNDModel> deductions);
    }
}
