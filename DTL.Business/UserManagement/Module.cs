using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.UserManagement
{
    public class Module : IModule
    {
        private readonly IDapperService _dapper;
        public Module(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public int AddModule(ModuleModel moduleModel)
        {
            int outputId = 0;
            
            var dbparams = new DynamicParameters();
            dbparams.Add("@ModuleName", moduleModel.ModuleName, DbType.String);
            dbparams.Add("@CreatedBy", moduleModel.CreatedBy, DbType.String);
            dbparams.Add("@ModuleId", outputId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("SaveModule", dbparams);
            outputId = dbparams.Get<int>("@ModuleId");
            return outputId;
        }

        public IEnumerable<ModuleModel> GetModuleByParam(int? moduleid, string modulename)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@ModuleName", modulename, DbType.String);
            dbparams.Add("@ModuleId", moduleid, DbType.Int32);
            
            return _dapper.GetAll<ModuleModel>("GetModuleByParam", dbparams);
        }

        public IEnumerable<ModuleModel> GetArchiveModule()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<ModuleModel>("GetArchivedModule", dbparams);
        }

        public void DeleteModule(int moduleid)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ModuleId", moduleid, DbType.Int32);
            _dapper.Execute("DeleteModule", dbparams);
        }

        public int UpdateModule(ModuleModel moduleModel)
        {
            var dbparams = new DynamicParameters();
            
            dbparams.Add("@ModuleName", moduleModel.ModuleName, DbType.String);
            dbparams.Add("@ModifiedBy", moduleModel.ModifideBy, DbType.String);
            dbparams.Add("@ModuleId", moduleModel.ModuleId, DbType.Int32);

            return _dapper.Execute("UpdateModule", dbparams);
        }

    }
}
