using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using DTL.Model.Models.Mediclaim;
using DTL.Model.Models.Mediclaim.Hospitalization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Mediclaim.Modify
{
    public class Modify : IModify
    {
        private readonly IDapperService _dapper;
        public Modify(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public CashlessModel GetCashlessByParam(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            var cashlessDetail = _dapper.Get<CashlessModel>("GetCashlessListByClaimId", dbparams);
            cashlessDetail.Documents = GetMediclaimDocumentsByParam(null, claimId, "cashless");
            return cashlessDetail;
        }
        public int UpdateMediclaimCashless(CashlessModel cashlessModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@NameOfHospital", cashlessModel.NameOfHospital, DbType.String);
            dbparams.Add("@HospitalPhoneNumber", cashlessModel.HospitalPhoneNumber, DbType.String);
            dbparams.Add("@HospitalAddress", cashlessModel.HospitalAddress, DbType.String);
            dbparams.Add("@HospitalId", cashlessModel.HospitalId, DbType.String);
            dbparams.Add("@EmailId", cashlessModel.EmailId, DbType.String);
            dbparams.Add("@NameOfPatient", cashlessModel.NameOfPatient, DbType.String);
            dbparams.Add("@PatientEmailId", cashlessModel.PatientEmailId, DbType.String);
            dbparams.Add("@Gender", cashlessModel.Gender, DbType.String);
            dbparams.Add("@PatientPhoneNumber", cashlessModel.PatientPhoneNumber, DbType.String);
            dbparams.Add("@PatientAddress", cashlessModel.PatientAddress, DbType.String);
            dbparams.Add("@DateOfBirth", cashlessModel.DateOfBirth, DbType.Date);
            dbparams.Add("@Age", cashlessModel.Age, DbType.String);
            dbparams.Add("@MedicalSectionPageNumber", cashlessModel.MedicalSectionPageNumber, DbType.String);
            dbparams.Add("@NameOfCardHolder", cashlessModel.NameOfCardHolder, DbType.String);
            dbparams.Add("@MedicalCardNumber", cashlessModel.MedicalCardNumber, DbType.String);
            dbparams.Add("@AdmissionNumber", cashlessModel.AdmissionNumber, DbType.String);
            dbparams.Add("@CardCategory", cashlessModel.CardCategory, DbType.String);
            dbparams.Add("@CaseType", cashlessModel.CaseType, DbType.String);
            dbparams.Add("@TypeOfTreatment", cashlessModel.TypeOfTreatment, DbType.String);
            dbparams.Add("@Amount", cashlessModel.Amount, DbType.Decimal);
            dbparams.Add("@DateOfAdmission", cashlessModel.DateOfAdmission, DbType.Date);
            dbparams.Add("@RelationWithRetire", cashlessModel.RelationWithRetire, DbType.String);
            dbparams.Add("@DependantDOB", cashlessModel.DependantDOB, DbType.Date);
            dbparams.Add("@DependantAge", cashlessModel.DependantAge, DbType.Int32);
            dbparams.Add("@DateOfDischargeOrDeath", cashlessModel.DateOfDischargeOrDeath, DbType.Date);            
            dbparams.Add("@ClaimStatusId", cashlessModel.ClaimStatusId, DbType.Int32);
            dbparams.Add("@RejectReason", cashlessModel.RejectReason, DbType.String);
            dbparams.Add("@ModifiedBy", cashlessModel.ModifideBy, DbType.String);
            dbparams.Add("@ClaimId", cashlessModel.ClaimId, DbType.Int32);
            dbparams.Add("@AccountHolderName", cashlessModel.AccountHolderName, DbType.String);
            dbparams.Add("@AccountNumber", cashlessModel.AccountNumber, DbType.String);
            dbparams.Add("@BankName", cashlessModel.BankName, DbType.String);
            dbparams.Add("@BICCode", cashlessModel.BICCode, DbType.String);
            dbparams.Add("@IFSCNumber", cashlessModel.IFSCNumber, DbType.String);
            dbparams.Add("@BranchName", cashlessModel.BranchName, DbType.String);
            dbparams.Add("@PPONumber", cashlessModel.PPONumber, DbType.String);
            dbparams.Add("@Organization", cashlessModel.Organization, DbType.String);
            dbparams.Add("@Department", cashlessModel.Department, DbType.String);
            dbparams.Add("@Designation", cashlessModel.Designation, DbType.String);
            dbparams.Add("@DateOfRetirement", cashlessModel.DateOfRetirement, DbType.Date);
            var returnVal = _dapper.Execute("UpdateMediclaimCashless", dbparams);
      
            foreach (var item in cashlessModel.Documents)
            {
                if (item.DocumentId > 0) {
                    continue;
                }
                SaveDocuments(cashlessModel.Documents, cashlessModel.ClaimId);
            }

            return returnVal;

        }

        public NonCashlessModel GetNonCashlessByParam(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            var nonCashlessDetail = _dapper.Get<NonCashlessModel>("GetNonCashlessClaimByParam", dbparams);
            nonCashlessDetail.OPDCNDList = GetOPDCNDList(claimId).AsList();
            if (nonCashlessDetail.ClaimFor.ToLower() == "dependent") {
                nonCashlessDetail.Dependent = GetDependentInformationByParam(nonCashlessDetail.ClaimId);
            }
            nonCashlessDetail.Documents = GetMediclaimDocumentsByParam(null, claimId,"noncashless");
            return nonCashlessDetail;
        }

        private IEnumerable<DispensaryModel> GetDispensaries(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            return _dapper.GetAll<DispensaryModel>("GetDispensaryByClaimId", dbparams);
        }

        private IEnumerable<IPDModel> GetIPDList(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            return _dapper.GetAll<IPDModel>("GetIPDByClaimId", dbparams);
        }

        private IEnumerable<OPDCNDModel> GetOPDCNDList(int claimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ClaimId", claimId, DbType.Int32);
            return _dapper.GetAll<OPDCNDModel>("GetOPDCNDByClaimId", dbparams);
        }

        public int UpdateNonCashless(NonCashlessModel nonCashless)
        {

            var dbparams = new DynamicParameters();

            dbparams.Add("@ClaimId", nonCashless.ClaimId, DbType.Int32);
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
            dbparams.Add("@ModifiedBy", nonCashless.ModifideBy, DbType.String);

            dbparams.Add("@EmailId", nonCashless.EmailId, DbType.String);
            dbparams.Add("@Organization", nonCashless.Organization, DbType.String);
            dbparams.Add("@MedicalCardNo", nonCashless.MedicalCardNo, DbType.String);
            dbparams.Add("@AccountHolderName", nonCashless.AccountHolderName, DbType.String);
            dbparams.Add("@AccountNumber", nonCashless.AccountNumber, DbType.String);
            dbparams.Add("@BankName", nonCashless.BankName, DbType.String);
            dbparams.Add("@BICCode", nonCashless.BICCode, DbType.String);
            dbparams.Add("@IFSCNumber", nonCashless.IFSCNumber, DbType.String);
            dbparams.Add("@BranchName", nonCashless.BranchName, DbType.String);

            var _count = _dapper.Execute("UpdateMediclaimNonCashless", dbparams);

            if (_count > 0)
            {
                if (nonCashless.ClaimFor.ToLower() == "dependent")
                {
                    if (CheckIsDependentAlreadyThere(nonCashless.ClaimId))
                    {
                        nonCashless.Dependent.NonCashlessClaimId = nonCashless.ClaimId;
                        nonCashless.Dependent.ModifiedBy = nonCashless.ModifideBy;
                        UpdateNonCashlessDependent(nonCashless.Dependent);
                    }
                    else
                    {
                        nonCashless.Dependent.NonCashlessClaimId = nonCashless.ClaimId;
                        nonCashless.Dependent.CreatedBy = nonCashless.ModifideBy;
                        SaveNonCashlessDependent(nonCashless.Dependent);
                    }
                }
                UpdateOPDCNDDetail(nonCashless.OPDCNDList, nonCashless.ModifideBy);
                if (nonCashless.Documents.Count() > 0) { SaveDocuments(nonCashless.Documents, nonCashless.ClaimId); }
            }

            return _count;
        }
        public void UpdateOPDCNDDetail(IList<OPDCNDModel> opdcndList, string modifiedBy)
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
                dbparams.Add("@NonCashlessClaimId", opdcnd.NonCashlessClaimId, DbType.Int32);
                dbparams.Add("@OtherAmount", opdcnd.OtherAmount, DbType.Decimal);

                if (opdcnd.OPDCNDId > 0)
                {
                    dbparams.Add("@OPDCNDId", opdcnd.OPDCNDId, DbType.Int32);
                    dbparams.Add("@ModifiedBy", modifiedBy, DbType.String);
                    _dapper.Execute("UpdateOPDCND", dbparams);
                }
                else {
                    dbparams.Add("@CreatedBy", modifiedBy, DbType.String);
                    _dapper.Execute("SaveOPDCND", dbparams);
                }

              
            }
        }


        public bool CheckIsDependentAlreadyThere(int claimId)
        {
            var dbparams = new DynamicParameters();
            var Id = _dapper.Get<int>($"SELECT Id FROM dbo.DependentInformation WHERE NonCashlessClaimId = {claimId}", dbparams, CommandType.Text);
            return Id > 0;
        }

        private void SaveNonCashlessDependent(DependentInformation dependentInformation)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Name", dependentInformation.Name, DbType.String);
            dbparams.Add("@DateOfBirth",dependentInformation.DateOfBirth, DbType.DateTime);
            dbparams.Add("@Age", dependentInformation.Age, DbType.Int32);
            dbparams.Add("@RelationWithRetire", dependentInformation.RelationWithRetire, DbType.String);
            dbparams.Add("@NonCashlessClaimId", dependentInformation.NonCashlessClaimId, DbType.Int32);
            dbparams.Add("@CreatedBy", dependentInformation.CreatedBy, DbType.String);
            //dbparams.Add("@ModifiedBy", ipd.ModifideBy, DbType.String);
            //dbparams.Add("@ModifiedDate", ipd.ModifideDate, DbType.DateTime);
            _dapper.Execute("SaveDependentInformation", dbparams);
        }

        public void UpdateNonCashlessDependent(DependentInformation dependentInformation)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", dependentInformation.Id, DbType.Int32);
            dbparams.Add("@Name", dependentInformation.Name, DbType.String);
            dbparams.Add("@DateOfBirth", dependentInformation.DateOfBirth, DbType.DateTime);
            dbparams.Add("@Age", dependentInformation.Age, DbType.Int32);
            dbparams.Add("@RelationWithRetire", dependentInformation.RelationWithRetire, DbType.String);
            dbparams.Add("@ModifiedBy", dependentInformation.ModifiedBy, DbType.String);
            //dbparams.Add("@ModifiedBy", ipd.ModifideBy, DbType.String);
            //dbparams.Add("@ModifiedDate", ipd.ModifideDate, DbType.DateTime);
            _dapper.Execute("UpdateDependentInformation", dbparams);
        }



        public DependentInformation GetDependentInformationByParam(int nonCashlessClaimId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@NonCashlessClaimId", nonCashlessClaimId, DbType.Int32);
            return  _dapper.Get<DependentInformation>("GetDependentInformationByParam", dbparams);
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
                if (document.DocumentId > 0) { continue; }
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
        public IEnumerable<MediclaimDocumentModel> GetMediclaimDocumentsByParam(int? documentId, int? referenceId,string applicationSubArea, bool isActive = true)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DocumentId", documentId, DbType.Int32);
            dbparams.Add("@ReferenceId", referenceId, DbType.Int32);
            dbparams.Add("@ApplicationSubArea", applicationSubArea, DbType.String);
            return _dapper.GetAll<MediclaimDocumentModel>("GetDocumentsByParam", dbparams);
        }

        public void DeleteDocuments(int documentId)
        {
            var dbparams = new DynamicParameters();
            string query = $"Update dbo.Document Set Active = 0 Where DocumentId = {documentId}";
            _dapper.Execute(query, dbparams, CommandType.Text);
        }
        #endregion

    }
}
