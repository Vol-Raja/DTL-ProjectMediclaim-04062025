using System;

namespace DTL.Model.Models.Mediclaim.Hospitalization
{
    public class DispensaryModel : BaseModel
    {
        public int DispensaryId { get; set; }
        public DateTime DispensaryDate { get; set; }
        public string DispensaryLocation { get; set; }
        public string DoctorVisited { get; set; }
        public string TokenOPDNo { get; set; }
        public decimal MedicineAmount { get; set; }
        public string Investigation { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal OtherAmount { get; set; }
        public int NonCashlessClaimId { get; set; }

        public string DispensaryDateYYYYMMDD
        {
            get { return DispensaryDate.ToString("yyyy-MM-dd"); }
        }
    }
}
