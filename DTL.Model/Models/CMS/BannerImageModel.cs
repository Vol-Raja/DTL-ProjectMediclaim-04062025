namespace DTL.Model.Models.CMS
{
    public class BannerImageModel : BaseModel
    {
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }
        public string Description { get; set; }
        public string DescriptionHindi { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Image { get; set; }

        public bool IsGallery { get; set; }

    }
}
