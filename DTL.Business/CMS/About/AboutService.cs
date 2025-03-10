using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.About
{
    public class AboutService : IAboutService
    {
        private readonly IDapperService _dapper;

        public AboutService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditAboutData(AboutModel AboutModel, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? AboutModel.ID : null), DbType.Guid);
            dbparams.Add("@TextContent", AboutModel.TextContent, DbType.String);
            dbparams.Add("@TextContentHindi", AboutModel.TextContentHindi, DbType.String);
            dbparams.Add("@Image", AboutModel.Image, DbType.Binary);
            dbparams.Add("@ImageName", AboutModel.ImageName, DbType.String);
            dbparams.Add("@ImageContentType", AboutModel.ImageContentType, DbType.String);
           
            dbparams.Add("@IsDeleted", AboutModel.IsDeleted, DbType.Boolean);
            dbparams.Add("@IsPublished", AboutModel.IsPublished, DbType.Boolean);
            if (!isEdit)
                dbparams.Add("@CreatedBy", AboutModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", AboutModel.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditAbout", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<AboutModel> GetAboutModelByParam(Guid? AboutId, bool? IsDeleted, bool? IsPublished)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", AboutId, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
            dbparams.Add("@IsPublished", IsPublished, DbType.Boolean);


            return _dapper.GetAll<AboutModel>("GetAboutByParam", dbparams);
        }

        public string AddEditTrusteeData(TrusteeModel TrusteeModel, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? TrusteeModel.ID : null), DbType.Guid);
            dbparams.Add("@NameEnglish", TrusteeModel.NameEnglish, DbType.String);
            dbparams.Add("@NameHindi", TrusteeModel.NameHindi, DbType.String);
            dbparams.Add("@PositionEnglish", TrusteeModel.PositionEnglish, DbType.String);
            dbparams.Add("@PositionHindi", TrusteeModel.PositionHindi, DbType.String);
            dbparams.Add("@Phone", TrusteeModel.Phone, DbType.String);
            dbparams.Add("@Image", TrusteeModel.Image, DbType.Binary);
            dbparams.Add("@ImageName", TrusteeModel.ImageName, DbType.String);
            dbparams.Add("@ImageContentType", TrusteeModel.ImageContentType, DbType.String);

            dbparams.Add("@IsDeleted", TrusteeModel.IsDeleted, DbType.Boolean);
          
            if (!isEdit)
                dbparams.Add("@CreatedBy", TrusteeModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", TrusteeModel.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditTrustee", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<TrusteeModel> GetTrusteeModelByParam(Guid? Id, bool? IsDeleted)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", Id, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
          


            return _dapper.GetAll<TrusteeModel>("GetTrusteeByParam", dbparams);
        }

    }
}

