using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.WhatsNew
{
    public class WhatsNewService : IWhatsNewService
    {
        private readonly IDapperService _dapper;

        public WhatsNewService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditWhatsNewData(WhatsNewModel model, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? model.ID : null), DbType.Guid);
            dbparams.Add("@TitleInEnglish", model.TitleInEnglish, DbType.String);
            dbparams.Add("@TitleInHindi", model.TitleInHindi, DbType.String);

            dbparams.Add("@WhatsNewDate", model.WhatsNewDate, DbType.DateTime);
            dbparams.Add("@WhatsNewDateHindi", model.WhatsNewDateHindi, DbType.DateTime);
            dbparams.Add("@AttachmentTitleInEnglish", model.AttachmentTitleInEnglish, DbType.String);
            dbparams.Add("@AttachmentTitleInHindi", model.AttachmentTitleInHindi, DbType.String);
            dbparams.Add("@AttachmentFileInEnglish", model.AttachmentFileInEnglish, DbType.Binary);
            dbparams.Add("@AttachmentFileInHindi", model.AttachmentFileInHindi, DbType.Binary);
            dbparams.Add("@EnglishFileName", model.EnglishFileName, DbType.String);
            dbparams.Add("@EnglishContentType", model.EnglishContentType, DbType.String);
            dbparams.Add("@HindiFileName", model.HindiFileName, DbType.String);
            dbparams.Add("@HindiContentType", model.HindiContentType, DbType.String);
            dbparams.Add("@IsDeleted", model.IsDeleted, DbType.Boolean);
            dbparams.Add("@IsArchieved", model.IsArchieved, DbType.Boolean);
            dbparams.Add("@Description", model.Description, DbType.String);
            dbparams.Add("@DescriptionHindi", model.DescriptionHindi, DbType.String);
            dbparams.Add("@ImageFileName", model.ImageFileName, DbType.String);
            dbparams.Add("@ImageContentType", model.ImageContentType, DbType.String);
            dbparams.Add("@Image", model.Image, DbType.Binary);
          

            if (!isEdit)
                dbparams.Add("@CreatedBy", model.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", model.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditWhatsNew", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<WhatsNewModel> GetWhatsNewModelByParam(Guid? Id, bool? IsDeleted, bool? IsArchieved)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", Id, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
            dbparams.Add("@IsArchieved", IsArchieved, DbType.Boolean);


            return _dapper.GetAll<WhatsNewModel>("GetWhatsNewByParam", dbparams);
        }

    }
}

