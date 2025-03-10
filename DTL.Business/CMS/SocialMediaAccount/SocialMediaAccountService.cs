using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.SocialMediaAccount
{
    public class SocialMediaAccountService : ISocialMediaAccountService
    {
        private readonly IDapperService _dapper;

        public SocialMediaAccountService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditSocialMediaAccountData(SocialMediaAccountModel model, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? model.ID : null), DbType.Guid);
            dbparams.Add("@Facebook", model.Facebook, DbType.String);
            dbparams.Add("@Instagram", model.Instagram, DbType.String);
            dbparams.Add("@Youtube", model.Youtube, DbType.String);
            dbparams.Add("@Twitter", model.Twitter, DbType.String);
          
            dbparams.Add("@IsDeleted", model.IsDeleted, DbType.Boolean);
           
            if (!isEdit)
                dbparams.Add("@CreatedBy", model.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", model.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditSocialMediaAccount", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<SocialMediaAccountModel> GetSocialMediaAccountModelByParam(Guid? Id, bool? IsDeleted)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", Id, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
          
            return _dapper.GetAll<SocialMediaAccountModel>("GetSocialMediaAccountByParam", dbparams);
        }

    }
}

