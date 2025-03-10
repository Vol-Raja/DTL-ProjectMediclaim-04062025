using System;

namespace DTL.Model.Models.CMS
{
    public class WhatsNewModel : BaseModel
    {
        public string TitleInEnglish { get; set; }

        public string TitleInHindi { get; set; }

        public DateTime WhatsNewDate { get; set; }
        public DateTime WhatsNewDateHindi { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchieved { get; set; }

        public string AttachmentTitleInEnglish { get; set; }
        public string AttachmentTitleInHindi { get; set; }
       

        public byte[] AttachmentFileInEnglish { get; set; }
        public byte[] AttachmentFileInHindi { get; set; }
        public string EnglishFileName { get; set; }
        public string EnglishContentType { get; set; }
        public string HindiFileName { get; set; }
        public string HindiContentType { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }
        public string Description { get; set; }
        public string DescriptionHindi { get; set; }
        public string ImageFileName { get; set; }
        public string ImageContentType { get; set; }
        public byte[] Image { get; set; }

    }
}
