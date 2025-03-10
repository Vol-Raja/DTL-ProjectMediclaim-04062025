using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.Mediclaim.Detail.NonCashless
{
    public class NonCashlessDetail : INonCashlessDetail
    {
        private readonly IDapperService _dapper;
        public NonCashlessDetail(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }

        public NonCashlessModel GetNonCashlessByClaimId(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            var nonCashlessDetail = _dapper.Get<NonCashlessModel>("GetNonCashlessClaimByParam", dbparams);
            if (nonCashlessDetail != null)
            {
                nonCashlessDetail.OPDCNDList = GetOPDCNDList(claimId).AsList();
                if (nonCashlessDetail.ClaimFor.ToLower() == "dependent")
                {
                    nonCashlessDetail.Dependent = GetDependentInformationByParam(nonCashlessDetail.ClaimId);
                }
                nonCashlessDetail.Documents = GetMediclaimDocumentsByParam(null, claimId);
            }
            return nonCashlessDetail;
        }

        public IEnumerable<NonCashlessModel> GetNonCashlessList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NonCashlessModel> GetNonCashlessListByCreatedBy(string createdBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CreatedBy", createdBy, DbType.String);
            return _dapper.GetAll<NonCashlessModel>("GetNonCashlessClaimByParam", dbparams);

        }

        public IEnumerable<NonCashlessModel> GetNonCashlessListByStatus(int claimStatus)
        {
            throw new NotImplementedException();
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

        public IEnumerable<MediclaimDocumentModel> GetMediclaimDocumentsByParam(int? documentId, int? referenceId, bool active = true)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", documentId, DbType.Int32);
            dbparams.Add("@ReferenceId", referenceId, DbType.Int32);
            dbparams.Add("@ApplicationSubArea", "noncashless", DbType.String);
            dbparams.Add("@Active", active, DbType.Boolean);
            return _dapper.GetAll<MediclaimDocumentModel>("GetDocumentsByParam", dbparams);
        }

        public DependentInformation GetDependentInformationByParam(int nonCashlessClaimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@NonCashlessClaimId", nonCashlessClaimId, DbType.Int32);
            return _dapper.Get<DependentInformation>("GetDependentInformationByParam", dbparams);
        }

        public int UpdateCashlessDelete(int claimId, bool deleteFlag, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            dbparams.Add("@IsDelete", deleteFlag, DbType.Boolean);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            return _dapper.Execute("UpdateNonCashlessIsDelete", dbparams);
        }
    }
}
