using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.CMS.CMSHospital
{
    public class CMSHospitalService : ICMSHospitalService
    {
        private readonly IDapperService _dapper;

        public CMSHospitalService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditCMSHospitalData(CMSHospitalModel model, bool isEdit)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", (isEdit ? model.ID : null), DbType.Guid);
            dbparams.Add("@NameInEnglish", model.NameInEnglish, DbType.String);
            dbparams.Add("@NameInHindi", model.NameInHindi, DbType.String);

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
            _dapper.Execute("AddEditCMSHospital", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }
        public IEnumerable<CMSHospitalModel> GetCMSHospitalModelByParam(Guid? Id, bool? IsDeleted)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", Id, DbType.Guid);
            dbparams.Add("@IsDeleted", IsDeleted, DbType.Boolean);
          
            return _dapper.GetAll<CMSHospitalModel>("GetCMSHospitalByParam", dbparams);
        }

    }
}

