
namespace DTL.Model.Models.Mediclaim
{
    public class ClaimAndVoucherDetailModel
    {
        public CashlessModel Cashless { get; set; }
        public NonCashlessModel NonCashless { get; set; }
        public MediclaimVoucherModel Voucher { get; set; }
        //chnage by rjan 22/01/25
        public ApproveRejectModel approveReject { get; set; }
        //end
    }
}
