using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.FooterLegalSection
{
    public class FooterLegalSectionService : IFooterLegalSectionService
    {
        private readonly IDapperService _dapper;

        public FooterLegalSectionService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditFooterLegalSectionData(FooterLegalSectionModel model, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? model.ID : null), DbType.Guid);
            dbparams.Add("@CopyrightPolicy", model.CopyrightPolicy, DbType.String);
            dbparams.Add("@CMAPPolicy", model.CMAPPolicy, DbType.String);

            dbparams.Add("@CAPPolicy", model.CAPPolicy, DbType.String);
           
            dbparams.Add("@ContentReviewPolicy", model.ContentReviewPolicy, DbType.String);
            dbparams.Add("@Disclaimer", model.Disclaimer, DbType.String);
            dbparams.Add("@HyperlinkPolicy", model.HyperlinkPolicy, DbType.String);
            dbparams.Add("@TermsConditionPolicy", model.TermsConditionPolicy, DbType.String);
            dbparams.Add("@ContingencyManagementPlanPolicy", model.ContingencyManagementPlanPolicy, DbType.String);
            dbparams.Add("@PrivacyPolicy", model.PrivacyPolicy, DbType.String);
            dbparams.Add("@WebsiteMonitoringPlanPolicy", model.WebsiteMonitoringPlanPolicy, DbType.String);
            dbparams.Add("@SecurityPolicy", model.SecurityPolicy, DbType.String);

            dbparams.Add("@IsDeleted", model.IsDeleted, DbType.Boolean);
           

            if (!isEdit)
                dbparams.Add("@CreatedBy", model.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifiedBy", model.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditFooterLegalSection", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<FooterLegalSectionModel> GetFooterLegalSectionModelByParam(Guid? Id, bool? IsDeleted)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", Id, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
          
            return _dapper.GetAll<FooterLegalSectionModel>("GetFooterLegalSectionByParam", dbparams);
        }

    }
}

