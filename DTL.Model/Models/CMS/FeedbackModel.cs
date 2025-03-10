namespace DTL.Model.Models.CMS
{
    public class FeedbackModel : BaseModel
    {

        public string Name { get; set; }

        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Details { get; set; }
        public string CaptchaCode { get; set; }
        
        public bool IsDeleted { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }
    }
}
