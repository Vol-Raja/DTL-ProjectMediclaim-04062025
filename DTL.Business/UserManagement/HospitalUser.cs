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
    public class HospitalUser : IHospitalUser
    {
        private readonly IDapperService _dapper;

        public HospitalUser(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public int AddHospitalUser(HospitalUserModel hospitalUserModel)
        {
            int outputId = 0;

            var dbparams = new DynamicParameters();
            dbparams.Add("@ID", hospitalUserModel.ID, DbType.Guid);
            dbparams.Add("@NameOfHospital", hospitalUserModel.NameOfHospital, DbType.String);
            dbparams.Add("@EmailAddress", hospitalUserModel.EmailAddress, DbType.String);
            dbparams.Add("@PhoneNumber", hospitalUserModel.PhoneNumber, DbType.String);
            dbparams.Add("@TypeOfHospital", hospitalUserModel.TypeOfHospital, DbType.String);
            dbparams.Add("@AuthorizedPerson", hospitalUserModel.AuthorizedPerson, DbType.String);
            dbparams.Add("@Address", hospitalUserModel.Address, DbType.String);
            dbparams.Add("@CreatedBy", hospitalUserModel.CreatedBy, DbType.String);
            dbparams.Add("@HospitalUserId", outputId, DbType.Int32, direction: ParameterDirection.Output);
            dbparams.Add("@Username", hospitalUserModel.Username, DbType.String);
            _dapper.Execute("SaveHospitalUser", dbparams);
            outputId = dbparams.Get<int>("@HospitalUserId");
            return outputId;
        }

        public IEnumerable<HospitalUserModel> GetHospitalUserByParam(int? hospitaluserid, string nameOfHospital, string emailaddress, string typeOfHospital, string phoneNumber)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@NameOfHospital", nameOfHospital, DbType.String);
            dbparams.Add("@EmailAddress", emailaddress, DbType.String);
            dbparams.Add("@TypeOfHospital", typeOfHospital, DbType.String);
            dbparams.Add("@PhoneNumber", phoneNumber, DbType.String);
            dbparams.Add("@HospitalUserId", hospitaluserid, DbType.Int32);

            return _dapper.GetAll<HospitalUserModel>("GetHospitalUserByParam", dbparams);
        }

        public IEnumerable<HospitalUserModel> GetArchiveHospitalUser()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<HospitalUserModel>("GetArchiveHospitalUser", dbparams);
        }


        public void HospitalUserToggleIsDeleteFlag(int hospitaluserid, bool isDeleteFlag, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@HospitalUserId", hospitaluserid, DbType.Int32);
            dbparams.Add("@IsDelete", isDeleteFlag, DbType.Boolean);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            _dapper.Execute("HospitalUserToggleIsDeleteFlag", dbparams);
        }

        public int UpdateHospitalUser(HospitalUserModel hospitalUserModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@NameOfHospital", hospitalUserModel.NameOfHospital, DbType.String);
            dbparams.Add("@EmailAddress", hospitalUserModel.EmailAddress, DbType.String);
            dbparams.Add("@PhoneNumber", hospitalUserModel.PhoneNumber, DbType.String);
            dbparams.Add("@TypeOfHospital", hospitalUserModel.TypeOfHospital, DbType.String);
            dbparams.Add("@AuthorizedPerson", hospitalUserModel.AuthorizedPerson, DbType.String);
            dbparams.Add("@Address", hospitalUserModel.Address, DbType.String);
            dbparams.Add("@ModifiedBy", hospitalUserModel.CreatedBy, DbType.String);
            dbparams.Add("@HospitalUserId", hospitalUserModel.HospitalUserId, DbType.Int32);
            dbparams.Add("@Username", hospitalUserModel.Username, DbType.String);
            return _dapper.Execute("UpdateHospitalUser", dbparams);
        }
        public int DeleteHospitalUserPermanently(Guid id)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id", id);
            return _dapper.Execute($"Delete FROM HospitalUser WHERE Id=@Id", dbparams, CommandType.Text);
        }
        public HospitalUserModel GetHospitalUserByUsername(string username)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Username", username);

            return _dapper.Get<HospitalUserModel>($"Select * FROM HospitalUser WHERE Username=@Username", dbparams, CommandType.Text);
        }
    }
}
