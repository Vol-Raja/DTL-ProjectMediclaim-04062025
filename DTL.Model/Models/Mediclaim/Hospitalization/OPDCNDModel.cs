using System;

namespace DTL.Model.Models.Mediclaim.Hospitalization
{
    public class OPDCNDModel : BaseModel
    {
        public int OPDCNDId { get; set; }
        public DateTime OPDCNDDate { get; set; }
        public string HospitalName { get; set; }
        public decimal MedicineAmount { get; set; }
        public decimal InvestigationAmount { get; set; }
        public decimal ConsultationAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string HospitalizationClaimType { get; set; }
        public decimal OtherAmount { get; set; }
        public int NonCashlessClaimId { get; set; }
        public string OPDCNDDateYYYYMMDD { get { return OPDCNDDate.ToString("yyyy-MM-dd"); } }
    }
}
