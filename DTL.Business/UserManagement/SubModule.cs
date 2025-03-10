using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.UserManagement
{
    public class SubModule : ISubModule
    {
        private readonly IDapperService _dapper;
        public SubModule(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public int AddSubModule(SubModuleModel moduleModel)
        {
            int outputId = 0;
            
            var dbparams = new DynamicParameters();
            dbparams.Add("@ModuleName", moduleModel.ModuleName, DbType.String);
            dbparams.Add("@SubModuleName", moduleModel.SubModuleName, DbType.String);
            dbparams.Add("@CreatedBy", moduleModel.CreatedBy, DbType.String);
            dbparams.Add("@SubModuleId", outputId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("SaveSubModule", dbparams);
            outputId = dbparams.Get<int>("@SubModuleId");
            return outputId;
        }

        public IEnumerable<SubModuleModel> GetSubModuleByParam(int? submoduleid, string modulename,string submodulename)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@ModuleName", modulename, DbType.String);
            dbparams.Add("@SubModuleName", submodulename, DbType.String);
            dbparams.Add("@SubModuleId", submoduleid, DbType.Int32);
            
            return _dapper.GetAll<SubModuleModel>("GetSubModuleByParam", dbparams);
        }

        public IEnumerable<SubModuleModel> GetArchiveSubModule()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<SubModuleModel>("GetArchivedSubModule", dbparams);
        }

        public void DeleteSubModule(int submoduleid)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SubModuleId", submoduleid, DbType.Int32);
            _dapper.Execute("DeleteSubModule", dbparams);
        }

        public int UpdateSubModule(SubModuleModel subModuleModel)
        {
            var dbparams = new DynamicParameters();
            
            dbparams.Add("@SubModuleName", subModuleModel.SubModuleName, DbType.String);
            dbparams.Add("@ModuleName", subModuleModel.ModuleName, DbType.String);
            dbparams.Add("@ModifiedBy", subModuleModel.ModifideBy, DbType.String);
            dbparams.Add("@SubModuleId", subModuleModel.SubModuleId, DbType.Int32);

            return _dapper.Execute("UpdateSubModule", dbparams);
        }

    }
}
