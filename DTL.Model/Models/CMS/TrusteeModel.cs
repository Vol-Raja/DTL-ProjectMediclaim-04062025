namespace DTL.Model.Models.CMS
{
    public class TrusteeModel : BaseModel
    {
        public bool IsDeleted { get; set; }
        public bool ReadOnly { get; set; }
        public bool IsNew { get; set; }

        public string NameEnglish { get; set; }
        public string NameHindi { get; set; }
        public string PositionEnglish { get; set; }
        public string PositionHindi { get; set; }
        public string Phone { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public string ImageContentType { get; set; }



    }
}
