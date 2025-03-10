using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IDapperService _dapper;

        public AuthService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }

        public int AddEditRolePageAccess(int id, string roleId, string pageName)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@id", (id == 0 ? 0 : id), DbType.Int32);
            dbparams.Add("@roleId", roleId, DbType.String);
            dbparams.Add("@pageName", pageName, DbType.String);
            dbparams.Add("@flag", (id == 0 ? 2 : 3), DbType.Int32);
            return _dapper.Execute("sp_AddEditRolePageAccess", dbparams);
        }

        public int DeleteRolePageAccess(int rolePageId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@id", rolePageId, DbType.Int32);
            dbparams.Add("@roleId", null, DbType.String);
            dbparams.Add("@pageName", null, DbType.String);
            dbparams.Add("@flag", 4, DbType.Int32);
            return _dapper.Execute("sp_AddEditRolePageAccess", dbparams);
        }

        public async Task<IEnumerable<RolePageModel>> GetRolePageAccessAsync()
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@id", 0, DbType.Int32);
            dbparams.Add("@roleId", null, DbType.String);
            dbparams.Add("@pageName", null, DbType.String);
            dbparams.Add("@flag", 0, DbType.Int32);
            var data = await _dapper.GetAllAsync<RolePageModel>("sp_AddEditRolePageAccess", dbparams);
            return data;
        }

        public RolePageModel GetRolePageAccessByIdAsync(int rolePageId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@id", rolePageId, DbType.Int32);
            dbparams.Add("@roleId", null, DbType.String);
            dbparams.Add("@pageName", null, DbType.String);
            dbparams.Add("@flag", 1, DbType.Int32);
            return  _dapper.Get<RolePageModel>("sp_AddEditRolePageAccess", dbparams);
        }

        public async Task<IEnumerable<RolePageModel>> GetRolePageAccessByIdAsync(string userId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@userId", userId, DbType.String);
            dbparams.Add("@rolePageId", null, DbType.Int32);
            return await _dapper.GetAllAsync<RolePageModel>("sp_GetUserRolePages", dbparams);
        }
    }
}
