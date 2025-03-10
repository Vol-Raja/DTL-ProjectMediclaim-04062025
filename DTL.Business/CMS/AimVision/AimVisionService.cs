using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.AimVision
{
    public class AimVisionService : IAimVisionService
    {
        private readonly IDapperService _dapper;

        public AimVisionService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditAimVisionData(AimVisionModel AimVisionModel, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? AimVisionModel.ID : null), DbType.Guid);
            dbparams.Add("@AimContent", AimVisionModel.AimContent, DbType.String);
            dbparams.Add("@VisionContent", AimVisionModel.VisionContent, DbType.String);
            dbparams.Add("@AimContentHindi", AimVisionModel.AimContentHindi, DbType.String);
            dbparams.Add("@VisionContentHindi", AimVisionModel.VisionContentHindi, DbType.String);

            dbparams.Add("@IsDeleted", AimVisionModel.IsDeleted, DbType.Boolean);
            dbparams.Add("@IsPublished", AimVisionModel.IsPublished, DbType.Boolean);
            if (!isEdit)
                dbparams.Add("@CreatedBy", AimVisionModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", AimVisionModel.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditAimVision", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<AimVisionModel> GetAimVisionModelByParam(Guid? AimVisionId, bool? IsDeleted, bool? IsPublished)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", AimVisionId, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
            dbparams.Add("@IsPublished", IsPublished, DbType.Boolean);


            return _dapper.GetAll<AimVisionModel>("GetAimVisionByParam", dbparams);
        }

    }
}

