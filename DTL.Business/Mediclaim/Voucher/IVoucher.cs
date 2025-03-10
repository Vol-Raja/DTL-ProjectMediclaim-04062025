using DTL.Model.Models.Mediclaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Mediclaim.Voucher
{
    public interface IVoucher
    {
        int SaveMediclaimVoucher(MediclaimVoucherModel mediclaimVoucher);
        int UpdateMediclaimVoucher(MediclaimVoucherModel mediclaimVoucher);
        IEnumerable<MediclaimVoucherModel> GetMediclaimVouchersByCreatedBy(string createdBy);
        IEnumerable<MediclaimVoucherModel> GetMediclaimVouchersByParam(int? voucherId, string payTo, string entryNo, string pageNo, int? claimId, string claimType);
        IEnumerable<MedicalCardModel> GetVoucherByPageNumber(string pageNo);
        CashlessModel GetCashlessDetailByClaimId(int claimId);
        NonCashlessModel GetNonCashlessDetailByClaimId(int claimId);
        decimal GetApprovedAmount(string claimType, int claimNumber);
        void ApproveVoucher(int VoucherId, string ModifiedBy, int VoucherStatusId);
        int UpdateVoucherDelete(MediclaimVoucherModel voucherModel);
        int DeleteVoucher(int voucherId);
    }
}