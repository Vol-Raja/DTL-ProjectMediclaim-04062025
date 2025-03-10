using DTL.Model.Models;
using DTL.Model.Models.CMS;
using System.Collections.Generic;

namespace DTL.Website.Models
{
    public class HomeModel
    {
        public IEnumerable<LinkModel> Links { get; set; }
        public IEnumerable<NoticeModel> Notices { get; set; }
        public IEnumerable<WhatsNewModel> WhatsNews { get; set; }
        public IEnumerable<TenderModel> Tenders { get; set; }
        public IEnumerable<TestimonyModel> Testimonies { get; set; }
        public IEnumerable<BannerImageModel> BannerImages { get; set; }
        public IEnumerable<EventModel> Events { get; set; }
        public AboutModel AboutUs { get; set; }

        public AimVisionModel AimVision { get; set; }
        public SocialMediaAccountModel SocialMediaAccount { get; set; }
        public VisitorModel VisitorModel { get; set; }


    }
}
