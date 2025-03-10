using System;

namespace DTL.Model.Models.ApprovalLadder
{
    public class ApprovalLadderModel : BaseModel
    {
        public int LadderId { get; set; }
        public string Module { get; set; }
        public string LadderName { get; set; }
        public string Comment { get; set; }
        public string LadderStatus { get; set; }
        public int ReferenceId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
