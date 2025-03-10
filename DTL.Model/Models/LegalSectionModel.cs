using DTL.Model.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class LegalSectionModel : BaseModel
    {
        #region Properties
        public string CaseNo { get; set; }

        public string CourtType { get; set; }

        public DateTime? CaseInitialDate { get; set; }

        public DateTime? NextHearingDate { get; set; }

        public string PartiesInvolved { get; set; }

        // new properties
        public string PetitionerName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string StatusType { get; set; }

        public string Department { get; set; }

        public byte[] AttachmentFileInEnglish { get; set; }

        public string EnglishFileName { get; set; }
        public string EnglishContentType { get; set; }

        public string LegalAdvisorStatus { get; set; }
        public string LegalAdvisorRemarks { get; set; }

        // 

        public string NatureOfCase { get; set; }

        public string SummaryOfCase { get; set; }

        public string NameOfCouncil { get; set; }

        public string SubjectOfCase { get; set; }

        public DateTime? CaseEndDate { get; set; }

        public string CaseResult { get; set; }
        public string FileNumber { get; set; }

        public IEnumerable<CourtTypeList> CourtTypeList { get; set; }

        public bool? Approve { get; set; }
        public bool IsDeleted { get; set; }

        public bool IsReadOnly { get; set; }
        public bool IsNew { get; set; }
        #endregion

    }
}
