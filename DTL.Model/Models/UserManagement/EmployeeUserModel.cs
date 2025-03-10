using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models.UserManagement
{
   public class EmployeeUserModel
    {
		public int EmpUserid { get; set; }
		public string NameOfEmployee { get; set; }
		public string EmailAddress { get; set; }
		public string PhoneNumber { get; set; }
		public string AuthorizedPerson { get; set; }
		public string Address { get; set; }
		public string Username { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsReadOnly { get; set; }
        public string CreatedBy { get; set; }
        public Guid ID { get; set; }
        public object CreatedDate { get; set; }
    }
}
