namespace DTL.Model.Models.CMS
{
    public class ContactUsModel : BaseModel
    {

        public string Name { get; set; }
        public string NameHindi { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string DesignationHindi { get; set; }
        public string Designation { get; set; }
        public string Telephone { get; set; }

        public bool IsDeleted { get; set; }
        public bool ViewOnly { get; set; }
        public bool IsNew { get; set; }
    }
}
