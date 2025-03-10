using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models.UserManagement
{
    public class AdminUserModel : BaseModel
    {
        public int AdminUserId { get;set;}
        public string Name { get;set;}
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
