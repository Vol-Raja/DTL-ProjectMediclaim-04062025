
using Dapper;
using DTL.Business.Dapper;
using DTL.Model;
using DTL.Model.Models;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System.Collections.Generic;
using System.Data;
using System;

namespace DTL.Business.Mediclaim.MediclaimRaise
{
    public class Raise : IRaise
    {
        private readonly IDapperService _dapper;
        private string _createdBy;

        public Raise(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }

        /// <summary>
        /// Create new cashless claim
        /// </summary>
        /// <param name="cashless"></param>
        /// <returns>Unique Claim Id</returns>
        public int SaveMediclaimCashless(CashlessModel cashless)
        {
            int outputClaimId = 0;
            var dbparams = new DynamicParameters();
            dbparams.Add("@NameOfHospital", cashless.NameOfHospital, DbType.String);
            dbparams.Add("@HospitalPhoneNumber", cashless.HospitalPhoneNumber, DbType.String);
            dbparams.Add("@HospitalAddress", cashless.HospitalAddress, DbType.String);
            dbparams.Add("@HospitalId", cashless.HospitalId, DbType.String);
            dbparams.Add("@EmailId", cashless.EmailId, DbType.String);
            dbparams.Add("@NameOfPatient", cashless.NameOfPatient, DbType.String);
            dbparams.Add("@PatientEmailId", cashless.PatientEmailId, DbType.String);
            dbparams.Add("@Gender", cashless.Gender, DbType.String);
            dbparams.Add("@PatientPhoneNumber", cashless.PatientPhoneNumber, DbType.String);
            dbparams.Add("@PatientAddress", cashless.PatientAddress, DbType.String);
            dbparams.Add("@DateOfBirth", cashless.DateOfBirth, DbType.Date);
            dbparams.Add("@Age", cashless.Age, DbType.String);
            dbparams.Add("@MedicalSectionPageNumber", cashless.MedicalSectionPageNumber, DbType.String);
            dbparams.Add("@NameOfCardHolder", cashless.NameOfCardHolder, DbType.String);
            dbparams.Add("@MedicalCardNumber", cashless.MedicalCardNumber, DbType.String);
            dbparams.Add("@AdmissionNumber", cashless.AdmissionNumber, DbType.String);
            dbparams.Add("@CardCategory", cashless.CardCategory, DbType.String);
            dbparams.Add("@CaseType", cashless.CaseType, DbType.String);
            dbparams.Add("@TypeOfTreatment", cashless.TypeOfTreatment, DbType.String);
            dbparams.Add("@Amount", cashless.Amount, DbType.Decimal);
            dbparams.Add("@DateOfAdmission", cashless.DateOfAdmission, DbType.Date);
            dbparams.Add("@DateOfDischargeOrDeath", cashless.DateOfDischargeOrDeath, DbType.Date);
            dbparams.Add("@ClaimStatusId", cashless.ClaimStatusId, DbType.Int32);
            dbparams.Add("@CreatedBy", cashless.CreatedBy, DbType.String);
            dbparams.Add("@AccountHolderName", cashless.AccountHolderName, DbType.String);
            dbparams.Add("@AccountNumber", cashless.AccountNumber, DbType.String);
            dbparams.Add("@RelationWithRetire", cashless.RelationWithRetire, DbType.String);
            dbparams.Add("@DependantDOB", cashless.DependantDOB, DbType.Date);
            dbparams.Add("@DependantAge", cashless.DependantAge, DbType.Int32);
            dbparams.Add("@BankName", cashless.BankName, DbType.String);
            dbparams.Add("@BICCode", cashless.BICCode, DbType.String);
            dbparams.Add("@IFSCNumber", cashless.IFSCNumber, DbType.String);
            dbparams.Add("@BranchName", cashless.BranchName, DbType.String);
            dbparams.Add("@PPONumber", cashless.PPONumber, DbType.String);
            dbparams.Add("@Organization", cashless.Organization, DbType.String);
            dbparams.Add("@Department", cashless.Department, DbType.String);
            dbparams.Add("@Designation", cashless.Designation, DbType.String);
            dbparams.Add("@DateOfRetirement", cashless.DateOfRetirement, DbType.Date);
            dbparams.Add("@ClaimId", outputClaimId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("SaveMediclaimCashless", dbparams);
            outputClaimId = dbparams.Get<int>("@ClaimId");
            if (outputClaimId > 0)
            {
                SaveDocuments(cashless.Documents, outputClaimId);
            }

            return outputClaimId;
        }

        //New changes by nirbhay

        public int SaveAddNewAdmission(CashlessModel cashless)
        {
            int outputId = 0;
            var dbparams = new DynamicParameters();
            dbparams.Add("@NameOfHospital", cashless.NameOfHospital, DbType.String);
            dbparams.Add("@HospitalPhoneNumber", cashless.HospitalPhoneNumber, DbType.String);
            dbparams.Add("@HospitalAddress", cashless.HospitalAddress, DbType.String);
            dbparams.Add("@HospitalId", cashless.HospitalId, DbType.String);
            dbparams.Add("@EmailId", cashless.EmailId, DbType.String);
            dbparams.Add("@PatientName", cashless.PatientName, DbType.String);
            dbparams.Add("@PensionerName", cashless.PensionerName, DbType.String);
            dbparams.Add("@Relation", cashless.Relation, DbType.String);
            dbparams.Add("@EmployeeNo", cashless.EmployeeNo, DbType.String);
            dbparams.Add("@SerialNo", cashless.SerialNo, DbType.String);
            // dbparams.Add("@PageNo", cashless.PageNo, DbType.String);
            dbparams.Add("@CategoryOfRoom", cashless.CategoryOfRoom, DbType.String);
            dbparams.Add("@NameOfDoctor", cashless.NameOfDoctor, DbType.String);
            dbparams.Add("@DoctorMobile_no", cashless.Doctor_NO, DbType.String);
            dbparams.Add("@Case_Type", cashless.CaseType, DbType.String);
            dbparams.Add("@PatientNo", cashless.PatientNo, DbType.String);
            dbparams.Add("@IssueDate", cashless.IssueDate, DbType.Date);
            dbparams.Add("@ResidentAddress", cashless.ResidentAddress, DbType.String);
            dbparams.Add("@ProvisionalDiagnosis", cashless.ProvisionalDiagnosis, DbType.String);
            dbparams.Add("@IstendedTreatment", cashless.IstendedTreatment, DbType.String);
            dbparams.Add("@DurationOfStay", cashless.DurationOfStay, DbType.String);
            dbparams.Add("@DateOfAdmission", cashless.DateOfAdmission, DbType.Date);
            //dbparams.Add("@DateOfDischargeOrDeath", cashless.DateOfDischargeOrDeath, DbType.Date);
            //dbparams.Add("@Diagnosis", cashless.Diagnosis, DbType.String);
            //dbparams.Add("@Treatment", cashless.Treatment, DbType.String);
            //dbparams.Add("@SignatureDischargeTime", cashless.SignatureDischargeTime, DbType.String);
            dbparams.Add("@PPONumber", cashless.PPONumber, DbType.String);
            dbparams.Add("@CreatedBy", cashless.CreatedBy, DbType.String);
            dbparams.Add("@StatusCreditId", cashless.StatusCreditId, DbType.Int32);
            dbparams.Add("@Organization", cashless.Organization, DbType.String);  // add 11/02/2025
            // dbparams.Add("@CL_Id", cashless.CL_Id, DbType.Guid);
            // dbparams.Add("@CL_Id", cashless.CL_Id == Guid.Empty ? Guid.NewGuid() : cashless.CL_Id, DbType.Guid);
            dbparams.Add("@ID", outputId, DbType.Int32, direction: ParameterDirection.Output);
            //dbparams.Add("@SerialNo", outputId, DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("SaveCreditletter", dbparams);
            outputId = dbparams.Get<int>("@ID");
            // outputId = dbparams.Get<string>("@SerialNo");
            if (outputId > 0)
            {
                SaveCreditLetterDocuments(cashless.Documents, outputId);
            }
            return outputId;
        }

        //End new changes

        public int UpdateCashlessDelete(CashlessModel cashlessModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", cashlessModel.ClaimId, DbType.Int32);
            dbparams.Add("@IsDelete", cashlessModel.IsDelete, DbType.Boolean);
            dbparams.Add("@ModifiedBy", cashlessModel.ModifideBy, DbType.String);
            return _dapper.Execute("UpdateCashlessIsDelete", dbparams);
        }
        //new changes by nirbhay

        //public int Searchppo(CashlessModel cashlessModel)
        //{
        public CashlessModel Searchppo(int PPONumber)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PPONumber", PPONumber, DbType.Int32);
           // return _dapper.Execute("GetCashlessListByPPONumber", dbparams);
            var PPODetail = _dapper.Get<CashlessModel>("GetCashlessListByPPONumber", dbparams);
            return PPODetail;
        }

        public CashlessModel Loginuser(string PPONumber1)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PPONumber1", PPONumber1, DbType.String);
            // return _dapper.Execute("GetCashlessListByPPONumber", dbparams);
            var PPODetail = _dapper.Get<CashlessModel>("GetCashlessListByloginuser", dbparams);
            return PPODetail;
        }

        public CashlessModel Getcreditletterdata(string PPONumber2)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PPONumber1", PPONumber2, DbType.String);
            // return _dapper.Execute("GetCashlessListByPPONumber", dbparams);
            var PPODetail = _dapper.Get<CashlessModel>("GetCashlessListBymediclaim", dbparams);
            return PPODetail;
        }
        //end new changes



        /// <summary>
        /// Create new non cashless claim
        /// </summary>
        /// <param name="nonCashless"></param>
        /// <returns>Unique Claim Id</returns>
        public int SaveMediclaimNonCashless(NonCashlessModel nonCashless)
        {
            int outputClaimId = 0;
            _createdBy = nonCashless.CreatedBy;

            var dbparams = new DynamicParameters();
            dbparams.Add("@EmployeeNumber", nonCashless.EmployeeNumber, DbType.String);
            dbparams.Add("@PPONumber", nonCashless.PPONumber, DbType.String);
            dbparams.Add("@MedicalSectionPageNumber", nonCashless.MedicalSectionPageNumber, DbType.String);
            dbparams.Add("@MedicalCardHolderName", nonCashless.MedicalCardHolderName, DbType.String);
            dbparams.Add("@Designation", nonCashless.Designation, DbType.String);
            dbparams.Add("@PatientName", nonCashless.PatientName, DbType.String);
            dbparams.Add("@Gender", nonCashless.Gender, DbType.String);
            dbparams.Add("@DateOfBirth", nonCashless.DateOfBirth, DbType.Date);
            dbparams.Add("@Age", nonCashless.Age, DbType.Int32);
            dbparams.Add("@ClaimFor", nonCashless.ClaimFor, DbType.String);
            dbparams.Add("@MobileNumber", nonCashless.MobileNumber, DbType.String);
            dbparams.Add("@CardCategory", nonCashless.CardCategory, DbType.String);
            dbparams.Add("@Address", nonCashless.Address, DbType.String);
            dbparams.Add("@ClaimType", nonCashless.ClaimType, DbType.String);
            dbparams.Add("@MedicalCardPhotoCopy", nonCashless.MedicalCardPhotoCopy, DbType.Boolean);
            dbparams.Add("@PrescriptionDetailPhotoCopy", nonCashless.PrescriptionDetailPhotoCopy, DbType.Boolean);
            dbparams.Add("@OriginalBill", nonCashless.OriginalBill, DbType.Boolean);
            dbparams.Add("@CashMemo", nonCashless.CashMemo, DbType.Boolean);
            dbparams.Add("@ClaimStatusId", nonCashless.ClaimStatusId, DbType.Int32);
            dbparams.Add("@CreatedBy", nonCashless.CreatedBy, DbType.String);
            dbparams.Add("@EmailId", nonCashless.EmailId, DbType.String);
            dbparams.Add("@Organization", nonCashless.Organization, DbType.String);
            dbparams.Add("@MedicalCardNo", nonCashless.MedicalCardNo, DbType.String);
            dbparams.Add("@AccountHolderName", nonCashless.AccountHolderName, DbType.String);
            dbparams.Add("@AccountNumber", nonCashless.AccountNumber, DbType.String);
            dbparams.Add("@BankName", nonCashless.BankName, DbType.String);
            dbparams.Add("@BICCode", nonCashless.BICCode, DbType.String);
            dbparams.Add("@IFSCNumber", nonCashless.IFSCNumber, DbType.String);
            dbparams.Add("@BranchName", nonCashless.BranchName, DbType.String);
            dbparams.Add("@ClaimId", outputClaimId, DbType.Int32, direction: ParameterDirection.Output);

            _dapper.Execute("SaveMediclaimNonCashless", dbparams);
            outputClaimId = dbparams.Get<int>("@ClaimId");
            if (outputClaimId > 0)
            {
                if (nonCashless.ClaimFor.ToLower() == "dependent")
                {
                    nonCashless.Dependent.NonCashlessClaimId = outputClaimId;
                    nonCashless.Dependent.CreatedBy = nonCashless.CreatedBy;
                    SaveNonCashlessDependent(nonCashless.Dependent);
                }
                SaveOPDCNDDetail(nonCashless.OPDCNDList, outputClaimId);
                
                SaveDocuments(nonCashless.Documents, outputClaimId);

            }
            return outputClaimId;
        }
        public void SaveOPDCNDDetail(IList<OPDCNDModel> opdcndList, int nonCashlessclaimId)
        {
            var dbparams = new DynamicParameters();
            foreach (var opdcnd in opdcndList)
            {
                dbparams.Add("@OPDCNDDate", opdcnd.OPDCNDDate, DbType.Date);
                dbparams.Add("@HospitalName", opdcnd.HospitalName, DbType.String);
                dbparams.Add("@MedicineAmount", opdcnd.MedicineAmount, DbType.Decimal);
                dbparams.Add("@InvestigationAmount", opdcnd.InvestigationAmount, DbType.Decimal);
                dbparams.Add("@ConsultationAmount", opdcnd.ConsultationAmount, DbType.Decimal);
                dbparams.Add("@TotalAmount", opdcnd.TotalAmount, DbType.Decimal);
                dbparams.Add("@HospitalizationClaimType", opdcnd.HospitalizationClaimType, DbType.String);
                dbparams.Add("@CreatedBy", _createdBy, DbType.String);
                dbparams.Add("@NonCashlessClaimId", nonCashlessclaimId, DbType.String);
                dbparams.Add("@OtherAmount", opdcnd.OtherAmount, DbType.Decimal);
                _dapper.Execute("SaveOPDCND", dbparams);
            }
        }
   
        public int UpdateNonCashlessDelete(NonCashlessModel nonCashlessModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", nonCashlessModel.ClaimId, DbType.Int32);
            dbparams.Add("@IsDelete", nonCashlessModel.IsDelete, DbType.Boolean);
            dbparams.Add("@ModifiedBy", nonCashlessModel.ModifideBy, DbType.String);
            return _dapper.Execute("UpdateNonCashlessIsDelete", dbparams);
        }
        public void SaveNonCashlessDependent(DependentInformation dependentInformation)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Name", dependentInformation.Name, DbType.String);
            dbparams.Add("@DateOfBirth", dependentInformation.DateOfBirth, DbType.DateTime);
            dbparams.Add("@Age", dependentInformation.Age, DbType.Int32);
            dbparams.Add("@RelationWithRetire", dependentInformation.RelationWithRetire, DbType.String);
            dbparams.Add("@NonCashlessClaimId", dependentInformation.NonCashlessClaimId, DbType.Int32);
            dbparams.Add("@CreatedBy", dependentInformation.CreatedBy, DbType.String);
            //dbparams.Add("@ModifiedBy", ipd.ModifideBy, DbType.String);
            //dbparams.Add("@ModifiedDate", ipd.ModifideDate, DbType.DateTime);
            _dapper.Execute("SaveDependentInformation", dbparams);
        } 

        #region Documents
        public IEnumerable<MasterDocumentModel> GetMasterDocumentList()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<MasterDocumentModel>("SELECT DocumentId, DocumentName FROM dbo.MasterDocumentList WHERE IsDeleted = 0 ORDER BY 2 ASC", dbparams, CommandType.Text);
        }
        public void SaveDocuments(IEnumerable<MediclaimDocumentModel> mediclaimDocuments, int claimId)
        {
            var dbparams = new DynamicParameters();
            int _documentId = 0;
            foreach (var document in mediclaimDocuments)
            {
                dbparams.Add("@DocumentPath", document.DocumentPath, DbType.String);
                dbparams.Add("@ApplicationArea", document.ApplicationArea, DbType.String);
                dbparams.Add("@ApplicationSubArea", document.ApplicationSubArea, DbType.String);
                dbparams.Add("@ReferenceId", claimId, DbType.Int32);
                dbparams.Add("@DocumentFor", document.DocumentFor, DbType.String);
                dbparams.Add("@CreatedBy", document.CreatedBy, DbType.String);
                dbparams.Add("@FileName", document.FileName, DbType.String);
                dbparams.Add("@DocumentId", _documentId, DbType.Int32, direction: ParameterDirection.Output);
                _dapper.Execute("SaveDocument", dbparams);
                _documentId = dbparams.Get<int>("@DocumentId");
            }
        }
        public IEnumerable<MediclaimDocumentModel> GetMediclaimDocumentsByParam(int? documentId, int? referenceId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", documentId, DbType.Int32);
            dbparams.Add("@ReferenceId", referenceId, DbType.Int32);
            return _dapper.GetAll<MediclaimDocumentModel>("GetDocumentsByParam", dbparams);
        }

        // add by rajan 15/01/2025
        public IEnumerable<MasterDocumentModel> GetDocumentListForCreditLetter()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<MasterDocumentModel>("SELECT DocumentId, DocumentName FROM dbo.CreditLetter_DocumentList WHERE IsDeleted = 0 ORDER BY 1 ASC", dbparams, CommandType.Text);
        }

        public void SaveCreditLetterDocuments(IEnumerable<MediclaimDocumentModel> Creditletter_Documents, int ID)
        {
            var dbparams = new DynamicParameters();
            int _documentId = 0;
            foreach (var document in Creditletter_Documents)
            {
                dbparams.Add("@DocumentPath", document.DocumentPath, DbType.String);
                dbparams.Add("@ApplicationArea", document.ApplicationArea, DbType.String);
                dbparams.Add("@ApplicationSubArea", document.ApplicationSubArea, DbType.String);
                dbparams.Add("@ReferenceId", ID, DbType.Int32);
                dbparams.Add("@DocumentFor", document.DocumentFor, DbType.String);
                dbparams.Add("@CreatedBy", document.CreatedBy, DbType.String);
                dbparams.Add("@FileName", document.FileName, DbType.String);
                dbparams.Add("@DocumentId", _documentId, DbType.Int32, direction: ParameterDirection.Output);
                _dapper.Execute("SaveCreditLetter_Document", dbparams);
                _documentId = dbparams.Get<int>("@DocumentId");
            }
        }
        //end

        #endregion
    }
}
