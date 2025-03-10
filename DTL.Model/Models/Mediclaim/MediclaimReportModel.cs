using System;

namespace DTL.Model.Models.Mediclaim
{
    public class MediclaimReportModel
    {
        public int ClaimId { get; set; }
        public string MedicalSectionPageNumber { get; set; }
        public string PatientName { get; set; }
        public string ApplicationType { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string RejectReason { get; set; }
        public string CreatedDateString { get { return CreatedDate.ToShortDateString(); } }
    }
}
