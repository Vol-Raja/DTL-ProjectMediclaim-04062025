using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.Report
{
    public interface IReport
    {
        IEnumerable<MediclaimReportModel> GetMedilaimsForReportByParam(int? claimId, string claimType, DateTime? fromDate, DateTime? toDate, int claimStatusId = 1);
        CashlessModel GetCashlessById(int ClaimId);
        NonCashlessModel GetNonCashlessByClaimId(int ClaimId);
        IEnumerable<DispensaryModel> GetDispensaries(int claimId);
        IEnumerable<IPDModel> GetIPDList(int claimId);
        IEnumerable<OPDCNDModel> GetOPDCNDList(int claimId);
    }
}
