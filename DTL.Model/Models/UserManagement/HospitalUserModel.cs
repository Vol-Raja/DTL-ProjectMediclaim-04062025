namespace DTL.Model.Models.UserManagement
{
    public class HospitalUserModel : BaseModel
    {
		public int HospitalUserId { get; set; }
		public string NameOfHospital { get; set; }
		public string EmailAddress { get; set; }
		public string PhoneNumber { get; set; }
		public string TypeOfHospital { get; set; }
		public string AuthorizedPerson { get; set; }
		public string Address { get; set; }
        public string Username { get; set; }
        public bool IsDeleted { get; set; }

		public bool IsReadOnly { get; set; }


    }
}
