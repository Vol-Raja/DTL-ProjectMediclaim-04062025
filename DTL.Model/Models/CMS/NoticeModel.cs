using System;

namespace DTL.Model.Models.CMS
{
    public class NoticeModel : BaseModel
    {
        public string TitleInEnglish { get; set; }

        public string TitleInHindi { get; set; }
       
        public DateTime NoticeDate { get; set; }
        public DateTime NoticeDateHindi { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchieved { get; set; }

        public string AttachmentTitleInEnglish { get; set; }
        public string AttachmentTitleInHindi { get; set; }
        public string AttachmentFileInHindiBase64 { get; set; }
        public string AttachmentFileInEnglishBase64 { get; set; }

        public byte[] AttachmentFileInEnglish { get; set; }
        public byte[] AttachmentFileInHindi { get; set; }
        public string EnglishFileName { get; set; }
        public string EnglishContentType { get; set; }
        public string HindiFileName { get; set; }
        public string HindiContentType { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }
    }
}
