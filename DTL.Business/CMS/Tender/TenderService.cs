using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.Tender
{
    public class TenderService : ITenderService
    {
        private readonly IDapperService _dapper;

        public TenderService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditTenderData(TenderModel tenderModel, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? tenderModel.ID : null), DbType.Guid);
            dbparams.Add("@TitleInEnglish", tenderModel.TitleInEnglish, DbType.String);
            dbparams.Add("@TitleInHindi", tenderModel.TitleInHindi, DbType.String);
            dbparams.Add("@TenderIdInHindi", tenderModel.TenderIdInHindi, DbType.String);
            dbparams.Add("@TenderIdEnglish", tenderModel.TenderIdEnglish, DbType.String);
            dbparams.Add("@OpeningDate", tenderModel.OpeningDate, DbType.DateTime);
            dbparams.Add("@OpeningTime", tenderModel.OpeningTime, DbType.DateTime);
            dbparams.Add("@PublishDate", tenderModel.PublishDate, DbType.DateTime);
            dbparams.Add("@PublishTime", tenderModel.PublishTime, DbType.DateTime);
            dbparams.Add("@ClosingDate", tenderModel.ClosingDate, DbType.DateTime);
            dbparams.Add("@ClosingTime", tenderModel.ClosingTime, DbType.DateTime);
            dbparams.Add("@DiscriptionInEnglish", tenderModel.DiscriptionInEnglish, DbType.String);
            dbparams.Add("@DiscriptionInHindi", tenderModel.DiscriptionInHindi, DbType.String);
            dbparams.Add("@AttachmentTitleInEnglish", tenderModel.AttachmentTitleInEnglish, DbType.String);
            dbparams.Add("@AttachmentTitleInHindi", tenderModel.AttachmentTitleInHindi, DbType.String);
            dbparams.Add("@AttachmentFileInEnglish", tenderModel.AttachmentFileInEnglish, DbType.Binary);
            dbparams.Add("@AttachmentFileInHindi", tenderModel.AttachmentFileInHindi, DbType.Binary);

            dbparams.Add("@IsDeleted", tenderModel.IsDeleted, DbType.Boolean);
            dbparams.Add("@IsArchieved", tenderModel.IsArchieved, DbType.Boolean);
            if (!isEdit)
                dbparams.Add("@CreatedBy", tenderModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", tenderModel.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditTender", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<TenderModel> GetTenderModelByParam(Guid? TenderId, bool? IsDeleted, bool? IsArchieved)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@TenderId", TenderId, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
            dbparams.Add("@IsArchieved", IsArchieved, DbType.Boolean);
      

            return _dapper.GetAll<TenderModel>("GetTenderByParam", dbparams);
        }

    }
}

