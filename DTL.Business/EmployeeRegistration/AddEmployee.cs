using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.Business.EmployeeRegistration
{
    public class AddEmployee : IAddEmployee
    {
        private readonly IDapperService _dapper;

        public AddEmployee(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }

        public string AddEditEmployeeData(EmployeeProfileModel employeeRegistrationModel, bool isEdit)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Prefix", employeeRegistrationModel.Prefix, DbType.String);
            dbparams.Add("@Id", (isEdit ? employeeRegistrationModel.ID : null), DbType.Guid);
            dbparams.Add("@EmployeeName", employeeRegistrationModel.EmployeeName, DbType.String);
            dbparams.Add("@ProfileImage", employeeRegistrationModel.ProfileImage, DbType.Binary);
            dbparams.Add("@EmployeeId", employeeRegistrationModel.EmployeeId, DbType.Int64);
            dbparams.Add("@DOB", employeeRegistrationModel.DOB, DbType.Date);
            dbparams.Add("@Gender", employeeRegistrationModel.Gender, DbType.String);
            dbparams.Add("@DTLOffice", employeeRegistrationModel.DTLOffice, DbType.String);
            dbparams.Add("@Designation", employeeRegistrationModel.Designation, DbType.String);
            dbparams.Add("@ServiceStartDate", employeeRegistrationModel.ServiceStartDate, DbType.Date);
            dbparams.Add("@ServiceEndDate", employeeRegistrationModel.ServiceEndDate, DbType.Date);
            dbparams.Add("@RAddress", employeeRegistrationModel.ResidentialAddress, DbType.String);
            dbparams.Add("@PAddress", employeeRegistrationModel.PermanentAddress, DbType.String);
            dbparams.Add("@RState", employeeRegistrationModel.RState, DbType.String);
            dbparams.Add("@PState", employeeRegistrationModel.PState, DbType.String);
            dbparams.Add("@RDistrict", employeeRegistrationModel.RDistrict, DbType.String);
            dbparams.Add("@PDistrict", employeeRegistrationModel.PDistrict, DbType.String);
            dbparams.Add("@RZipcode", employeeRegistrationModel.RZipcode, DbType.String);
            dbparams.Add("@PZipcode", employeeRegistrationModel.PZipcode, DbType.String);
            dbparams.Add("@EmailAddress", employeeRegistrationModel.EmailAddress, DbType.String);
            dbparams.Add("@Mobile", employeeRegistrationModel.Mobile, DbType.String);
            dbparams.Add("@Phone", employeeRegistrationModel.Phone, DbType.String);
            dbparams.Add("@ReasonOfRetirement", employeeRegistrationModel.ReasonOfRetirement, DbType.String);
            dbparams.Add("@SpouseName", employeeRegistrationModel.SpouseName, DbType.String);
            //dbparams.Add("@FatherName", employeeRegistrationModel.FatherName, DbType.String);
            if (!isEdit)
                dbparams.Add("@CreatedBy", employeeRegistrationModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifideBy", employeeRegistrationModel.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("sp_AddEditEmployeeProfile", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }

        public async Task<IEnumerable<EmployeeProfileModel>> GetAllEmployeeAsync(string role, bool IsExistingEmployee = false)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@employeeId", null, DbType.Guid);
            dbparams.Add("@role", role, DbType.String);
            dbparams.Add("@IsExistingEmployee", IsExistingEmployee, DbType.Boolean);
            return await _dapper.GetAllAsync<EmployeeProfileModel>("sp_GetEmployee", dbparams);
        }

        public EmployeeProfileModel GetEmployeeById(Guid employeeId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@employeeId", employeeId, DbType.Guid);
            var data = _dapper.Get<EmployeeProfileModel>("sp_GetEmployee", dbparams);
            if (data != null)
                data.ID = employeeId;
            return data;
        }

        public EmployeeModel GetEmployeeDetailsById(Guid employeeId)
        {
            //var dbparams = new DynamicParameters();
            //dbparams.Add("@employeeId", employeeId, DbType.Guid);
            var paras = new Dictionary<string, object>
            {
                { "employeeId", employeeId }
            };
            var employeeDetailsDS = _dapper.GetDataSet("sp_GetEmployeeDetailsById", paras);
            var form5Details = employeeDetailsDS.Tables[0].AsEnumerable().Select(row =>
                               new Form5Model
                               {
                                   ID = row["ID"] == DBNull.Value ? Guid.Empty : row.Field<Guid>("ID"),
                                   Aadhar = row["Aadhar"] == DBNull.Value ? string.Empty : row.Field<string>("Aadhar"),
                                   BankAccountNumber = row["BankAccountNumber"] == DBNull.Value ? string.Empty : row.Field<string>("BankAccountNumber"),
                                   Bank = row["Bank"] == DBNull.Value ? string.Empty : row.Field<string>("Bank"),
                                   BankAccountName = row["BankAccountName"] == DBNull.Value ? string.Empty : row.Field<string>("BankAccountName"),
                                   BankAddress = row["BankAddress"] == DBNull.Value ? string.Empty : row.Field<string>("BankAddress"),
                                   BICCode = row["BICCode"] == DBNull.Value ? string.Empty : row.Field<string>("BICCode"),
                                   EmployeeRegistrationId = row["EmployeeRegistrationId"] == DBNull.Value ? employeeId : row.Field<Guid>("EmployeeRegistrationId"),
                                   IdentificationMark = row["IdentificationMark"] == DBNull.Value ? string.Empty : row.Field<string>("IdentificationMark"),
                                   IFSCCode = row["IFSCCode"] == DBNull.Value ? string.Empty : row.Field<string>("IFSCCode"),
                                   PAN = row["PAN"] == DBNull.Value ? string.Empty : row.Field<string>("PAN"),
                                   SpecimenSignature1 = row["SpecimenSignature"] == DBNull.Value ? null : row.Field<byte[]>("SpecimenSignature"),
                                   SpecimenSignature2 = row["SpecimenSignature2"] == DBNull.Value ? null : row.Field<byte[]>("SpecimenSignature2"),
                                   SpecimenSignature3 = row["SpecimenSignature3"] == DBNull.Value ? null : row.Field<byte[]>("SpecimenSignature3"),
                                   ThumbImpression1 = row["ThumbImpression1"] == DBNull.Value ? null : row.Field<byte[]>("ThumbImpression1"),
                                   ThumbImpression2 = row["ThumbImpression2"] == DBNull.Value ? null : row.Field<byte[]>("ThumbImpression2"),
                                   PanCard = row["PANCard"] == DBNull.Value ? null : row.Field<byte[]>("PANCard"),
                                   AadharCard = row["AadharCard"] == DBNull.Value ? null : row.Field<byte[]>("AadharCard")
                               }).FirstOrDefault();

            var nomineeDetails = employeeDetailsDS.Tables[1].AsEnumerable().Select(row =>
                               new NomineeDetailsModel
                               {
                                   ID = row.Field<Guid>("Id"),
                                   //Arrear as share %
                                   Arrear = row["Arrear"] == DBNull.Value ? 0 : row.Field<int>("Arrear"),
                                   Commutation = row["Commutation"] == DBNull.Value ? 0 : row.Field<int>("Commutation"),
                                   //ContigencyReason = row["ContigencyReason"] == DBNull.Value ? null : row.Field<string>("ContigencyReason"),
                                   DateOfBirth = row["DateOfBirth"] == DBNull.Value ? null : row.Field<DateTime>("DateOfBirth"),
                                   EmployeeRegistrationId = row.Field<Guid>("EmployeeRegistrationId") == Guid.Empty ? employeeId : row.Field<Guid>("EmployeeRegistrationId"),
                                   GuardianName = row["GuardianName"] == DBNull.Value ? null : row.Field<string>("GuardianName"),
                                   GuardianRelation = row["GuardianRelation"] == DBNull.Value ? null : row.Field<string>("GuardianRelation"),
                                   GuardianAddress = row["GuardianAddress"] == DBNull.Value ? null : row.Field<string>("GuardianAddress"),
                                   MemberName = row["MemberName"] == DBNull.Value ? null : row.Field<string>("MemberName"),
                                   MemberOf = row["MemberOf"] == DBNull.Value ? null : row.Field<string>("MemberOf"),
                                   RelationShip = row["RelationShip"] == DBNull.Value ? null : row.Field<string>("RelationShip")
                               }).ToList();

            var pensionSlip = employeeDetailsDS.Tables[2].AsEnumerable().Select(row =>
                               new PensionSlipModel
                               {
                                   ID = row.Field<Guid>("Id"),
                                   ABPLast10Months = row["ABPLast10Months"] == DBNull.Value ? 0 : row.Field<decimal>("ABPLast10Months"),
                                   AdmissiablePension = row["AdmissiablePension"] == DBNull.Value ? 0 : row.Field<decimal>("AdmissiablePension"),
                                   DRPercent = row["DRPercent"] == DBNull.Value ? 0 : row.Field<decimal>("DRPercent"),
                                   DR = row["DR"] == DBNull.Value ? 0 : row.Field<decimal>("DR"),
                                   AdmissiableDate = row["AdmissiableDate"] == DBNull.Value ? null : row.Field<DateTime>("AdmissiableDate"),
                                   AdmissibleForFromDate_Enhanced = row["AdmissibleForFromDate_Enhanced"] == DBNull.Value ? null : row.Field<DateTime>("AdmissibleForFromDate_Enhanced"),
                                   AdmissibleForFromDate_Normal = row["AdmissibleForFromDate_Normal"] == DBNull.Value ? null : row.Field<DateTime>("AdmissibleForFromDate_Normal"),
                                   AdmissibleForToDate_Enhanced = row["AdmissibleForToDate_Enhanced"] == DBNull.Value ? null : row.Field<DateTime>("AdmissibleForToDate_Enhanced"),
                                   AdmissibleForToDate_Normal = row["AdmissibleForToDate_Normal"] == DBNull.Value ? null : row.Field<DateTime>("AdmissibleForToDate_Normal"),
                                   PSCommutation = row["Commutation"] == DBNull.Value ? 0 : row.Field<decimal>("Commutation"),
                                   CommutedPortion = row["CommutedPortion"] == DBNull.Value ? 0 : row.Field<decimal>("CommutedPortion"),
                                   EmolumentsForPension = row["EmolumentsForPension"] == DBNull.Value ? 0 : row.Field<decimal>("EmolumentsForPension"),
                                   EmployeeRegistrationId = row["EmployeeRegistrationId"] == DBNull.Value ? Guid.Empty : row.Field<Guid>("EmployeeRegistrationId") == Guid.Empty ? employeeId : row.Field<Guid>("EmployeeRegistrationId"),
                                   Gratuity = row["Gratuity"] == DBNull.Value ? 0 : row.Field<decimal>("Gratuity"),
                                   GratuityType = row["GratuityType"] == DBNull.Value ? null : row.Field<string>("GratuityType"),
                                   PensionAtNormalRate = row["PensionAtNormalRate"] == DBNull.Value ? 0 : row.Field<decimal>("PensionAtNormalRate"),
                                   PensionEnhancedRate = row["PensionEnhancedRate"] == DBNull.Value ? 0 : row.Field<decimal>("PensionEnhancedRate"),
                                   ServiceStartDate = row["ServiceStartDate"] == DBNull.Value ? null : row.Field<DateTime>("ServiceStartDate"),
                                   ServiceEndDate = row["ServiceEndDate"] == DBNull.Value ? null : row.Field<DateTime>("ServiceEndDate"),
                                   DOB = row["DOB"] == DBNull.Value ? null : row.Field<DateTime>("DOB"),
                                   LeaveEncashment = row["LeaveEncashment"] == DBNull.Value ? null : row.Field<decimal>("LeaveEncashment"),
                                   Taxable = row["Taxable"] == DBNull.Value ? null : row.Field<decimal>("Taxable"),
                                   NonTaxable = row["NonTaxable"] == DBNull.Value ? null : row.Field<decimal>("NonTaxable"),
                                   LeaveEncashmentDays = row["LeaveEncashmentDays"] == DBNull.Value ? null : row.Field<int>("LeaveEncashmentDays"),
                                   LumsumPayableCommutation = row["LumsumCommu"] == DBNull.Value ? null : row.Field<decimal>("LumsumCommu"),
                                   TaxableAmountCommutation = row["TaxableCommu"] == DBNull.Value ? null : row.Field<decimal>("TaxableCommu"),
                                   ServicePeriod = row["ServicePeriod"] == DBNull.Value ? null : row.Field<String>("ServicePeriod"),
                                   TaxableLeaveEncashment = row["TaxableLeaveEncashment"] == DBNull.Value ? 0 : row.Field<decimal>("TaxableLeaveEncashment")
                               }).FirstOrDefault();

            var serviceHistory = employeeDetailsDS.Tables[3].AsEnumerable().Select(row =>
                               new ServiceHistoryModel
                               {
                                   ID = row.Field<Guid>("Id"),
                                   AdditionalServiceDays = row["AdditionalServiceDays"] == DBNull.Value ? 0 : row.Field<int>("AdditionalServiceDays"),
                                   AdditionalServiceMonth = row["AdditionalServiceMonth"] == DBNull.Value ? 0 : row.Field<int>("AdditionalServiceMonth"),
                                   AdditionalServiceYears = row["AdditionalServiceYears"] == DBNull.Value ? 0 : row.Field<int>("AdditionalServiceYears"),
                                   BasicPay = row["BasicPay"] == DBNull.Value ? 0 : row.Field<decimal>("BasicPay"),
                                   //CategoryOfEmployeement = row["CategoryOfEmployeement"] == DBNull.Value ? null : row.Field<string>("CategoryOfEmployeement"),
                                   DTLDepartmentName = row["DTLDepartmentName"] == DBNull.Value ? null : row.Field<string>("DTLDepartmentName"),
                                   HalfYear = row["HalfYear"] == DBNull.Value ? null : row.Field<string>("HalfYear"),
                                   IsMedicalCardRequired = row["IsMedicalCardRequired"] == DBNull.Value ? null : row.Field<string>("IsMedicalCardRequired"),
                                   NPA = row["NPA"] == DBNull.Value ? 0 : row.Field<decimal>("NPA"),
                                   DAPercent = row["DAPercent"] == DBNull.Value ? 0 : row.Field<decimal>("DAPercent"),
                                   DA = row["DA"] == DBNull.Value ? 0 : row.Field<decimal>("DA"),
                                   EmployeeRegistrationId = row["EmployeeRegistrationId"] == DBNull.Value ? Guid.Empty : row.Field<Guid>("EmployeeRegistrationId") == Guid.Empty ? employeeId : row.Field<Guid>("EmployeeRegistrationId"),
                                   Perticulars = row["Perticulars"] == DBNull.Value ? null : row.Field<string>("Perticulars"),
                                   QualifyingServiceDays = row["QualifyingServiceDays"] == DBNull.Value ? 0 : row.Field<int>("QualifyingServiceDays"),
                                   QualifyingServiceMonth = row["QualifyingServiceMonth"] == DBNull.Value ? 0 : row.Field<int>("QualifyingServiceMonth"),
                                   QualifyingServiceYear = row["QualifyingServiceYear"] == DBNull.Value ? 0 : row.Field<int>("QualifyingServiceYear"),
                                   ReasonOfRetirement = row["ReasonOfRetirement"] == DBNull.Value ? null : row.Field<string>("ReasonOfRetirement"),
                                   ServiceDetails = row["ServiceDetails"] == DBNull.Value ? null : row.Field<string>("ServiceDetails"),
                                   ServiceNotCountedDays = row["ServiceNotCountedDays"] == DBNull.Value ? 0 : row.Field<int>("ServiceNotCountedDays"),
                                   ServiceNotCountedMonth = row["ServiceNotCountedMonth"] == DBNull.Value ? 0 : row.Field<int>("ServiceNotCountedMonth"),
                                   ServiceNotCountedYear = row["ServiceNotCountedYear"] == DBNull.Value ? 0 : row.Field<int>("ServiceNotCountedYear"),
                                   TotalServiceDays = row["TotalServiceDays"] == DBNull.Value ? 0 : row.Field<int>("TotalServiceDays"),
                                   TotalServiceMonth = row["TotalServiceMonth"] == DBNull.Value ? 0 : row.Field<int>("TotalServiceMonth"),
                                   TotalServiceYear = row["TotalServiceYear"] == DBNull.Value ? 0 : row.Field<int>("TotalServiceYear"),
                                   PaySlip1 = row["PaySlip1"] == DBNull.Value ? null : row.Field<byte[]>("PaySlip1"),
                                   PaySlip2 = row["PaySlip2"] == DBNull.Value ? null : row.Field<byte[]>("PaySlip2"),
                                   PaySlip3 = row["PaySlip3"] == DBNull.Value ? null : row.Field<byte[]>("PaySlip3"),
                                   Designation = row["Designation"] == DBNull.Value ? null : row.Field<string>("Designation"),
                                   LevelPayment = row["LevelPayment"] == DBNull.Value ? 0 : row.Field<int>("LevelPayment"),
                                   /*
                                   ServiceStartDate = row["ServiceStartDate"] == DBNull.Value ? null : row.Field<DateTime>("ServiceStartDate"),
                                   ServiceEndDate = row["ServiceEndDate"] == DBNull.Value ? null : row.Field<DateTime>("ServiceEndDate"),
                                   DOB = row["DOB"] == DBNull.Value ? null : row.Field<DateTime>("DOB")
                                   */
                               }).FirstOrDefault();


            return new EmployeeModel()
            {
                employeeProfile = GetEmployeeById(employeeId),
                form5 = form5Details ?? new Form5Model(),
                nomineeDetails = nomineeDetails ?? new List<NomineeDetailsModel>(),
                pensionSlip = pensionSlip ?? new PensionSlipModel(),
                serviceHistory = serviceHistory ?? new ServiceHistoryModel()
            };
        }

        public int AddForm5Data(Form5Model form5Model, bool isEdit)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", (isEdit ? form5Model.ID : null), DbType.Guid);
            dbparams.Add("@Aadhar", form5Model.Aadhar, DbType.String);
            dbparams.Add("@PAN", form5Model.PAN, DbType.String);
            dbparams.Add("@BankAccountNumber", form5Model.BankAccountNumber, DbType.String);
            dbparams.Add("@Bank", form5Model.Bank, DbType.String);
            dbparams.Add("@BankAccountName", form5Model.BankAccountName, DbType.String);
            dbparams.Add("@BankAddress", form5Model.BankAddress, DbType.String);
            dbparams.Add("@IFSCCode", form5Model.IFSCCode, DbType.String);
            dbparams.Add("@BICCode", form5Model.BICCode, DbType.String);
            dbparams.Add("@IdentificationMark", form5Model.IdentificationMark, DbType.String);
            if (form5Model.PanCard != null)
                dbparams.Add("@PanCard", form5Model.PanCard, DbType.Binary);
            if (form5Model.AadharCard != null)
                dbparams.Add("@AadharCard", form5Model.AadharCard, DbType.Binary);
            if (form5Model.SpecimenSignature1 != null)
                dbparams.Add("@SpecimenSignature1", form5Model.SpecimenSignature1, DbType.Binary);
            if (form5Model.SpecimenSignature2 != null)
                dbparams.Add("@SpecimenSignature2", form5Model.SpecimenSignature2, DbType.Binary);
            if (form5Model.SpecimenSignature3 != null)
                dbparams.Add("@SpecimenSignature3", form5Model.SpecimenSignature3, DbType.Binary);
            if (form5Model.ThumbImpression1 != null)
                dbparams.Add("@ThumbImpression1", form5Model.ThumbImpression1, DbType.Binary);
            if (form5Model.ThumbImpression2 != null)
                dbparams.Add("@ThumbImpression2", form5Model.ThumbImpression2, DbType.Binary);
            dbparams.Add("@EmployeeRegistrationId", form5Model.EmployeeRegistrationId, DbType.Guid);
            return _dapper.Execute("sp_AddForm5", dbparams);
        }

        public int AddNominee(NomineeDetailsModel nomineeDetailsModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", null, DbType.Guid);
            dbparams.Add("@MemberName", nomineeDetailsModel.MemberName, DbType.String);
            dbparams.Add("@RelationShip", nomineeDetailsModel.RelationShip, DbType.String);
            dbparams.Add("@DateOfBirth", nomineeDetailsModel.DateOfBirth, DbType.Date);
            //dbparams.Add("@ContigencyReason", nomineeDetailsModel.ContigencyReason, DbType.String);
            dbparams.Add("@GuardianName", nomineeDetailsModel.GuardianName, DbType.String);
            dbparams.Add("@GuardianRelation", nomineeDetailsModel.GuardianRelation, DbType.String);
            dbparams.Add("@GuardianAddress", nomineeDetailsModel.GuardianAddress, DbType.String);
            dbparams.Add("@Commutation", nomineeDetailsModel.Commutation, DbType.Int32);
            dbparams.Add("@Arrear", nomineeDetailsModel.Arrear, DbType.Int32);
            //dbparams.Add("@MemberOf", nomineeDetailsModel.MemberOf, DbType.String);
            dbparams.Add("@EmployeeRegistrationId", nomineeDetailsModel.EmployeeRegistrationId, DbType.Guid);
            return _dapper.Execute("sp_AddNomineeDetails", dbparams);
        }

        public string AddPensionSlip(PensionSlipModel pensionSlipModel, bool isEdit)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", (isEdit ? pensionSlipModel.ID : null), DbType.Guid);
            dbparams.Add("@ABPLast10Months", pensionSlipModel.ABPLast10Months, DbType.Int32);
            dbparams.Add("@AdmissiablePension", pensionSlipModel.AdmissiablePension, DbType.Decimal);
            dbparams.Add("@DRPercent", pensionSlipModel.DRPercent, DbType.Decimal);
            dbparams.Add("@DR", pensionSlipModel.DR, DbType.Decimal);
            dbparams.Add("@AdmissiableDate", pensionSlipModel.AdmissiableDate, DbType.DateTime2);
            dbparams.Add("@AdmissibleForFromDate_Enhanced", pensionSlipModel.AdmissibleForFromDate_Enhanced, DbType.DateTime2);
            dbparams.Add("@AdmissibleForFromDate_Normal", pensionSlipModel.AdmissibleForFromDate_Normal, DbType.DateTime2);
            dbparams.Add("@AdmissibleForToDate_Enhanced", pensionSlipModel.AdmissibleForToDate_Enhanced, DbType.DateTime2);
            dbparams.Add("@AdmissibleForToDate_Normal", pensionSlipModel.AdmissibleForToDate_Normal, DbType.DateTime2);
            dbparams.Add("@Commutation", pensionSlipModel.PSCommutation, DbType.Int32);
            dbparams.Add("@CommutedPortion", pensionSlipModel.CommutedPortion, DbType.Int32);
            //dbparams.Add("@CreatedBy", pensionSlipModel.CreatedBy, DbType.String);
            //dbparams.Add("@CreatedDate", pensionSlipModel.CreatedDate, DbType.DateTime2);
            dbparams.Add("@EmolumentsForPension", pensionSlipModel.EmolumentsForPension, DbType.Int32);
            dbparams.Add("@Gratuity", pensionSlipModel.Gratuity, DbType.Int32);
            dbparams.Add("@GratuityType", pensionSlipModel.GratuityType, DbType.String);
            //dbparams.Add("@flag", pensionSlipModel.ModifideBy, DbType.Int32);
            //dbparams.Add("@flag", pensionSlipModel.ModifideDate, DbType.Int32);
            dbparams.Add("@PensionAtNormalRate", pensionSlipModel.PensionAtNormalRate, DbType.Int32);
            dbparams.Add("@PensionEnhancedRate", pensionSlipModel.PensionEnhancedRate, DbType.Int32);
            dbparams.Add("@EmployeeRegistrationId", pensionSlipModel.EmployeeRegistrationId, DbType.Guid);
            dbparams.Add("@LeaveEncashment", pensionSlipModel.LeaveEncashment, DbType.Decimal);
            dbparams.Add("@Taxable", pensionSlipModel.Taxable, DbType.Decimal);
            dbparams.Add("@NonTaxable", pensionSlipModel.NonTaxable, DbType.Decimal);
            dbparams.Add("@LeaveEncashmentDays", pensionSlipModel.LeaveEncashmentDays, DbType.Int32);
            dbparams.Add("@LumsumCommu", pensionSlipModel.LumsumPayableCommutation, DbType.Decimal);
            dbparams.Add("@TaxableCommu", pensionSlipModel.TaxableAmountCommutation, DbType.Decimal);
            dbparams.Add("@ServicePeriod", pensionSlipModel.ServicePeriod, DbType.String);
            dbparams.Add("@TaxableLeaveEncashment", pensionSlipModel.TaxableLeaveEncashment, DbType.Decimal);
            if (!isEdit)
                dbparams.Add("@CreatedBy", pensionSlipModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifideBy", pensionSlipModel.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("sp_AddPensionSlip", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }

        public string AddServiceHistory(ServiceHistoryModel serviceHistoryModel, bool isEdit)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", (isEdit ? serviceHistoryModel.ID : null), DbType.Guid);
            dbparams.Add("@DTLDepartmentName", serviceHistoryModel.DTLDepartmentName, DbType.String);
            //dbparams.Add("@CategoryOfEmployeement", serviceHistoryModel.CategoryOfEmployeement, DbType.String);
            dbparams.Add("@ReasonOfRetirement", serviceHistoryModel.ReasonOfRetirement, DbType.String);
            dbparams.Add("@IsMedicalCardRequired", serviceHistoryModel.IsMedicalCardRequired, DbType.String);
            dbparams.Add("@TotalServiceYear", serviceHistoryModel.TotalServiceYear, DbType.Int32);
            dbparams.Add("@TotalServiceMonth", serviceHistoryModel.TotalServiceMonth, DbType.Int32);
            dbparams.Add("@TotalServiceDays", serviceHistoryModel.TotalServiceDays, DbType.Int32);
            dbparams.Add("@AdditionalServiceYears", serviceHistoryModel.AdditionalServiceYears, DbType.Int32);
            dbparams.Add("@AdditionalServiceMonth", serviceHistoryModel.AdditionalServiceMonth, DbType.Int32);
            dbparams.Add("@AdditionalServiceDays", serviceHistoryModel.AdditionalServiceDays, DbType.Int32);
            dbparams.Add("@ServiceNotCountedYear", serviceHistoryModel.ServiceNotCountedYear, DbType.Int32);
            dbparams.Add("@ServiceNotCountedMonth", serviceHistoryModel.ServiceNotCountedMonth, DbType.Int32);
            dbparams.Add("@ServiceNotCountedDays", serviceHistoryModel.ServiceNotCountedDays, DbType.Int32);
            dbparams.Add("@QualifyingServiceYear", serviceHistoryModel.QualifyingServiceYear, DbType.Int32);
            dbparams.Add("@QualifyingServiceMonth", serviceHistoryModel.QualifyingServiceMonth, DbType.Int32);
            dbparams.Add("@QualifyingServiceDays", serviceHistoryModel.QualifyingServiceDays, DbType.Int32);
            dbparams.Add("@ServiceDetails", serviceHistoryModel.ServiceDetails, DbType.String);
            dbparams.Add("@Perticulars", serviceHistoryModel.Perticulars, DbType.String);
            dbparams.Add("@HalfYear", serviceHistoryModel.HalfYear, DbType.String);
            dbparams.Add("@BasicPay", serviceHistoryModel.BasicPay, DbType.Int64);
            dbparams.Add("@DAPercent", serviceHistoryModel.DAPercent, DbType.Decimal);
            dbparams.Add("@DA", serviceHistoryModel.DA, DbType.Decimal);
            dbparams.Add("@NPA", serviceHistoryModel.NPA, DbType.Int64);
            dbparams.Add("@Designation", serviceHistoryModel.Designation, DbType.String);
            dbparams.Add("@LevelPayment", serviceHistoryModel.LevelPayment, DbType.Int64);
            if (serviceHistoryModel.PaySlip1 != null)
                dbparams.Add("@PaySlip1", serviceHistoryModel.PaySlip1, DbType.Binary);
            if (serviceHistoryModel.PaySlip1 != null)
                dbparams.Add("@PaySlip2", serviceHistoryModel.PaySlip2, DbType.Binary);
            if (serviceHistoryModel.PaySlip1 != null)
                dbparams.Add("@PaySlip3", serviceHistoryModel.PaySlip3, DbType.Binary);
            dbparams.Add("@EmployeeRegistrationId", serviceHistoryModel.EmployeeRegistrationId, DbType.Guid);
            if (!isEdit)
                dbparams.Add("@CreatedBy", serviceHistoryModel.CreatedBy, DbType.String);
            else
                dbparams.Add("@ModifideBy", serviceHistoryModel.ModifideBy, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("sp_ServiceHistory", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }

        public int RemoveNominee(Guid employeeId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@EmployeeRegistrationId", employeeId, DbType.Guid);
            return _dapper.Execute("sp_DeleteNonimees", dbparams);
        }

        public int UpatePesionAppStatus(Guid empId, int status)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@EmployeeRegistrationId", empId, DbType.Guid);
            dbparams.Add("@status", status, DbType.Int32);
            return _dapper.Execute("sp_UpdateEmployeePensionStatus", dbparams);
        }
        public IEnumerable<EmployeeProfileModel> GetEmployeeByDTLOffice(String Dtloffice, String SearchType, String SearchVal)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DTLOffice", Dtloffice, DbType.String);
            var data = _dapper.GetAll<EmployeeProfileModel>("sp_GetEmployeeDetailsByDTLOffice", dbparams);
            // List<EmployeeProfileModel> lst = data.ToList();
            return data.AsEnumerable();
        }
        /*public EmployeeProfileModel GetListOfSearchEmployeeFromDtlOffice(string EmpId)
         {

         }*/

        public int UpdateRejectionStatusRemarks(Guid empId, string Role, int status, string Remarks)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@EmployeeRegistrationId", empId, DbType.Guid);
            dbparams.Add("@Role", Role, DbType.String);
            dbparams.Add("@Status", status, DbType.Int32);
            dbparams.Add("@Remarks", Remarks, DbType.String);
            return _dapper.Execute("sp_UpdateEmployeeRegStatusByHRAdmin", dbparams);
        }


        public int DeleteEmp(Guid employeeId, string deleteReason)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@EmployeeRegistrationId", employeeId, DbType.Guid);
            dbparams.Add("@DeleteReason", deleteReason, DbType.String);
            return _dapper.Execute("sp_DeleteEmp", dbparams);
        }

        public async Task<IEnumerable<EmployeeProfileModel>> GetAllArchiveEmployeeAsync(Guid employeeId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@employeeId", (employeeId == Guid.Empty ? null : employeeId), DbType.Guid);
            // dbparams.Add("@role", role, DbType.String);
            return await _dapper.GetAllAsync<EmployeeProfileModel>("sp_GetArchiveEmployee", dbparams);
        }

        public EmployeeProfileModel GetArchiveEmployeeById(Guid employeeId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@employeeId", employeeId, DbType.Guid);
            var data = _dapper.Get<EmployeeProfileModel>("sp_GetArchiveEmployee", dbparams);
            if (data != null)
                data.ID = employeeId;
            return data;
        }

        public int UpatePesionOrderID(Guid empId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@EmployeeRegistrationId", empId, DbType.Guid);
            return _dapper.Execute("sp_UpatePesionOrderID", dbparams);
        }
    }
}
