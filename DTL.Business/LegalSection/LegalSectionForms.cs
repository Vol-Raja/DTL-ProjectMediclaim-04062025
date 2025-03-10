using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.LegalSection
{
    public class LegalSectionForms : ILegalSectionForm
    {
        private readonly IDapperService _dapper;

        public LegalSectionForms(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }

        public string AddEditLegalCaseDetails(LegalSectionModel legalSectionModel, bool isEdit)
        {
            var dbparam = new DynamicParameters();
            dbparam.Add("@ID", (isEdit ? legalSectionModel.ID : null), DbType.Guid);
            dbparam.Add("@CaseNo", legalSectionModel.CaseNo, DbType.String);
            dbparam.Add("@CourtType", legalSectionModel.CourtType, DbType.String);
            dbparam.Add("@FileNumber", legalSectionModel.FileNumber, DbType.String);
            dbparam.Add("@CaseInitialDate", legalSectionModel.CaseInitialDate, DbType.DateTime);
            if (legalSectionModel.NextHearingDate == DateTime.MinValue)
                legalSectionModel.NextHearingDate = null;
            dbparam.Add("@NextHearingDate", legalSectionModel.NextHearingDate, DbType.DateTime);
            dbparam.Add("@PartiesInvolved", legalSectionModel.PartiesInvolved, DbType.String);
            dbparam.Add("@NatureOfCase", legalSectionModel.NatureOfCase, DbType.String);
            dbparam.Add("@SummaryOfCase", legalSectionModel.SummaryOfCase, DbType.String);
            dbparam.Add("@NameOfCouncil", legalSectionModel.NameOfCouncil, DbType.String);
            dbparam.Add("@SubjectOfCase", legalSectionModel.SubjectOfCase, DbType.String);
            if (legalSectionModel.CaseEndDate == DateTime.MinValue)
                legalSectionModel.CaseEndDate = null;
            dbparam.Add("@CaseEndDate", legalSectionModel.CaseEndDate, DbType.DateTime);
            dbparam.Add("@CaseResult", legalSectionModel.CaseResult, DbType.String);
            if (!isEdit)
                dbparam.Add("@CreatedBy", legalSectionModel.CreatedBy, DbType.String);
            else
                dbparam.Add("@ModifideBy", legalSectionModel.ModifideBy, DbType.String);
            dbparam.Add("@PetitionerName", legalSectionModel.PetitionerName, DbType.String);
            dbparam.Add("@Email", legalSectionModel.Email, DbType.String);
            dbparam.Add("@Mobile", legalSectionModel.Mobile, DbType.String);
            dbparam.Add("@StatusType", legalSectionModel.StatusType, DbType.String);
            dbparam.Add("@Department", legalSectionModel.Department, DbType.String);
            dbparam.Add("@AttachmentFileInEnglish", legalSectionModel.AttachmentFileInEnglish, DbType.Binary);
            dbparam.Add("@EnglishFileName", legalSectionModel.EnglishFileName, DbType.String);
            dbparam.Add("@EnglishContentType", legalSectionModel.EnglishContentType, DbType.String);
            dbparam.Add("@LegalAdvisorRemarks", legalSectionModel.LegalAdvisorRemarks, DbType.String);
            dbparam.Add("@LegalAdvisorStatus", legalSectionModel.LegalAdvisorStatus, DbType.String);
            dbparam.Add("@ReturnMsg", "", DbType.String, ParameterDirection.Output);
            _dapper.Execute("usp_LegalCasesDetails_Upsert", dbparam);
            var result = dbparam.Get<String>("@ReturnMsg");
            return result;
        }

        public IEnumerable<LegalSectionModel> GetAllCases()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<LegalSectionModel>("usp_LegalCases_GetAll", dbparams);
        }

        public LegalSectionModel GetCaseDetailsById(Guid Id)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", Id, DbType.Guid);
            return _dapper.Get<LegalSectionModel>("usp_GetCasetDetailsById", dbparams);
        }

        public int ApproveLegalCases(Guid id, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", id, DbType.Guid);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            return _dapper.Execute("ApproveLegalCases", dbparams);
        }

        public int DeleteLegalCases(Guid id, string modifiedBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", id, DbType.Guid);
            dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
            return _dapper.Execute("DeleteLegalCases", dbparams);
        }
        public int GetLegalCaseCount()
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Count", "", DbType.Int32, ParameterDirection.Output);
            _dapper.Execute("GetLegalCaseCount", dbparams);
            return dbparams.Get<int>("@Count");

        }
        public IEnumerable<LegalSectionModel > GetCaseDetailsByCaseId(string CaseId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CaseId", CaseId, DbType.String);
            return _dapper.GetAll<LegalSectionModel>("SELECT TOP 1 * FROM LegalCases WHERE CaseNo = @CaseId", dbparams, CommandType.Text);
        }
    }
}
