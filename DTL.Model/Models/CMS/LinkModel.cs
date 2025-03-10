namespace DTL.Model.Models.CMS
{
    public class LinkModel : BaseModel
    {
        public bool IsDeleted { get; set; }
        public bool IsArchieved { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }
        public string Title { get; set; }
        public byte[] FileContent { get; set; }
        public string DocumentFileName { get; set; }
        public string ContentType { get; set; }
        public int FileSize { get; set; }
        public string FileContentBase64 { get; set; }
        public string Link { get; set; }
        public string TitleHindi { get; set; }
    }
}
