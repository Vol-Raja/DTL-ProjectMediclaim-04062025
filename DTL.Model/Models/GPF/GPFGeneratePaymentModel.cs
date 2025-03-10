namespace DTL.Model.Models.GPF
{
    public class GPFGeneratePaymentModel : BaseModel
    {
        public int PaymentId { get; set; }
        public string ApplicationId { get; set; }
        public string NameOfEmployee { get; set; }
        public string ContactNo { get; set; }
        public string LoanType { get; set; }
        public int NoOfEMI { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string BICCode { get; set; }
    }
}
