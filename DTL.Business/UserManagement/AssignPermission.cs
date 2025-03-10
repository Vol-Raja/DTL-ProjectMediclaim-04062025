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
    public class AssignPermission : IAssignPermission
    {
        private readonly IDapperService _dapper;

        public AssignPermission(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public int AddAssignPermission(AssignPermissionModel assignPermissionModel)
        {
            int outputId = 0;

            var dbparams = new DynamicParameters();
            dbparams.Add("@UserId", assignPermissionModel.UserId, DbType.String);
            dbparams.Add("@ModuleName", assignPermissionModel.ModuleName, DbType.String);
            dbparams.Add("@SubModuleName", assignPermissionModel.SubModuleName, DbType.String);
            dbparams.Add("@Create", assignPermissionModel.Create, DbType.Boolean);
            dbparams.Add("@View", assignPermissionModel.View, DbType.Boolean);
            dbparams.Add("@Edit", assignPermissionModel.Edit, DbType.Boolean);
            dbparams.Add("@Delete", assignPermissionModel.Delete, DbType.Boolean);
            dbparams.Add("@CreatedBy", assignPermissionModel.CreatedBy, DbType.String);
            dbparams.Add("@AssignPermissionId", outputId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("SaveAssignPermission", dbparams);
            outputId = dbparams.Get<int>("@AssignPermissionId");
            return outputId;
        }

        public void DeleteAssignPermission(int assignPermissionId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@AssignPermissionId",assignPermissionId, DbType.Int32);
            _dapper.Execute("DeleteAssignPermission", dbparams);
        }

        public IEnumerable<AssignPermissionModel> GetArchiveAssignPermission()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<AssignPermissionModel>("GetArchivedAssignPermission", dbparams);
        }

        public IEnumerable<AssignPermissionModel> GetAssignPermissionByParam(string userId, string modulename, string submodulename)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@UserId", userId, DbType.String);
            dbparams.Add("@ModuleName", modulename, DbType.String);
            dbparams.Add("@SubModuleName", submodulename, DbType.String);
            return _dapper.GetAll<AssignPermissionModel>("GetAssignPermissionByParam", dbparams);
        }

        public int UpdateAssignPermission(AssignPermissionModel assignPermissionModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ModuleName", assignPermissionModel.ModuleName, DbType.String);
            dbparams.Add("@SubModuleName", assignPermissionModel.SubModuleName, DbType.String);
            dbparams.Add("@Create", assignPermissionModel.Create, DbType.Boolean);
            dbparams.Add("@View", assignPermissionModel.View, DbType.Boolean);
            dbparams.Add("@Edit", assignPermissionModel.Edit, DbType.Boolean);
            dbparams.Add("@Delete", assignPermissionModel.Delete, DbType.Boolean);
            dbparams.Add("@ModifiedBy", assignPermissionModel.ModifideBy, DbType.String);
            dbparams.Add("@AssignPermissionId", assignPermissionModel.AssignPermissionId, DbType.Int32);

            return _dapper.Execute("UpdateAssignPermission", dbparams);
        }
    }
}
