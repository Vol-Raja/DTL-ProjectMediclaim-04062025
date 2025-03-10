using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.GPF;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DTL.Business.GPF.EmployeeInfo
{
    public class EmployeeInfo : IEmployeeInfo
    {
        private readonly IDapperService _dapper;
        public EmployeeInfo(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public IEnumerable<GPFEmployeeInfoModel> Get(string orgCode, string retirementStatus)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@orgCode", orgCode, DbType.String);
            dbparams.Add("@retirementStatus", retirementStatus, DbType.String);

            string sql = $@"
                select * from (
	                select EXTERNAL_CODE 'ExternalCode', EMP_CODE 'EmployeeCode', EMP_NAME 'EmployeeName', dsg_code 'Designation',
		                COMP_CODE 'OrganisationCode', email_id 'Email', CONTACT_NO 'Contact', DOJ 'DOJ', dor 'DOR',
		                case 
			                when GETDATE() >= dor then 'Retiree' 
			                when DATEADD(MONTH, 3, GETDATE()) >= dor then 'Ready for retirement'
			                else 'Ongoing'
		                end 'RetirementStatus'
	                from EMPLOYEE_DEFINATION
                ) t
                where (LOWER(@orgCode) = 'all' or OrganisationCode = @orgCode) and (LOWER(@retirementStatus) = 'all' or RetirementStatus = @retirementStatus)";

            return _dapper.GetAll<GPFEmployeeInfoModel>(sql, dbparams, CommandType.Text);
        }

        public GPFEmployeeDetail GetEmployee(string externalCode)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@externalCode", externalCode, DbType.String);

            string sql = "";

            sql = $@"
                select EMP_NAME 'Name', email_id 'Email', Mobile_no 'Mobile', COMP_CODE 'OrganisationCode',
                    case SEX when 'M' then 'Male' when 'F' then 'Female' else 'Other' end 'Gender'
                from EMPLOYEE_DEFINATION
                where EXTERNAL_CODE = @externalCode
            ";
            GPFEmployeeDetail res = _dapper.GetAll<GPFEmployeeDetail>(sql, dbparams, CommandType.Text).FirstOrDefault();

            sql = $@"
                select EMP_NAME 'EmployeeName', EMP_CODE 'EmployeeCode', F_Name 'FatherName', DOB 'DateOfBirth', DOJ 'DateOfJoining', dol 'DateOfLeaving', 
                    MARITAL_STATUS 'MaritalStatus', '' 'Level', BASIC 'BasicPay', Blood_Grp 'BloodGroup', 
                    dsg_code 'Designation', DEPT_code 'Department', '' 'PayScale', Physical_disable 'PhysicalDisability', 
                    Ident_mark 'IdentificationMarks', dor 'DateOfRetirement', date_of_death 'DateOfDeath', 
                    emp_type 'EmployeeType', CONTACT_NO 'ContactNumber', gpf_no 'GPFNumber'
                from EMPLOYEE_DEFINATION
                where EXTERNAL_CODE = @externalCode
            ";
            var basicDetails = _dapper.GetAll<GPFEmployeeDetail_Basic>(sql, dbparams, CommandType.Text);
            res.BasicDetails = basicDetails.Count() > 0 ? basicDetails.FirstOrDefault() : new GPFEmployeeDetail_Basic();

            sql = $@"
                select bank_code 'BankName', Bank_Branch 'Branch', bank_ac_no 'AccountNo', IFSC_Code 'IFSC', 
                    Aadhar_no 'AadharNo', pan_no 'PanNo', bank_Address 'Address'
                from EMPLOYEE_DEFINATION
                where EXTERNAL_CODE = @externalCode
            ";
            var bankDetails = _dapper.GetAll<GPFEmployeeDetail_Bank>(sql, dbparams, CommandType.Text);
            res.BankDetails = bankDetails.Count() > 0 ? bankDetails.FirstOrDefault() : new GPFEmployeeDetail_Bank();

            sql = $@"
                select CURR_ADD 'Address', CURR_STREET 'Street', CURR_CITY 'City', 
                    CURR_STATE 'State', CURR_PIN 'PinCode', '' 'Nominee'
                from EMPLOYEE_DEFINATION
                where EXTERNAL_CODE = @externalCode
            ";
            var currentAddress = _dapper.GetAll<GPFEmployeeDetail_Address>(sql, dbparams, CommandType.Text);
            res.CurrentAddress = currentAddress.Count() > 0 ? currentAddress.FirstOrDefault() : new GPFEmployeeDetail_Address();

            sql = $@"
                select PERM_ADD 'Address', PERM_STREET 'Street', PERM_CITY 'City',
                    PERM_STATE 'State', PERM_PIN 'PinCode', '' 'Nominee'
                from EMPLOYEE_DEFINATION
                where EXTERNAL_CODE = @externalCode
            ";
            var permanentAddress = _dapper.GetAll<GPFEmployeeDetail_Address>(sql, dbparams, CommandType.Text);
            res.PermanentAddress = permanentAddress.Count() > 0 ? permanentAddress.FirstOrDefault() : new GPFEmployeeDetail_Address();

            sql = $@"
                select b.EMP_CODE 'EmployeeCode', b.EMP_NAME 'EmployeeName', a.member_name 'MemberName', a.relation 'Relationship', a.physicaldisorder 'PhysicalDisorder',
                    a.DOB 'DateOfBirth', a.govt_employee 'GovtEmployee', a.eligible 'Eligible', a.address1 'HNo', a.address2 'Locality', a.city 'City', a.state 'State', a.dist 'Dis', 
                    a.pin 'PinCode', '' 'TransferToNominee', a.bank_code 'BankName', a.bank_ac_no 'AccountNo', a.CONTACT_NO 'ContactNo', a.Mobile_no 'MobileNo', a.DOD 'DateOfDeath', 
                    a.PAN_no 'PanNo', a.remark 'Remarks', a.LIFE_TIME_PENSION 'LifeTimePension', a.REASON_LIFE_TIME 'Reason'
                from family_member a
                left join EMPLOYEE_DEFINATION b on a.external_code = b.EXTERNAL_CODE
                where a.external_code = @externalCode
            ";
            var familyDetails = _dapper.GetAll<GPFEmployeeDetail_Family>(sql, dbparams, CommandType.Text);
            res.FamilyDetails = familyDetails.Count() > 0 ? familyDetails.FirstOrDefault() : new GPFEmployeeDetail_Family();

            sql = $@"
                select g_name 'GuardianName', g_rel 'Relation', g_add1 'HNo', g_add2 'Locality', g_state 'State', g_pin 'PinCode'
                from family_member a
                left join EMPLOYEE_DEFINATION b on a.external_code = b.EXTERNAL_CODE
                where a.external_code = @externalCode
            ";
            var familyGuardianDetails = _dapper.GetAll<GPFEmployeeDetail_Guardian>(sql, dbparams, CommandType.Text);
            res.FamilyGuardianDetails = familyGuardianDetails.Count() > 0 ? familyGuardianDetails.FirstOrDefault() : new GPFEmployeeDetail_Guardian();

            sql = $@"
                select b.EMP_CODE 'EmployeeCode', a.name 'NomineeName', a.SHARE 'Share', a.dob 'DateOfBirth', a.relation 'Relationship', a.rel_other 'RelationshipOther', 
                    a.nominee_type 'NomineeType', a.eligible 'Eligible', '' 'AsPerShare', a.address1 'HNo', a.address2 'Locality', a.city 'City', a.state 'State', a.dist 'Dis', 
                    a.pin 'PinCode', '' 'Reason'
                from nominee_details a
                left join EMPLOYEE_DEFINATION b on a.external_code = b.EXTERNAL_CODE
                where a.external_code = @externalCode
            ";
            var nomineeDetails = _dapper.GetAll<GPFEmployeeDetail_Nominee>(sql, dbparams, CommandType.Text);
            res.NomineeDetails = nomineeDetails.Count() > 0 ? nomineeDetails.FirstOrDefault() : new GPFEmployeeDetail_Nominee();

            sql = $@"
                select g_name 'GuardianName', g_rel 'Relation', g_add1 'HNo', g_add2 'Locality', g_state 'State', g_pin 'PinCode'
                from nominee_details a
                left join EMPLOYEE_DEFINATION b on a.external_code = b.EXTERNAL_CODE
                where a.external_code = @externalCode
            ";
            var nomineeGuardianDetails = _dapper.GetAll<GPFEmployeeDetail_Guardian>(sql, dbparams, CommandType.Text);
            res.NomineeGuardianDetails = nomineeGuardianDetails.Count() > 0 ? nomineeGuardianDetails.FirstOrDefault() : new GPFEmployeeDetail_Guardian();


            return res;
        }

        public bool EmployeeExists(string externalCode)
        {
            var dbparams = new Dictionary<string, object> { { "@externalCode", externalCode } };

            string sql = $@"
                select EMP_CODE 'EmployeeCode'
                from EMPLOYEE_DEFINATION
                where EXTERNAL_CODE = @externalCode
            ";
            var ds = _dapper.GetDataSet(sql, dbparams, CommandType.Text);
            return ds.Tables[0].Rows.Count > 0;

        }

        public IEnumerable<object> EmployeeAutocomplete(string orgCode, string term)
        {
            var dbparams = new Dictionary<string, object> { { "@orgCode", orgCode ?? "all" }, { "@term", term } };

            string sql = $@"
                select distinct top 20 EXTERNAL_CODE 'EmployeeCode', EMP_Code 'EmployeeId', EMP_NAME 'EmployeeName'
                from EMPLOYEE_DEFINATION
                where (@orgCode = 'all' or COMP_CODE = @orgCode)
                    AND (LOWER(EXTERNAL_CODE) like LOWER('%'+@term+'%') or LOWER(EMP_NAME) like LOWER('%'+@term+'%'))
            ";
            var ds = _dapper.GetDataSet(sql, dbparams, CommandType.Text);
            List<object> res = ds.Tables[0].Rows.Cast<DataRow>().Select(q =>
            {
                return (object)new
                {
                    id = q["EmployeeCode"],
                    label = q["EmployeeId"] + " - " + q["EmployeeName"],
                    value = q["EmployeeId"] + " - " + q["EmployeeName"],
                };
            }).ToList();
            return res;
        }

    }
}
