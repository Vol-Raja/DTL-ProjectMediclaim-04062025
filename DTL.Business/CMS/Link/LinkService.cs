using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.Link
{
    public class LinkService : ILinkService
    {
        private readonly IDapperService _dapper;

        public LinkService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditLinkData(LinkModel linkModel, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? linkModel.ID : null), DbType.Guid);
            dbparams.Add("@Title", linkModel.Title, DbType.String);
            dbparams.Add("@TitleHindi", linkModel.TitleHindi, DbType.String);
            dbparams.Add("@Link", linkModel.Link, DbType.String);
            dbparams.Add("@DocumentFileName", linkModel.DocumentFileName, DbType.String);
            dbparams.Add("@ContentType", linkModel.ContentType, DbType.String);
            dbparams.Add("@FileSize", linkModel.FileSize, DbType.String);
            dbparams.Add("@FileContent", linkModel.FileContent, DbType.Binary);

            dbparams.Add("@IsDeleted", linkModel.IsDeleted, DbType.Boolean);
            dbparams.Add("@IsArchieved", linkModel.IsArchieved, DbType.Boolean);
            if (!isEdit)
                dbparams.Add("@CreatedBy", linkModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", linkModel.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditLink", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<LinkModel> GetLinkModelByParam(Guid? LinkId, bool? IsDeleted, bool? IsArchieved)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@LinkId", LinkId, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
            dbparams.Add("@IsArchieved", IsArchieved, DbType.Boolean);


            return _dapper.GetAll<LinkModel>("GetLinkByParam", dbparams);
        }

    }
}

