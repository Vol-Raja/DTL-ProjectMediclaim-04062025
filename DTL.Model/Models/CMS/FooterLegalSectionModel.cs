using System;

namespace DTL.Model.Models.CMS
{
    public class FooterLegalSectionModel : BaseModel
    {
        public string CopyrightPolicy { get; set; }

        public string CMAPPolicy  { get; set; }

        
        public bool IsDeleted { get; set; }

        public string CAPPolicy { get; set; }
        public string ContentReviewPolicy { get; set; }

        public string Disclaimer { get; set; }

        public string HyperlinkPolicy { get; set; }
        public string TermsConditionPolicy { get; set; }

        public string ContingencyManagementPlanPolicy { get; set; }
        public string PrivacyPolicy { get; set; }
        public string WebsiteMonitoringPlanPolicy { get; set; }
        public string SecurityPolicy { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }


    }
}
