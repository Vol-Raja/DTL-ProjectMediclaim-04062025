using DTL.Model.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.UserManagement
{
    public interface IEmployeeUser
    {
        int AddEmployeeUser(EmployeeUserModel employeeUserModel);
        IEnumerable<EmployeeUserModel> GetEmployeeUserByParam(int? Empuserid, string nameOfemployee, string emailaddress, string phoneNumber);
        IEnumerable<EmployeeUserModel> GetArchiveEmployeeUser();
    }
}
