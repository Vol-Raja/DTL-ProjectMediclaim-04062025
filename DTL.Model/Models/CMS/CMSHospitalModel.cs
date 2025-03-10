using System;

namespace DTL.Model.Models.CMS
{
    public class CMSHospitalModel : BaseModel
    {
        public string NameInEnglish { get; set; }

        public string NameInHindi { get; set; }
         
        public bool IsDeleted { get; set; }

   
        public byte[] AttachmentFileInEnglish { get; set; }

        public string EnglishFileName { get; set; }
        public string EnglishContentType { get; set; }

        public byte[] AttachmentFileInHindi { get; set; }
        public string HindiFileName { get; set; }
        public string HindiContentType { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }


    }
}
