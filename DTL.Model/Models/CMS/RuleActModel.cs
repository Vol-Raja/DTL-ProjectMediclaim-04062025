using System;

namespace DTL.Model.Models.CMS
{
    public class RuleActHelplineModel : BaseModel
    {
        public string TitleInEnglish { get; set; }

        public string TitleInHindi { get; set; }

     
        public bool IsDeleted { get; set; }

        public string ContactNumber { get; set; }
        public string ContactNumberInHindi { get; set; }
        public string HelplineDescription { get; set; }
        public string HelplineDescriptionInHindi { get; set; }

        public byte[] AttachmentFileInEnglish { get; set; }

        public string EnglishFileName { get; set; }
        public string EnglishContentType { get; set; }

        public byte[] AttachmentFileInHindi { get; set; }
        public string HindiFileName { get; set; }
        public string HindiContentType { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }
        public bool IsHelpline { get; set; }


    }
}
