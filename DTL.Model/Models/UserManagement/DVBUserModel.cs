namespace DTL.Model.Models.UserManagement
{
    public class DVBUserModel : BaseModel
    {
        public int DVBUserId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Dashboard { get; set; }
        public string DashboardUrl { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsReadOnly { get; set; }
    }
}
