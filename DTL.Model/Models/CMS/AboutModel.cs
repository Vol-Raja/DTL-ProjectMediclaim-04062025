namespace DTL.Model.Models.CMS
{
    public class AboutModel : BaseModel
    {
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }
        public string TextContent { get; set; }
        public string TextContentHindi { get; set; }

        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public string ImageContentType { get; set; }



    }
}
