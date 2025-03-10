namespace DTL.Model.Models.CMS
{
    public class AimVisionModel : BaseModel
    {
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }
        public string AimContent { get; set; }
        public string VisionContent { get; set; }
        public string AimContentHindi { get; set; }
        public string VisionContentHindi { get; set; }

    }
}
