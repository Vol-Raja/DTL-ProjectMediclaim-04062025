using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.Mediclaim;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.Mediclaim.Detail.Cashless
{
    public class CashlessDetail : ICashlessDetail
    {
        private readonly IDapperService _dapper;

        public CashlessDetail(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }

        public CashlessModel GetCashlessById(int ClaimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", ClaimId, DbType.Int32);
            var cashlessDetail = _dapper.Get<CashlessModel>("GetCashlessListByClaimId", dbparams);
            cashlessDetail.Documents = GetMediclaimDocumentsByParam(null, ClaimId);
            return cashlessDetail;
        }

        public IEnumerable<CashlessModel> GetCashlessList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CashlessModel> GetCashlessListByCreatedBy(string createdBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CreatedBy", createdBy, DbType.String);
            return _dapper.GetAll<CashlessModel>("GetCashlessListByCreatedBy", dbparams);
        }

        //NEW CHANGES BY NIRBHAY

        public IEnumerable<CashlessModel> GetCreditListByCreatedBy(string createdBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CreatedBy", createdBy, DbType.String);
            return _dapper.GetAll<CashlessModel>("GetCreditListByCreatedBy", dbparams);
        }

        //END NEW CHANGES

        public IEnumerable<CashlessModel> GetCashlessListByStatus(int claimStatus)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MediclaimDocumentModel> GetMediclaimDocumentsByParam(int? documentId, int? referenceId, bool active = true)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", documentId, DbType.Int32);
            dbparams.Add("@ReferenceId", referenceId, DbType.Int32);
            dbparams.Add("@ApplicationSubArea", "cashless", DbType.String);
            dbparams.Add("@Active", active, DbType.Boolean);
            return _dapper.GetAll<MediclaimDocumentModel>("GetDocumentsByParam", dbparams);
        }
        public CashlessModel GetMediclaimCreditLetter(string SerialNo)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@serialNo", SerialNo, DbType.String);
            var CashlessCredit = _dapper.Get<CashlessModel>("GetCreditLetterById", dbparams);
            CashlessCredit.Documents = GetCredit_Report_DocumentsByParam(null, null, SerialNo);
            return CashlessCredit;
        }
        public IEnumerable<MediclaimDocumentModel> GetCredit_Report_DocumentsByParam(int? documentId, int? referenceId, string SerialNO, bool active = true)
        {
            string number = SerialNO;
            string result = number.Substring(3);
            int ID = int.Parse(result);
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", documentId, DbType.Int32);
            dbparams.Add("@ReferenceId", ID, DbType.Int32);
            dbparams.Add("@ApplicationSubArea", "Creditletter", DbType.String);
            dbparams.Add("@Active", active, DbType.Boolean);
            return _dapper.GetAll<MediclaimDocumentModel>("GetCreditLetter_DocumentsByParam", dbparams);
        }

        public IEnumerable<MediclaimDocumentModel> GetExtend_Report_DocumentsByParam(int? documentId, int? referenceId, string ExtendDate, bool active = true)
        {
            string number = ExtendDate;
            string result = number.Substring(3);
            int ID = int.Parse(result);
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", documentId, DbType.Int32);
            dbparams.Add("@ReferenceId", ID, DbType.Int32);
            dbparams.Add("@ApplicationSubArea", "Extend_Date", DbType.String);
            dbparams.Add("@Active", active, DbType.Boolean);
            return _dapper.GetAll<MediclaimDocumentModel>("GetCreditLetter_DocumentsByParam", dbparams);
        }

        public ApproveRejectModel GetExtend_dateCreditLetter(string SerialNo)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@serialNo", SerialNo, DbType.String);
            var CashlessCredit = _dapper.Get<ApproveRejectModel>("GetCreditLetterById", dbparams);
            CashlessCredit.Documents = GetExtend_Report_DocumentsByParam(null, null, SerialNo);
            return CashlessCredit;
        }
    }
}
