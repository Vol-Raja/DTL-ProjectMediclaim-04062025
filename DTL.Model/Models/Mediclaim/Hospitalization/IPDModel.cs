using System;

namespace DTL.Model.Models.Mediclaim.Hospitalization
{
    public class IPDModel : BaseModel
    {
        public int IPDId { get; set; }
        public DateTime DateOfAddmission { get; set; }
        public string HospitalName { get; set; }
        public string TreatmentIllsness { get; set; }
        public DateTime DateOfDischarge { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal OtherAmount { get; set; }
        public int NonCashlessClaimId { get; set; }
        public string DateOfAddmissionYYYYMMDD { get { return DateOfAddmission.ToString("yyyy-MM-dd"); } }
        public string DateOfDischargeYYYYMMDD { get { return DateOfDischarge.ToString("yyyy-MM-dd"); } }
    }
}
