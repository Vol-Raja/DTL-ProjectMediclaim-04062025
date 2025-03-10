using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.Mediclaim;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.Mediclaim.MedicalPageDetail
{
    public class MedicalPageDetail : IMedicalPageDetail
    {
        private readonly IDapperService _dapper;
        public MedicalPageDetail(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public int DeleteMedicalPageDetail(MedicalPageDetailModel medicalPageDetailModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PageDetailId", medicalPageDetailModel.PageDetailId, DbType.Int32);
            dbparams.Add("@IsDelete", medicalPageDetailModel.IsDelete, DbType.Boolean);
            dbparams.Add("@ModifiedBy", medicalPageDetailModel.ModifideBy, DbType.String);
            return _dapper.Execute("UpdateMedicalPageIsDelete", dbparams);
        }

        public IEnumerable<MedicalPageDetailModel> GetMedicalPageDetailsByParam(int? pageDetailId, string employeeNumber, string ppoNumber, string department, int? pageNumber, string createdBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PageDetailId", pageDetailId, DbType.Int32);
            dbparams.Add("@EmployeeNumber", employeeNumber, DbType.String);
            dbparams.Add("@PPONumber", ppoNumber, DbType.String);
            dbparams.Add("@Department", department, DbType.String);
            dbparams.Add("@PageNumber", pageNumber, DbType.Int32);
            dbparams.Add("@CreatedBy", createdBy, DbType.String);
            return _dapper.GetAll<MedicalPageDetailModel>("GetMedicalPageDetailByParam", dbparams);
        }

        public int SaveMedicalPageDetail(MedicalPageDetailModel medicalPageDetailModel)
        {
            int pageDetailId = 0;

            var dbparams = new DynamicParameters();
            dbparams.Add("@PageNumber", medicalPageDetailModel.PageNumber, DbType.String);
            dbparams.Add("@EmployeeNumber", medicalPageDetailModel.EmployeeNumber, DbType.String);
            dbparams.Add("@Name", medicalPageDetailModel.Name, DbType.String);
            dbparams.Add("@PPONumber", medicalPageDetailModel.PPONumber, DbType.String);
            dbparams.Add("@Designation", medicalPageDetailModel.Designation, DbType.String);
            dbparams.Add("@Department", medicalPageDetailModel.Department, DbType.String);
            dbparams.Add("@CardCategory", medicalPageDetailModel.CardCategory, DbType.String);
            dbparams.Add("@MobileNumber", medicalPageDetailModel.MobileNumber, DbType.String);
            dbparams.Add("@DateOfRetirement", medicalPageDetailModel.DateOfRetirement, DbType.Date);
            dbparams.Add("@LBP", medicalPageDetailModel.LBP, DbType.String);
            dbparams.Add("@SpouseName", medicalPageDetailModel.SpouseName, DbType.String);
            dbparams.Add("@Dispensary", medicalPageDetailModel.Dispensary, DbType.String);
            dbparams.Add("@NameOfDependent", medicalPageDetailModel.NameOfDependent, DbType.String);
            dbparams.Add("@RelationWithRetiree", medicalPageDetailModel.RelationWithRetiree, DbType.String);
            dbparams.Add("@DependentDob", medicalPageDetailModel.DependentDob, DbType.Date);
            dbparams.Add("@CreatedBy", medicalPageDetailModel.CreatedBy, DbType.String);
            dbparams.Add("@PageDetailId", pageDetailId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("SaveMedicalPageDetail", dbparams);
            return pageDetailId = dbparams.Get<int>("@PageDetailId");
        }

        public int UpdateMedicalPageDetail(MedicalPageDetailModel medicalPageDetailModel)
        { 
            var dbparams = new DynamicParameters();
            dbparams.Add("@PageNumber", medicalPageDetailModel.PageNumber, DbType.String);
            dbparams.Add("@EmployeeNumber", medicalPageDetailModel.EmployeeNumber, DbType.String);
            dbparams.Add("@Name", medicalPageDetailModel.Name, DbType.String);
            dbparams.Add("@PPONumber", medicalPageDetailModel.PPONumber, DbType.String);
            dbparams.Add("@Designation", medicalPageDetailModel.Designation, DbType.String);
            dbparams.Add("@Department", medicalPageDetailModel.Department, DbType.String);
            dbparams.Add("@CardCategory", medicalPageDetailModel.CardCategory, DbType.String);
            dbparams.Add("@MobileNumber", medicalPageDetailModel.MobileNumber, DbType.String);
            dbparams.Add("@DateOfRetirement", medicalPageDetailModel.DateOfRetirement, DbType.Date);
            dbparams.Add("@LBP", medicalPageDetailModel.LBP, DbType.String);
            dbparams.Add("@SpouseName", medicalPageDetailModel.SpouseName, DbType.String);
            dbparams.Add("@Dispensary", medicalPageDetailModel.Dispensary, DbType.String);
            dbparams.Add("@NameOfDependent", medicalPageDetailModel.NameOfDependent, DbType.String);
            dbparams.Add("@RelationWithRetiree", medicalPageDetailModel.RelationWithRetiree, DbType.String);
            dbparams.Add("@DependentDob", medicalPageDetailModel.DependentDob, DbType.Date);
            dbparams.Add("@ModifiedBy", medicalPageDetailModel.ModifideBy, DbType.String);
            dbparams.Add("@PageDetailId", medicalPageDetailModel.PageDetailId, DbType.Int32);

            return _dapper.Execute("UpdateMedicalPageDetail", dbparams); 
        }
    }
}
