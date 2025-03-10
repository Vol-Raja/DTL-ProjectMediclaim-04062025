using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;

namespace DTL.Business.Grievance
{
    public class AddGrievance : IAddGrievance
    {
        private readonly IDapperService _dapper;

        public AddGrievance(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }
        public string AddEditGrievance(GrievanceModel grievanceModel, bool isEdit)
        {
            var dbparam = new DynamicParameters();
            dbparam.Add("@ID", (isEdit ? grievanceModel.ID : null), DbType.Guid);
            dbparam.Add("@Department", grievanceModel.Department, DbType.String);
            dbparam.Add("@PPONo", grievanceModel.PPONo, DbType.String);
            dbparam.Add("@EmployeeId", grievanceModel.EmployeeId, DbType.Int64);
            dbparam.Add("@Subject", grievanceModel.Subject, DbType.String);
            dbparam.Add("@MobileNo", grievanceModel.MobileNo, DbType.String);
            dbparam.Add("@GrievanceDetails", grievanceModel.GrievanceDetails, DbType.String);
            dbparam.Add("@Name", grievanceModel.Name, DbType.String);
            dbparam.Add("@UserType", grievanceModel.UserType, DbType.String);
            dbparam.Add("@Gender", grievanceModel.Gender, DbType.String);
            dbparam.Add("@Office", grievanceModel.Office, DbType.String);
            dbparam.Add("@Remarks", grievanceModel.Remarks, DbType.String);
            dbparam.Add("@Status", grievanceModel.Status, DbType.String);
            dbparam.Add("@IsDeleted", grievanceModel.IsDeleted, DbType.Boolean);
            dbparam.Add("@EmailID", grievanceModel.EmailID, DbType.String);
            dbparam.Add("@Description", grievanceModel.Description, DbType.String);
            dbparam.Add("@EnglishFileName", grievanceModel.EnglishFileName, DbType.String);
            dbparam.Add("@EnglishContentType", grievanceModel.EnglishContentType, DbType.String);
            dbparam.Add("@AttachmentFileInEnglish", grievanceModel.AttachmentFileInEnglish, DbType.Binary);
            dbparam.Add("@Address", grievanceModel.Address, DbType.String);
            dbparam.Add("@GrievanceHeadEnglishFileName", grievanceModel.GrievanceHeadEnglishFileName, DbType.String);
            dbparam.Add("@GrievanceHeadEnglishContentType", grievanceModel.GrievanceHeadEnglishContentType, DbType.String);
            dbparam.Add("@GrievanceHeadAttachmentFileInEnglish", grievanceModel.GrievanceHeadAttachmentFileInEnglish, DbType.Binary);

            if (!isEdit)
                dbparam.Add("@CreatedBy", grievanceModel.CreatedBy, DbType.String);
            else
                dbparam.Add("@ModifideBy", grievanceModel.ModifideBy, DbType.String);

            dbparam.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("sp_AddEditGrievance", dbparam);
            var result = dbparam.Get<String>("@ReturnMsg");
            return result;
        }
        public async Task<IEnumerable<GrievanceModel>> GetGrievanceDetails()
        {
            var dbparams = new DynamicParameters();
            var data = await _dapper.GetAllAsync<GrievanceModel>("sp_GetGrievance", dbparams);
            return data;
        }
        public GrievanceModel GetGrievanceDetailsById(Guid grvId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", grvId, DbType.Guid);
            var data = _dapper.Get<GrievanceModel>("sp_GetGrievanceById", dbparams);
            if (data != null)
                data.ID = grvId;
            return data;
        }
        public GrievanceModel GetGrievanceDetailsByGrievanceId(int grvId)
        {
            var dbparams = new DynamicParameters();
            var data = _dapper.Get<GrievanceModel>($"SELECT * FROM Grievance WHERE GrievanceID={grvId}", dbparams, CommandType.Text);

            return data;
        }
        public int DeleteGrievance(Guid grvId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", grvId, DbType.Guid);
            return _dapper.Execute("sp_DeleteGrievance", dbparams);
        }
        public int GrievanceReplyMessage(GrievanceModel grievanceModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", grievanceModel.ID, DbType.Guid);
            dbparams.Add("@Reply", grievanceModel.Reply, DbType.String);
            dbparams.Add("@ModifiedBy", grievanceModel.ModifideBy, DbType.String);
            return _dapper.Execute("GrievanceReplyMessage", dbparams);
        }
    }
}
