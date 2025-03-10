using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.Mediclaim.Report
{
    public class Report : IReport
    {
        private readonly IDapperService _dapper;
        public Report(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public IEnumerable<MediclaimReportModel> GetMedilaimsForReportByParam(int? claimId,  string claimType, DateTime? fromDate, DateTime? toDate, int claimStatusId = 1)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            dbparams.Add("@ClaimType", claimType, DbType.String);
            dbparams.Add("@FromDate", fromDate, DbType.Date);
            dbparams.Add("@ToDate", toDate, DbType.Date);
            dbparams.Add("@ClaimStatusId", claimStatusId, DbType.Int32);
            return _dapper.GetAll<MediclaimReportModel>("GetMedilaimsForReportByParam", dbparams);
        }

        public CashlessModel GetCashlessById(int ClaimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", ClaimId, DbType.Int32);
            var cashlessDetail = _dapper.Get<CashlessModel>("GetCashlessListByClaimId", dbparams);

            return cashlessDetail;
        }

        public NonCashlessModel GetNonCashlessByClaimId(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            var nonCashlessDetail = _dapper.Get<NonCashlessModel>("GetNonCashlessDetailByClaimId", dbparams);
            nonCashlessDetail.OPDCNDList = GetOPDCNDList(claimId).AsList();
            return nonCashlessDetail;
        }

        public IEnumerable<DispensaryModel> GetDispensaries(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            return _dapper.GetAll<DispensaryModel>("GetDispensaryByClaimId", dbparams);
        }

        public IEnumerable<IPDModel> GetIPDList(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            return _dapper.GetAll<IPDModel>("GetIPDByClaimId", dbparams);
        }

        public IEnumerable<OPDCNDModel> GetOPDCNDList(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            return _dapper.GetAll<OPDCNDModel>("GetOPDCNDByClaimId", dbparams);
        }
    }
}
