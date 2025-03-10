using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Mediclaim.Voucher
{
    public class Voucher : IVoucher
    {
        private readonly IDapperService _dapper;
        private string _createdBy;

        public Voucher(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public void ApproveVoucher(int VoucherId, string ModifiedBy, int VoucherStatus)
        {  
            var dbparams = new DynamicParameters();
            dbparams.Add("@VoucherStatus", VoucherStatus, DbType.Int32);
            dbparams.Add("@VoucherId", VoucherId, DbType.Int32);
            dbparams.Add("@ModifiedBy", ModifiedBy, DbType.String);
            _dapper.Execute("UpdateVoucherStatus", dbparams);
        }

        public decimal GetApprovedAmount(string claimType, int claimNumber)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimType", claimType, DbType.String);
            dbparams.Add("@ClaimNumber", claimNumber, DbType.Int32);
            return _dapper.Get<decimal>("GetApprovedAmount", dbparams);
        }

        public IEnumerable<MediclaimVoucherModel> GetMediclaimVouchersByCreatedBy(string createdBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CreatedBy", createdBy, DbType.String);
            return _dapper.GetAll<MediclaimVoucherModel>("GetMediclaimVoucherByParam", dbparams);
        }

        public IEnumerable<MediclaimVoucherModel> GetMediclaimVouchersByParam(int? voucherId, string payTo, string entryNo, string pageNo, int? claimId,string claimType)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@VoucherId", voucherId, DbType.Int32);
            dbparams.Add("@PayTo", payTo, DbType.String);
            dbparams.Add("@EntryNo", entryNo, DbType.String);
            dbparams.Add("@PageNo", pageNo, DbType.String);
            dbparams.Add("@CreatedBy", _createdBy, DbType.String);
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            dbparams.Add("@ClaimType", claimType, DbType.String);
            return _dapper.GetAll<MediclaimVoucherModel>("GetMediclaimVoucherByParam", dbparams);
        }

        public IEnumerable<MedicalCardModel> GetVoucherByPageNumber(string pageNo)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PageNo", pageNo, DbType.String);
            return _dapper.GetAll<MedicalCardModel>("GetVoucherByPageNumber", dbparams);
        }

        public int SaveMediclaimVoucher(MediclaimVoucherModel mediclaimVoucher)
        { 
            int outputVoucherId = 0;
            var dbparams = new DynamicParameters();
            dbparams.Add("@PayTo", mediclaimVoucher.PayTo, DbType.String);
            dbparams.Add("@ApprovedAmount", mediclaimVoucher.ApprovedAmount, DbType.Decimal);
            dbparams.Add("@AmountInWords", mediclaimVoucher.AmountInWords, DbType.String);
            dbparams.Add("@EntryNo", mediclaimVoucher.EntryNo, DbType.Int32);
            dbparams.Add("@PageNo", mediclaimVoucher.PageNo, DbType.String);
            dbparams.Add("@BankBranch", mediclaimVoucher.BankBranch, DbType.String);
            dbparams.Add("@AccountNumber", mediclaimVoucher.AccountNumber, DbType.String);
            dbparams.Add("@BICCode", mediclaimVoucher.BICCode, DbType.String);
            dbparams.Add("@IFSCCode", mediclaimVoucher.IFSCCode, DbType.String);
            dbparams.Add("@CreatedBy", mediclaimVoucher.CreatedBy, DbType.String);
            dbparams.Add("@ClaimId", mediclaimVoucher.ClaimId, DbType.Int32);
            dbparams.Add("@ClaimType", mediclaimVoucher.ClaimType, DbType.String);
            dbparams.Add("@BankName", mediclaimVoucher.BankName, DbType.String);
            dbparams.Add("@HospitalName", mediclaimVoucher.HospitalName, DbType.String);
            //dbparams.Add("@ModifiedDate", mediclaimVoucher.ModifideDate, DbType.DateTime);
            dbparams.Add("@VoucherId", outputVoucherId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("SaveMediclaimVoucher", dbparams);
            outputVoucherId = dbparams.Get<int>("@VoucherId");

            return outputVoucherId;
        }

        public int UpdateMediclaimVoucher(MediclaimVoucherModel mediclaimVoucher)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PayTo", mediclaimVoucher.PayTo, DbType.String);
            dbparams.Add("@ApprovedAmount", mediclaimVoucher.ApprovedAmount, DbType.Decimal);
            dbparams.Add("@AmountInWords", mediclaimVoucher.AmountInWords, DbType.String);
            dbparams.Add("@EntryNo", mediclaimVoucher.EntryNo, DbType.String);
            dbparams.Add("@PageNo", mediclaimVoucher.PageNo, DbType.String);
            dbparams.Add("@BankBranch", mediclaimVoucher.BankBranch, DbType.String);
            dbparams.Add("@AccountNumber", mediclaimVoucher.AccountNumber, DbType.String);
            dbparams.Add("@BICCode", mediclaimVoucher.BICCode, DbType.String);
            dbparams.Add("@IFSCCode", mediclaimVoucher.IFSCCode, DbType.String);
            dbparams.Add("@ModifiedBy", mediclaimVoucher.ModifideBy, DbType.String);
            dbparams.Add("@ClaimId", mediclaimVoucher.ClaimId, DbType.Int32);
            dbparams.Add("@ClaimType", mediclaimVoucher.ClaimType, DbType.String);
            dbparams.Add("@BankName", mediclaimVoucher.BankName, DbType.String);
            dbparams.Add("@HospitalName", mediclaimVoucher.HospitalName, DbType.String);
            //dbparams.Add("@ModifiedDate", mediclaimVoucher.ModifideDate, DbType.DateTime);
            dbparams.Add("@VoucherId", mediclaimVoucher.VoucherId, DbType.Int32);

            return _dapper.Execute("UpdateMediclaimVoucher", dbparams);
        }

        public int UpdateVoucherDelete(MediclaimVoucherModel voucherModel)
        {
            #region Old Implementation
            //var dbparams = new DynamicParameters();
            //dbparams.Add("@VoucherId", voucherModel.VoucherId, DbType.Int32);
            //dbparams.Add("@IsDelete", voucherModel.IsDelete, DbType.Boolean);
            //dbparams.Add("@ModifiedBy", voucherModel.ModifideBy, DbType.String);
            //return _dapper.Execute("UpdateVoucherIsDelete", dbparams); 
            #endregion

            return DeleteVoucher(voucherModel.VoucherId);
        }

        public int DeleteVoucher(int voucherId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@VoucherId", voucherId);

            return _dapper.Execute($"Delete FROM dbo.MediclaimVoucher WHERE VoucherId=@VoucherId", dbparams, CommandType.Text);
        }

        public CashlessModel GetCashlessDetailByClaimId(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            var cashlessDetail = _dapper.Get<CashlessModel>("GetCashlessListByClaimId", dbparams);            
            return cashlessDetail;
        }

        public NonCashlessModel GetNonCashlessDetailByClaimId(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            var nonCashlessDetail = _dapper.Get<NonCashlessModel>("GetNonCashlessDetailByClaimId", dbparams);
            nonCashlessDetail.OPDCNDList = _dapper.GetAll<OPDCNDModel>("GetOPDCNDByClaimId", dbparams).ToList();
            return nonCashlessDetail;
        }
    }
}