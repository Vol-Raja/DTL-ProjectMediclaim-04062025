using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.GPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.EmployeeDashboard.Data
{
    public class GPF :IGPF
    {
        private readonly IDapperService _dapper;
        public GPF(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }

        public IEnumerable<GPFEmployeeInfoModel> Get(string ppo_no, string orgCode, string retirementStatus)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ppono", ppo_no, DbType.String); //add by nirbhay
            dbparams.Add("@orgCode", orgCode, DbType.String);
            dbparams.Add("@retirementStatus", retirementStatus, DbType.String);

            string sql = $@"
                select * from (
                         select p.ppo_no, e.EXTERNAL_CODE 'ExternalCode', e.EMP_CODE 'EmployeeCode', e.EMP_NAME 'EmployeeName', e.dsg_code 'Designation',
		                 e.COMP_CODE 'OrganisationCode', e.email_id 'Email', e.CONTACT_NO 'Contact', e.DOJ 'DOJ', e.dor 'DOR',
		                case 
			                when GETDATE() >= dor then 'Retiree' 
			                when DATEADD(MONTH, 3, GETDATE()) >= dor then 'Ready for retirement'
			                else 'Ongoing'
		                end RetirementStatus from EMPLOYEE_DEFINATION as E  
                        join Pension_Order as p  
                        on p.external_code=E.EXTERNAL_CODE  and p.ppo_no=@ppono 

                 ) t
                   where (LOWER(@orgCode) = 'all' or OrganisationCode = @orgCode) and (LOWER(@retirementStatus) = 'all' or RetirementStatus = @retirementStatus)";

            return _dapper.GetAll<GPFEmployeeInfoModel>(sql, dbparams, CommandType.Text);
        }

        public GPFEmployeeDetail GetEmployee(string externalCode, string ppo)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@externalCode", externalCode, DbType.String);
            dbparams.Add("@ppono", ppo, DbType.String);

            string sql = "";

            sql = $@"
               

                    select E.EMP_NAME 'Name', E.email_id 'Email', E.Mobile_no 'Mobile', E.COMP_CODE 'OrganisationCode',
                    case E.SEX when 'M' then 'Male' when 'F' then 'Female' else 'Other' end 'Gender'
                from EMPLOYEE_DEFINATION as E
                        join Pension_Order as p  
                        on p.external_code=E.EXTERNAL_CODE  and p.ppo_no=@ppono
              
            ";
            GPFEmployeeDetail res = _dapper.GetAll<GPFEmployeeDetail>(sql, dbparams, CommandType.Text).FirstOrDefault();

            sql = $@"
              
                    select E.EMP_NAME 'EmployeeName',E.EMP_CODE 'EmployeeCode', E.F_Name 'FatherName', E.DOB 'DateOfBirth', E.DOJ 'DateOfJoining', E.dol 'DateOfLeaving', 
                    E.MARITAL_STATUS 'MaritalStatus', '' 'Level', E.BASIC 'BasicPay', E.Blood_Grp 'BloodGroup', 
                    E.dsg_code 'Designation', E.DEPT_code 'Department', '' 'PayScale', E.Physical_disable 'PhysicalDisability', 
                    E.Ident_mark 'IdentificationMarks', E.dor 'DateOfRetirement', E.date_of_death 'DateOfDeath', 
                    E.emp_type 'EmployeeType', E.CONTACT_NO 'ContactNumber', E.gpf_no 'GPFNumber'
                from EMPLOYEE_DEFINATION as E
				  join Pension_Order as p  
                        on p.external_code=E.EXTERNAL_CODE  and p.ppo_no=@ppono
            ";
            var basicDetails = _dapper.GetAll<GPFEmployeeDetail_Basic>(sql, dbparams, CommandType.Text);
            res.BasicDetails = basicDetails.Count() > 0 ? basicDetails.FirstOrDefault() : new GPFEmployeeDetail_Basic();

            sql = $@"
                  select E.bank_code 'BankName', E.Bank_Branch 'Branch', E.bank_ac_no 'AccountNo', E.IFSC_Code 'IFSC', 
                    E.Aadhar_no 'AadharNo', E.pan_no 'PanNo', E.bank_Address 'Address'
                from EMPLOYEE_DEFINATION  as E
				join Pension_Order as p 
                     on p.external_code=E.EXTERNAL_CODE  and p.ppo_no=@ppono
            ";
            var bankDetails = _dapper.GetAll<GPFEmployeeDetail_Bank>(sql, dbparams, CommandType.Text);
            res.BankDetails = bankDetails.Count() > 0 ? bankDetails.FirstOrDefault() : new GPFEmployeeDetail_Bank();

            sql = $@"
               select CURR_ADD 'Address', CURR_STREET 'Street', CURR_CITY 'City', 
                    CURR_STATE 'State', CURR_PIN 'PinCode', '' 'Nominee'
                from EMPLOYEE_DEFINATION as E
				join Pension_Order as p 
                     on p.external_code=E.EXTERNAL_CODE  and p.ppo_no=@ppono
       
            ";
            var currentAddress = _dapper.GetAll<GPFEmployeeDetail_Address>(sql, dbparams, CommandType.Text);
            res.CurrentAddress = currentAddress.Count() > 0 ? currentAddress.FirstOrDefault() : new GPFEmployeeDetail_Address();

            sql = $@"
                select PERM_ADD 'Address', PERM_STREET 'Street', PERM_CITY 'City',
                    PERM_STATE 'State', PERM_PIN 'PinCode', '' 'Nominee'
                from EMPLOYEE_DEFINATION as E
				join Pension_Order as p 
                     on p.external_code=E.EXTERNAL_CODE  and p.ppo_no=@ppono
             
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
				join Pension_Order as p 
                     on p.external_code=b.EXTERNAL_CODE  and p.ppo_no=@ppono
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

        public IEnumerable<GPFEmployeeInfoModel> GetEmployeeGPF()
        {
            var dbparams = new DynamicParameters();
            return _dapper.GetAll<GPFEmployeeInfoModel>("", dbparams);
        }
    }
}
