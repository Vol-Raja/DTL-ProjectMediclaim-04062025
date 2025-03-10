using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.Testimony
{
    public class TestimonyService : ITestimonyService
    {
        private readonly IDapperService _dapper;

        public TestimonyService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }

        public string AddEditTestimonyData(TestimonyModel TestimonyModel, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? TestimonyModel.ID : null), DbType.Guid);
            dbparams.Add("@NameEnglish", TestimonyModel.NameEnglish, DbType.String);
            dbparams.Add("@NameHindi", TestimonyModel.NameHindi, DbType.String);
            dbparams.Add("@PositionEnglish", TestimonyModel.PositionEnglish, DbType.String);
            dbparams.Add("@PositionHindi", TestimonyModel.PositionHindi, DbType.String);
            dbparams.Add("@Description", TestimonyModel.Description, DbType.String);
            dbparams.Add("@DescriptionHindi", TestimonyModel.DescriptionHindi, DbType.String);
            dbparams.Add("@Image", TestimonyModel.Image, DbType.Binary);
            dbparams.Add("@ImageName", TestimonyModel.ImageName, DbType.String);
            dbparams.Add("@ImageContentType", TestimonyModel.ImageContentType, DbType.String);

            dbparams.Add("@IsDeleted", TestimonyModel.IsDeleted, DbType.Boolean);

            if (!isEdit)
                dbparams.Add("@CreatedBy", TestimonyModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", TestimonyModel.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditTestimony", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<TestimonyModel> GetTestimonyModelByParam(Guid? Id, bool? IsDeleted)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", Id, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);



            return _dapper.GetAll<TestimonyModel>("GetTestimonyByParam", dbparams);
        }

    }
}

