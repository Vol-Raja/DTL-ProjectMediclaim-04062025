using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.RuleActHelpline
{
    public class RuleActHelplineService : IRuleActHelplineService
    {
        private readonly IDapperService _dapper;

        public RuleActHelplineService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditRuleActHelplineData(RuleActHelplineModel model, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? model.ID : null), DbType.Guid);
            dbparams.Add("@TitleInEnglish", model.TitleInEnglish, DbType.String);
            dbparams.Add("@TitleInHindi", model.TitleInHindi, DbType.String);
            dbparams.Add("@ContactNumber", model.ContactNumber, DbType.String);
            dbparams.Add("@ContactNumberInHindi", model.ContactNumberInHindi, DbType.String);
            dbparams.Add("@HelplineDescription", model.HelplineDescription, DbType.String);
            dbparams.Add("@HelplineDescriptionInHindi", model.HelplineDescriptionInHindi, DbType.String);
            dbparams.Add("@IsHelpline", model.IsHelpline, DbType.Boolean);
            dbparams.Add("@AttachmentFileInEnglish", model.AttachmentFileInEnglish, DbType.Binary);
            dbparams.Add("@AttachmentFileInHindi", model.AttachmentFileInHindi, DbType.Binary);
            dbparams.Add("@EnglishFileName", model.EnglishFileName, DbType.String);
            dbparams.Add("@EnglishContentType", model.EnglishContentType, DbType.String);
            dbparams.Add("@HindiFileName", model.HindiFileName, DbType.String);
            dbparams.Add("@HindiContentType", model.HindiContentType, DbType.String);
            dbparams.Add("@IsDeleted", model.IsDeleted, DbType.Boolean);
           

            if (!isEdit)
                dbparams.Add("@CreatedBy", model.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", model.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditRuleActHelpline", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<RuleActHelplineModel> GetRuleActHelplineModelByParam(Guid? Id, bool? IsDeleted, bool? IsHelpline)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", Id, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
            dbparams.Add("@IsHelpline", IsHelpline, DbType.Boolean);

            return _dapper.GetAll<RuleActHelplineModel>("GetRuleActHelplineByParam", dbparams);
        }

    }
}

