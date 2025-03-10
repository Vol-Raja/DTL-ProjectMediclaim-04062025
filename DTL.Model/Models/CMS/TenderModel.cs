using System;

namespace DTL.Model.Models.CMS
{
    public class TenderModel : BaseModel
    {
        public string TitleInEnglish { get; set; }

        public string TitleInHindi { get; set; }
        public string TenderIdInHindi { get; set; }
        public string TenderIdEnglish { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime PublishTime { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime ClosingTime { get; set; }
        public string DiscriptionInEnglish { get; set; }

        public string DiscriptionInHindi { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchieved { get; set; }

        public string AttachmentTitleInEnglish { get; set; }

        public string AttachmentTitleInHindi { get; set; }
        public string AttachmentFileInHindiBase64 { get; set; }
        public string AttachmentFileInEnglishBase64 { get; set; }

        public byte[] AttachmentFileInEnglish { get; set; }
        public byte[] AttachmentFileInHindi { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }
    }
}
