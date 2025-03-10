using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.BannerImage
{
    public class BannerImageService : IBannerImageService
    {
        private readonly IDapperService _dapper;

        public BannerImageService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditBannerImageData(BannerImageModel BannerImageModel, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? BannerImageModel.ID : null), DbType.Guid);
            dbparams.Add("@Description", BannerImageModel.Description, DbType.String);
            dbparams.Add("@DescriptionHindi", BannerImageModel.DescriptionHindi, DbType.String);
            dbparams.Add("@Image", BannerImageModel.Image, DbType.Binary);
            dbparams.Add("@FileName", BannerImageModel.FileName, DbType.String);
            dbparams.Add("@ContentType", BannerImageModel.ContentType, DbType.String);
            dbparams.Add("@IsDeleted", BannerImageModel.IsDeleted, DbType.Boolean);
            dbparams.Add("@IsPublished", BannerImageModel.IsPublished, DbType.Boolean);
            dbparams.Add("@IsGallery", BannerImageModel.IsGallery, DbType.Boolean);
            if (!isEdit)
                dbparams.Add("@CreatedBy", BannerImageModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", BannerImageModel.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditBannerImage", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<BannerImageModel> GetBannerImageModelByParam(Guid? BannerImageId, bool? IsDeleted, bool? IsPublished, bool? IsGallery)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", BannerImageId, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
            dbparams.Add("@IsPublished", IsPublished, DbType.Boolean);
            dbparams.Add("@IsGallery", IsGallery, DbType.Boolean);

            return _dapper.GetAll<BannerImageModel>("GetBannerImageByParam", dbparams);
        }

    }
}

