using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.UserManagement
{
     public class EmployeeUser :IEmployeeUser
    {
        private readonly IDapperService _dapper;
        public EmployeeUser(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public int AddEmployeeUser(EmployeeUserModel employeeUserModel)
        {
            int outputId = 0;

            var dbparams = new DynamicParameters();
            dbparams.Add("@ID", employeeUserModel.ID, DbType.Guid);
            dbparams.Add("@NameOfEmployee", employeeUserModel.NameOfEmployee, DbType.String);
            dbparams.Add("@EmailAddress", employeeUserModel.EmailAddress, DbType.String);
            dbparams.Add("@PhoneNumber", employeeUserModel.PhoneNumber, DbType.String);
            dbparams.Add("@AuthorizedPerson", employeeUserModel.AuthorizedPerson, DbType.String);
            dbparams.Add("@Address", employeeUserModel.Address, DbType.String);
            dbparams.Add("@CreatedBy", employeeUserModel.CreatedBy, DbType.String);
            dbparams.Add("@EmpUserid", outputId, DbType.Int32, direction: ParameterDirection.Output);
            dbparams.Add("@Username", employeeUserModel.Username, DbType.String);
            _dapper.Execute("SaveEmployeeUser", dbparams);
            outputId = dbparams.Get<int>("@EmpUserid");
            return outputId;
        }

        public IEnumerable<EmployeeUserModel> GetArchiveEmployeeUser()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<EmployeeUserModel>("GetArchiveEmployeeUser", dbparams);
        }

        public IEnumerable<EmployeeUserModel> GetEmployeeUserByParam(int? Empuserid, string nameOfemployee, string emailaddress, string phoneNumber)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@NameOfEmployee", nameOfemployee, DbType.String);
            dbparams.Add("@EmailAddress", emailaddress, DbType.String);
            dbparams.Add("@PhoneNumber", phoneNumber, DbType.String);
            dbparams.Add("@EmpUserid", Empuserid, DbType.Int32);

            return _dapper.GetAll<EmployeeUserModel>("GetEmployeeUserByParam", dbparams);
        }
    }
}
