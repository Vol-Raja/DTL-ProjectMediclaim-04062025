using Dapper;
using DocumentFormat.OpenXml.EMMA;
using DTL.Business.Dapper;
using DTL.Business.GPF.EmployeeInfo;
using DTL.Model.Models.GPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DTL.Business.GPF.EDLIS
{
    public class EDLIS : IEDLIS
    {
        private readonly IDapperService _dapper;
        private readonly IEmployeeInfo _empInfo;

        public EDLIS(IDapperService dapper, IEmployeeInfo empInfo)
        {
            _dapper = dapper;
            _empInfo = empInfo;
        }

        public GPF_EDLIS SaveEDLISApplication(GPF_EDLIS obj)
        {
            obj.ApplicationNumber = GenerateEDLISApplicationNumber(obj.Organization);
            obj.CreatedOn = DateTime.Now;
            obj.EDLISStatus = "Pending";

            var dParams = _dapper.GetDynamicParams(obj, "Id", new List<string> { "Nominees", "Family", "BankDetails", "Documents", "Employee" });
            var id = _dapper.Execute("Save_GPF_EDLIS", dParams);

            if (id > 0)
            {
                obj.BankDetails.ApplicationNumber = obj.ApplicationNumber;
                dParams = _dapper.GetDynamicParams(obj.BankDetails, "Id", new List<string> { });
                var i = _dapper.Execute("Save_GPF_EDLIS_BankDetails", dParams);
                foreach (var n in obj.Nominees)
                {
                    n.ApplicationNumber = obj.ApplicationNumber;
                    dParams = _dapper.GetDynamicParams(n, "Id", new List<string> { });
                    i = _dapper.Execute("Save_GPF_EDLIS_Nominee", dParams);
                }
                foreach (var f in obj.Family)
                {
                    f.ApplicationNumber = obj.ApplicationNumber;
                    dParams = _dapper.GetDynamicParams(f, "Id", new List<string> { });
                    i = _dapper.Execute("Save_GPF_EDLIS_Family", dParams);
                }
            }

            return obj;
        }
        public int SaveEDLISApplicationDoc(GPF_EDLIS_Documents obj)
        {
            var dParams = _dapper.GetDynamicParams(obj, "Id", new List<string> { });
            var id = _dapper.Execute("Save_GPF_EDLIS_Documents", dParams);
            return id;
        }

        public IEnumerable<GPF_EDLIS_View> GetEDLISApplications(string status, string applicationNumber = "", string employeeCode = "", string organization = "")
        {
            var dParams = new DynamicParameters();
            dParams.Add("@status", status.ToLower().Trim(), DbType.String);
            dParams.Add("@applicationNumber", (applicationNumber ?? "").ToLower().Trim(), DbType.String);
            dParams.Add("@employeeCode", (employeeCode ?? "").ToLower().Trim(), DbType.String);
            dParams.Add("@organization", (organization ?? "").ToLower().Trim(), DbType.String);
            var sql = $@"
                select a.*, b.EMP_NAME 'EmployeeName', EMP_CODE 'EmployeeCode', b.gpf_no 'GPFAccountNumber'
                from GPF_EDLIS a
                left join EMPLOYEE_DEFINATION b on a.EmployeeExternalCode = b.EXTERNAL_CODE
                where (@status = 'all' or LOWER(EDLISStatus) = @status) 
                and (@applicationNumber = '' or LOWER(ApplicationNumber) like '%'+@applicationNumber+'%')
                and (@employeeCode = '' or LOWER(a.EmployeeExternalCode) like '%'+@employeeCode+'%')
                and (@employeeCode = '' or LOWER(b.EMP_NAME) like '%'+@employeeCode+'%')
                and (@organization = '' or @organization = 'all' or LOWER(b.COMP_CODE) = @organization)
                order by a.CreatedOn desc
            ";
            var ds = _dapper.GetAll<GPF_EDLIS_View>(sql, dParams, CommandType.Text);
            return ds;
        }

        public IEnumerable<GPF_EDLIS_View_Disburs> GetEDLISApplicationsDisburs()
        {
            var dParams = new DynamicParameters();
            var sql = $@"
                select a.*, b.EMP_NAME 'EmployeeName', EMP_CODE 'EmployeeCode', b.gpf_no 'GPFAccountNumber'
                    , c.BankName, c.BranchName, c.AccountNumber, c.IFSC, c.BIS
                from GPF_EDLIS a
                left join EMPLOYEE_DEFINATION b on a.EmployeeExternalCode = b.EXTERNAL_CODE
                left join GPF_EDLIS_BankDetails c on a.ApplicationNumber = c.ApplicationNumber
                where EDLISStatus in ('Pending with Disbursement', 'Completed')
                order by a.CreatedOn desc
            ";
            var ds = _dapper.GetAll<GPF_EDLIS_View_Disburs>(sql, dParams, CommandType.Text);
            return ds;
        }

        public GPF_EDLIS GetEDLISApplication(string applicationNumber)
        {
            GPF_EDLIS res;
            var dParams = new DynamicParameters();
            dParams.Add("@appNo", applicationNumber.ToLower().Trim(), DbType.String);

            var sql = $@"select top 1 * from GPF_EDLIS where LOWER(ApplicationNumber) = @appNo";
            res = _dapper.Get<GPF_EDLIS>(sql, dParams, CommandType.Text);

            sql = $@"select top 1 * from GPF_EDLIS_BankDetails where LOWER(ApplicationNumber) = @appNo";
            res.BankDetails = _dapper.Get<GPF_EDLIS_BankDetails>(sql, dParams, CommandType.Text);

            sql = $@"select * from GPF_EDLIS_Nominee where LOWER(ApplicationNumber) = @appNo";
            res.Nominees = _dapper.GetAll<GPF_EDLIS_Nominee>(sql, dParams, CommandType.Text);
            
            sql = $@"select * from GPF_EDLIS_Family where LOWER(ApplicationNumber) = @appNo";
            res.Family = _dapper.GetAll<GPF_EDLIS_Family>(sql, dParams, CommandType.Text);

            sql = $@"select * from GPF_EDLIS_Documents where LOWER(ApplicationNumber) = @appNo";
            res.Documents = _dapper.GetAll<GPF_EDLIS_Documents>(sql, dParams, CommandType.Text);

            res.Employee = _empInfo.GetEmployee(res.EmployeeExternalCode);

            return res;
        }

        public int UpdateEDLISApplicationStatus(GPF_EDLIS edlis)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@applicationNumber", edlis.ApplicationNumber);
            dParams.Add("@edlisStatus", edlis.EDLISStatus);
            if (edlis.FinalSettlementAmount.HasValue) dParams.Add("@finalSettlementAmount", edlis.FinalSettlementAmount.Value);
            if (!string.IsNullOrWhiteSpace(edlis.Remark)) dParams.Add("@remark", edlis.Remark);
            if (!string.IsNullOrWhiteSpace(edlis.AG2Remark)) dParams.Add("@ag2remark", edlis.AG2Remark);

            var sql = "update GPF_EDLIS set " +
                ((!edlis.FinalSettlementAmount.HasValue) ? "" : "FinalSettlementAmount = @finalSettlementAmount, ") +
                ((string.IsNullOrWhiteSpace(edlis.Remark)) ? "" : "Remark = @remark, ") +
                ((string.IsNullOrWhiteSpace(edlis.AG2Remark)) ? "" : "AG2Remark = @ag2remark, ") +
                "EDLISStatus = @edlisStatus " +
                "where ApplicationNumber = @applicationNumber";

            var i = _dapper.Execute(sql, dParams, CommandType.Text);
            return i;
        }

        public int UpdateEDLISApplicationPaid(GPF_EDLIS edlis)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@applicationNumber", edlis.ApplicationNumber);

            var sql = "update GPF_EDLIS set " +
                "EDLISStatus = 'Completed', " +
                "PaymentDone = 1 " +
                "where ApplicationNumber = @applicationNumber";

            var i = _dapper.Execute(sql, dParams, CommandType.Text);
            return i;
        }

        private string GenerateEDLISApplicationNumber(string orgCode)
        {
            var pre = $@"{orgCode}/{DateTime.Now.ToString("yyyyMM")}/";
            var list = new List<string>();
            for (int i = 1; i <= 99; i++)
                list.Add(pre + i.ToString().PadLeft(2, '0'));
            var sql = $@"select top 1 * from ( VALUES {string.Join(",", list.Select(q => "('" + q + "')"))} ) as t(a) where a not in (select distinct ApplicationNumber from GPF_EDLIS)";
            var res = _dapper.GetDataSet(sql, new Dictionary<string, object> { }, CommandType.Text);
            return res.Tables[0].Rows[0][0].ToString();
        }

    }
}
