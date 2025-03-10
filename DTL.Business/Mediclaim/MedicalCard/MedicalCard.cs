using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.Mediclaim;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.Mediclaim.MedicalCard
{
    public class MedicalCard : IMedicalCard
    {
        private readonly IDapperService _dapper;
        private string _createdBy;

        public MedicalCard(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public IEnumerable<MedicalCardModel> GetMedicalCardsByCreatedBy(string createdBy)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@CreatedBy", createdBy, DbType.String);
            return _dapper.GetAll<MedicalCardModel>("GetMedicalCardByParam", dbparams);
        }

        public IEnumerable<MedicalCardModel> GetMedicalCardsByParam(int medicalCardId, string cardNo, string employeeNo, string ppoNo)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@MedicalCardId", medicalCardId, DbType.Int32);
            dbparams.Add("@CardNo", cardNo, DbType.String);
            dbparams.Add("@EmployeeNo", employeeNo, DbType.String);
            dbparams.Add("@PPONo", ppoNo, DbType.String);
            dbparams.Add("@CreatedBy", _createdBy, DbType.String);
            return _dapper.GetAll<MedicalCardModel>("GetMedicalCardByParam", dbparams);
        }

        public int SaveMedicalCard(MedicalCardModel medicalCardModel)
        {
            int outputMedicalCardId = 0;
            var dbparams = new DynamicParameters();
            dbparams.Add("@CardNo", medicalCardModel.CardNo, DbType.String);
            dbparams.Add("@EmployeeNo", medicalCardModel.EmployeeNo, DbType.String);
            dbparams.Add("@PPONo", medicalCardModel.PPONo, DbType.String);
            dbparams.Add("@NameOfCardHolder", medicalCardModel.NameOfCardHolder, DbType.String);
            dbparams.Add("@Employer", medicalCardModel.Employer, DbType.String);
            dbparams.Add("@DateOfBirth", medicalCardModel.DateOfBirth, DbType.Date);
            dbparams.Add("@Age", medicalCardModel.Age, DbType.Int32);
            dbparams.Add("@Gender", medicalCardModel.Gender, DbType.String);
            dbparams.Add("@MedicalSectionPageNo", medicalCardModel.MedicalSectionPageNo, DbType.Int32);
            dbparams.Add("@CardCategory", medicalCardModel.CardCategory, DbType.String);
            dbparams.Add("@MobileNumber", medicalCardModel.MobileNumber, DbType.String);
            dbparams.Add("@Address", medicalCardModel.Address, DbType.String);
            dbparams.Add("@MedicalHistory", medicalCardModel.MedicalHistory, DbType.String);
            dbparams.Add("@BankName", medicalCardModel.BankName, DbType.String);
            dbparams.Add("@BICCode", medicalCardModel.BICCode, DbType.String);
            dbparams.Add("@IFSCCode", medicalCardModel.IFSCCode, DbType.String);
            dbparams.Add("@AccountNumber", medicalCardModel.AccountNumber, DbType.String);
            dbparams.Add("@NameOfDependent", medicalCardModel.NameOfDependent, DbType.String);
            dbparams.Add("@RelationWithRetiree", medicalCardModel.RelationWithRetiree, DbType.String);
            dbparams.Add("@DependentDob", medicalCardModel.DependentDob, DbType.Date);
            dbparams.Add("@CreatedBy", medicalCardModel.CreatedBy, DbType.String);            
            //dbparams.Add("@CreatedDate", medicalCardModel.CreatedDate, DbType.DateTime);
            //dbparams.Add("@ModifiedBy", medicalCardModel.ModifideBy, DbType.String);
            //dbparams.Add("@ModifiedDate", medicalCardModel.ModifideDate, DbType.DateTime);
            dbparams.Add("@MedicalCardId", outputMedicalCardId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("SaveMedicalCard", dbparams);
            outputMedicalCardId = dbparams.Get<int>("@MedicalCardId");

            return outputMedicalCardId;
        }

        public int DeleteMedicalCard(MedicalCardModel medicalCardModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@MedicalCardId", medicalCardModel.MedicalCardId, DbType.Int32);
            dbparams.Add("@IsDelete", medicalCardModel.IsDelete, DbType.Boolean);
            dbparams.Add("@ModifiedBy", medicalCardModel.ModifideBy, DbType.String);
            return _dapper.Execute("UpdateMedicalCardIsDelete", dbparams);
        }

        public int UpdateMedicalCard(MedicalCardModel medicalCardModel)
        {   
            var dbparams = new DynamicParameters();
            dbparams.Add("@CardNo", medicalCardModel.CardNo, DbType.String);
            dbparams.Add("@EmployeeNo", medicalCardModel.EmployeeNo, DbType.String);
            dbparams.Add("@PPONo", medicalCardModel.PPONo, DbType.String);
            dbparams.Add("@NameOfCardHolder", medicalCardModel.NameOfCardHolder, DbType.String);
            dbparams.Add("@Employer", medicalCardModel.Employer, DbType.String);
            dbparams.Add("@DateOfBirth", medicalCardModel.DateOfBirth, DbType.Date);
            dbparams.Add("@Age", medicalCardModel.Age, DbType.Int32);
            dbparams.Add("@Gender", medicalCardModel.Gender, DbType.String);
            dbparams.Add("@MedicalSectionPageNo", medicalCardModel.MedicalSectionPageNo, DbType.Int32);
            dbparams.Add("@CardCategory", medicalCardModel.CardCategory, DbType.String);
            dbparams.Add("@MobileNumber", medicalCardModel.MobileNumber, DbType.String);
            dbparams.Add("@Address", medicalCardModel.Address, DbType.String);
            dbparams.Add("@MedicalHistory", medicalCardModel.MedicalHistory, DbType.String);
            dbparams.Add("@BankName", medicalCardModel.BankName, DbType.String);
            dbparams.Add("@BICCode", medicalCardModel.BICCode, DbType.String);
            dbparams.Add("@IFSCCode", medicalCardModel.IFSCCode, DbType.String);
            dbparams.Add("@AccountNumber", medicalCardModel.AccountNumber, DbType.String);
            dbparams.Add("@NameOfDependent", medicalCardModel.NameOfDependent, DbType.String);
            dbparams.Add("@RelationWithRetiree", medicalCardModel.RelationWithRetiree, DbType.String);
            dbparams.Add("@DependentDob", medicalCardModel.DependentDob, DbType.Date);
            dbparams.Add("@ModifiedBy", medicalCardModel.ModifideBy, DbType.String); 
            dbparams.Add("@MedicalCardId", medicalCardModel.MedicalCardId, DbType.Int32);

            return _dapper.Execute("UpdateMedicalCard", dbparams);  
        }
    }
}