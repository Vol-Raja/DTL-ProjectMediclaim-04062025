using System;

namespace DTL.Model.Models.CMS
{
    public class SocialMediaAccountModel : BaseModel
    {
        public string Facebook { get; set; }

        public string Instagram { get; set; }


        public bool IsDeleted { get; set; }

        public string Youtube { get; set; }
        public string Twitter { get; set; }


        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }


    }
}
