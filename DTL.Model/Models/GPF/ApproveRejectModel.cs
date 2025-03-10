namespace DTL.Model.Models.GPF
{
    public class ApproveRejectModel
    {
        public int WithdrawId { get; set; }
        public int ApplicationStatusId { get; set; }
        public string RejectReason { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string ModifiedBy { get; set; }
    }
}
