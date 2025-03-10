using Dapper;
using DocumentFormat.OpenXml.EMMA;
using DTL.Business.Dapper;
using DTL.Business.GPF.EmployeeInfo;
using DTL.Model.Models.GPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DTL.Business.GPF.Settlement
{
    public class Settlement : ISettlement
    {
        private readonly IDapperService _dapper;
        private readonly IEmployeeInfo _empInfo;

        public Settlement(IDapperService dapper, IEmployeeInfo empInfo)
        {
            _dapper = dapper;
            _empInfo = empInfo;
        }

        public GPF_Settlement SaveSettlementApplication(GPF_Settlement obj)
        {
            obj.ApplicationNumber = GenerateSettlementApplicationNumber(obj.Organization);
            obj.CreatedOn = DateTime.Now;
            obj.SettlementStatus = "Pending";

            var dParams = _dapper.GetDynamicParams(obj, "Id", new List<string> { "Nominees", "BankDetails", "Documents", "Employee" });
            var id = _dapper.Execute("Save_GPF_Settlement", dParams);

            if (id > 0)
            {
                obj.BankDetails.ApplicationNumber = obj.ApplicationNumber;
                dParams = _dapper.GetDynamicParams(obj.BankDetails, "Id", new List<string> { });
                var i = _dapper.Execute("Save_GPF_Settlement_BankDetails", dParams);
                foreach (var n in obj.Nominees)
                {
                    n.ApplicationNumber = obj.ApplicationNumber;
                    dParams = _dapper.GetDynamicParams(n, "Id", new List<string> { });
                    i = _dapper.Execute("Save_GPF_Settlement_Nominee", dParams);
                }
            }

            return obj;
        }
        public int SaveSettlementApplicationDoc(GPF_Settlement_Documents obj)
        {
            var dParams = _dapper.GetDynamicParams(obj, "Id", new List<string> { });
            var id = _dapper.Execute("Save_GPF_Settlement_Documents", dParams);
            return id;
        }

        public IEnumerable<GPF_Settlement_View> GetSettlementApplications(string status, string applicationNumber = "", string employeeCode = "", string organization = "")
        {
            var dParams = new DynamicParameters();
            dParams.Add("@status", status.ToLower().Trim(), DbType.String);
            dParams.Add("@applicationNumber", (applicationNumber ?? "").ToLower().Trim(), DbType.String);
            dParams.Add("@employeeCode", (employeeCode ?? "").ToLower().Trim(), DbType.String);
            dParams.Add("@organization", (organization ?? "").ToLower().Trim(), DbType.String);
            var sql = $@"
                select a.*, b.EMP_NAME 'EmployeeName', EMP_CODE 'EmployeeCode', b.gpf_no 'GPFAccountNumber'
                from GPF_Settlement a
                left join EMPLOYEE_DEFINATION b on a.EmployeeExternalCode = b.EXTERNAL_CODE
                where (@status = 'all' or LOWER(SettlementStatus) = @status) 
                and (@applicationNumber = '' or LOWER(ApplicationNumber) like '%'+@applicationNumber+'%')
                and (@employeeCode = '' or LOWER(a.EmployeeExternalCode) like '%'+@employeeCode+'%')
                and (@employeeCode = '' or LOWER(b.EMP_NAME) like '%'+@employeeCode+'%')
                and (@organization = '' or @organization = 'all' or LOWER(b.COMP_CODE) = @organization)
                order by a.CreatedOn desc
            ";
            var ds = _dapper.GetAll<GPF_Settlement_View>(sql, dParams, CommandType.Text);
            return ds;
        }

        //public IEnumerable<GPF_Settlement_View> GetSettlementApplicationsByEmployee(string externalCode, string type = "all", string status = "all")
        //{
        //    var dParams = new DynamicParameters();
        //    dParams.Add("@empCode", externalCode.ToLower().Trim(), DbType.String);
        //    dParams.Add("@type", type.ToLower().Trim(), DbType.String);
        //    dParams.Add("@status", status.ToLower().Trim(), DbType.String);
        //    var sql = $@"
        //        select a.*, b.EMP_NAME 'EmployeeName', EMP_CODE 'EmployeeCode'
        //        from GPF_Settlement a
        //        left join EMPLOYEE_DEFINATION b on a.EmployeeExternalCode = b.EXTERNAL_CODE
        //        where LOWER(EmployeeExternalCode) = @empCode 
        //            and (@type = 'all' or LOWER(SettlementType) = @type)
        //            and (@status = 'all' or LOWER(ApplicationStatus) = @status)
        //        order by a.CreatedOn desc
        //    ";
        //    var ds = _dapper.GetAll<GPF_Settlement_View>(sql, dParams, CommandType.Text);
        //    sql = $@"select * from GPF_Settlement_Details where LOWER(ApplicationNumber) in ('" + string.Join("','", ds.Select(q => q.ApplicationNumber)) + "')";
        //    var ds2 = _dapper.GetAll<GPF_Settlement_Details>(sql, dParams, CommandType.Text);
        //    foreach (var d in ds)
        //        d.Details = ds2.FirstOrDefault(q => q.ApplicationNumber == d.ApplicationNumber);
        //    return ds;
        //}

        public IEnumerable<GPF_Settlement_View_Disburs> GetSettlementApplicationsDisburs()
        {
            var dParams = new DynamicParameters();
            var sql = $@"
                select a.*, b.EMP_NAME 'EmployeeName', EMP_CODE 'EmployeeCode', b.gpf_no 'GPFAccountNumber'
                    , c.BankName, c.BranchName, c.AccountNumber, c.IFSC, c.BIS
                from GPF_Settlement a
                left join EMPLOYEE_DEFINATION b on a.EmployeeExternalCode = b.EXTERNAL_CODE
                left join GPF_Settlement_BankDetails c on a.ApplicationNumber = c.ApplicationNumber
                where SettlementStatus in ('Pending with Disbursement', 'Completed')
                order by a.CreatedOn desc
            ";
            var ds = _dapper.GetAll<GPF_Settlement_View_Disburs>(sql, dParams, CommandType.Text);
            return ds;
        }

        public GPF_Settlement GetSettlementApplication(string applicationNumber)
        {
            GPF_Settlement res;
            var dParams = new DynamicParameters();
            dParams.Add("@appNo", applicationNumber.ToLower().Trim(), DbType.String);

            var sql = $@"select top 1 * from GPF_Settlement where LOWER(ApplicationNumber) = @appNo";
            res = _dapper.Get<GPF_Settlement>(sql, dParams, CommandType.Text);

            sql = $@"select top 1 * from GPF_Settlement_BankDetails where LOWER(ApplicationNumber) = @appNo";
            res.BankDetails = _dapper.Get<GPF_Settlement_BankDetails>(sql, dParams, CommandType.Text);

            sql = $@"select * from GPF_Settlement_Nominee where LOWER(ApplicationNumber) = @appNo";
            res.Nominees = _dapper.GetAll<GPF_Settlement_Nominee>(sql, dParams, CommandType.Text);

            sql = $@"select * from GPF_Settlement_Documents where LOWER(ApplicationNumber) = @appNo";
            res.Documents = _dapper.GetAll<GPF_Settlement_Documents>(sql, dParams, CommandType.Text);

            res.Employee = _empInfo.GetEmployee(res.EmployeeExternalCode);

            return res;
        }

        public int UpdateSettlementApplicationStatus(GPF_Settlement Settlement)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@applicationNumber", Settlement.ApplicationNumber);
            dParams.Add("@SettlementStatus", Settlement.SettlementStatus);
            if (Settlement.FinalSettlementAmount.HasValue) dParams.Add("@finalSettlementAmount", Settlement.FinalSettlementAmount.Value);
            if (!string.IsNullOrWhiteSpace(Settlement.Remark)) dParams.Add("@remark", Settlement.Remark);
            if (!string.IsNullOrWhiteSpace(Settlement.AG2Remark)) dParams.Add("@ag2remark", Settlement.AG2Remark);

            var sql = "update GPF_Settlement set " +
                ((!Settlement.FinalSettlementAmount.HasValue) ? "" : "FinalSettlementAmount = @finalSettlementAmount, ") +
                ((string.IsNullOrWhiteSpace(Settlement.Remark)) ? "" : "Remark = @remark, ") +
                ((string.IsNullOrWhiteSpace(Settlement.AG2Remark)) ? "" : "AG2Remark = @ag2remark, ") +
                "SettlementStatus = @SettlementStatus " +
                "where ApplicationNumber = @applicationNumber";

            var i = _dapper.Execute(sql, dParams, CommandType.Text);
            return i;
        }

        public int UpdateSettlementApplicationPaid(GPF_Settlement Settlement)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@applicationNumber", Settlement.ApplicationNumber);

            var sql = "update GPF_Settlement set " +
                "SettlementStatus = 'Completed', " +
                "PaymentDone = 1 " +
                "where ApplicationNumber = @applicationNumber";

            var i = _dapper.Execute(sql, dParams, CommandType.Text);
            return i;
        }

        private string GenerateSettlementApplicationNumber(string orgCode)
        {
            var pre = $@"{orgCode}/{DateTime.Now.ToString("yyyyMM")}/";
            var list = new List<string>();
            for (int i = 1; i <= 99; i++)
                list.Add(pre + i.ToString().PadLeft(2, '0'));
            var sql = $@"select top 1 * from ( VALUES {string.Join(",", list.Select(q => "('" + q + "')"))} ) as t(a) where a not in (select distinct ApplicationNumber from GPF_Settlement)";
            var res = _dapper.GetDataSet(sql, new Dictionary<string, object> { }, CommandType.Text);
            return res.Tables[0].Rows[0][0].ToString();
        }

    }
}
