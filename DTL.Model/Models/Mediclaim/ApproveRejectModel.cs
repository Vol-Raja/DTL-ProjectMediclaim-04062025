using System.Collections.Generic;

namespace DTL.Model.Models.Mediclaim
{
    public class ApproveRejectModel
    {
        public int ClaimId { get; set; }
        public int ClaimStatusId { get; set; }
        public string RejectReason { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string Remark { get; set; }
        public string CMORemark { get; set; }
        public string DealingAssistanceRemark { get; set; }
        public string ASORemark { get; set; }
        public string SORemark { get; set; }
        public string ModifiedBy { get; set; }
        public string Designation { get; set; }
        //Add new by rajan
        public int EmployeeNo { get; set; }

        public int StatusCreditId { get; set; }
        public string SerialNo { get; set; }
        public int Extended_Stay { get; set; }
        //public IEnumerable<MediclaimDocumentModel> Documents { get; set; }
        public IEnumerable<MediclaimDocumentModel> Documents { get; set; }
        //end
    }
}
