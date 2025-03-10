using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.UserManagement
{
    public class AdminUser : IAdminUser
    {
        private readonly IDapperService _dapper;
        public AdminUser(IDapperService dapper)
        {
            _dapper = dapper;
        }
        public int AddAdminUser(AdminUserModel dvbUserModel)
        {
            int outputId = 0;
            
            var dbparams = new DynamicParameters();
            dbparams.Add("@ID", dvbUserModel.ID, DbType.Guid);
            dbparams.Add("@Name", dvbUserModel.Name, DbType.String);
            dbparams.Add("@EmailAddress", dvbUserModel.EmailAddress, DbType.String);
            dbparams.Add("@PhoneNumber", dvbUserModel.PhoneNumber, DbType.String);
            dbparams.Add("@Username", dvbUserModel.UserName, DbType.String);
            dbparams.Add("@CreatedBy", dvbUserModel.CreatedBy, DbType.String);
            dbparams.Add("@AdminUserId", outputId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("SaveAdminUser", dbparams);
            outputId = dbparams.Get<int>("@AdminUserId");
            return outputId;
        }
        public IEnumerable<AdminUserModel> GetAdminUserByParam(int? adminuserid, string name, string emailaddress, string phonenumber)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Name", name, DbType.String);
            dbparams.Add("@EmailAddress", emailaddress, DbType.String);
            dbparams.Add("@PhoneNumber", phonenumber, DbType.String);
            dbparams.Add("@AdminUserId", adminuserid, DbType.Int32);
            
            return _dapper.GetAll<AdminUserModel>("GetAdminUserByParam", dbparams);
        }
        public IEnumerable<AdminUserModel> GetArchiveAdminUser()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<AdminUserModel>("GetArchiveAdminUser", dbparams);
        }
        public void AdminUserToggleIsDeleteFlag(int adminuserid, bool isDeleteFlag, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@AdminUserId", adminuserid, DbType.Int32);
            dbparams.Add("@IsDelete", isDeleteFlag, DbType.Boolean);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            _dapper.Execute("AdminUserToggleIsDeleteFlag", dbparams);
        }
        public int UpdateAdminUser(AdminUserModel adminUserModel)
        {
            var dbparams = new DynamicParameters();
            
            dbparams.Add("@Name", adminUserModel.Name, DbType.String);
            dbparams.Add("@EmailAddress", adminUserModel.EmailAddress, DbType.String);
            dbparams.Add("@PhoneNumber", adminUserModel.PhoneNumber, DbType.String);
            dbparams.Add("@ModifiedBy", adminUserModel.ModifideBy, DbType.String);
            dbparams.Add("@AdminUserId", adminUserModel.AdminUserId, DbType.Int32);

            return _dapper.Execute("UpdateAdminUser", dbparams);
        }
        public int DeleteAdminUserPermanently(int adminuserId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("AdminUserId", adminuserId);

            return _dapper.Execute($"Delete FROM AdminUser WHERE AdminUserId=@AdminUserId", dbparams, CommandType.Text);
        }
        public AdminUserModel GetAdminUserByUsername(string username)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Username", username);

            return _dapper.Get<AdminUserModel>($"Select * FROM AdminUser WHERE Username=@Username", dbparams, CommandType.Text);
        }
    }
}
