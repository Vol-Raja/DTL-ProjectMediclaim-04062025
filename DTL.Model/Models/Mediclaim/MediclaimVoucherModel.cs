namespace DTL.Model.Models.Mediclaim
{
    public class MediclaimVoucherModel : BaseModel
    {
        public int VoucherId { get; set; }
        public string PayTo { get; set; }

        //[DisplayFormat(DataFormatString = "{0:#.##}")]
        public decimal ApprovedAmount { get; set; }
        public string AmountInWords { get; set; }
        public int EntryNo { get; set; } = -1;
        public string PageNo { get; set; }
        public string BankBranch { get; set; }
        public string AccountNumber { get; set; }
        public string BICCode { get; set; }
        public string IFSCCode { get; set; }
        public int ClaimId { get; set; }
        public string ClaimType { get; set; }
        public bool IsDelete { get; set; }
        public int VoucherStatus { get; set; }
        public string BankName { get; set; }
        public string HospitalName { get; set; }

        public decimal ApprovedAmountInDecimal
        {
            get { return decimal.Round(ApprovedAmount, 2); }
        }

        public string CreatedDateString
        {
            get { return CreatedDate.ToString("dd-MM-yyyy"); }
        }
    }
}
