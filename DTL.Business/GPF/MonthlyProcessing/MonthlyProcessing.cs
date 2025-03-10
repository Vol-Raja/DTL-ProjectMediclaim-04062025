using ClosedXML.Excel;
using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.GPF;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace DTL.Business.GPF.EmployeeInfo
{
    public class MonthlyProcessing : IMonthlyProcessing
    {
        private readonly IDapperService _dapper;
        private readonly IEmployeeInfo _empInfo;
        private readonly IHostingEnvironment _env;

        public MonthlyProcessing(IDapperService dapper, IEmployeeInfo empInfo, IHostingEnvironment env)
        {
            _dapper = dapper;
            _empInfo = empInfo;
            _env = env;
        }

        public MonthlyGPFProcessData ValidateExcel(byte[] fileBytes)
        {
            XLWorkbook wb;
            MonthlyGPFProcessData res = new MonthlyGPFProcessData();
            res.FileSummary = new MonthlyProcessingExcelSummary();
            res.Entries = new List<MonthlyGPFProcessedEntry>();
            using (MemoryStream ms = new MemoryStream(fileBytes))
                wb = new XLWorkbook(ms);
            var dt = ImportExceltoDatatable(wb);
            var columns = new List<string> { "Employee Code", "Employee Name", "Organization", "Designation", "GPF Contribution", "Month", "Year" };
            res.FileSummary.MissingColumns = columns.Where(c => !dt.Columns.Contains(c)).ToList();
            dt.Columns.Add("Remark", typeof(string));
            int remarkIndex = dt.Columns.Count + 1, rowCount = 1;
            wb.Worksheet(1).Column(remarkIndex).SetValue("Remark");
            foreach (DataRow dr in dt.Rows)
            {
                rowCount++;
                string code = dr["Employee Code"].ToString(),
                    contribution = dr["GPF Contribution"].ToString(),
                    month = dr["Month"].ToString(),
                    year = dr["Year"].ToString();
                if (string.IsNullOrWhiteSpace(code))
                {
                    res.FileSummary.Failure++;
                    dr["Remark"] = "'Employee Code' is blank";
                    wb.Worksheet(1).Row(rowCount).Cell(dt.Columns["Employee Code"].Ordinal + 1).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    wb.Worksheet(1).Row(rowCount).Cell(remarkIndex).SetValue("'Employee Code' is blank");
                    continue;
                }
                if (!_empInfo.EmployeeExists(code))
                {
                    res.FileSummary.Failure++;
                    dr["Remark"] = "'Employee Code' not found in Emplyee Master";
                    wb.Worksheet(1).Row(rowCount).Cell(dt.Columns["Employee Code"].Ordinal + 1).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    wb.Worksheet(1).Row(rowCount).Cell(remarkIndex).SetValue("'Employee Code' not found in Emplyee Master");
                    continue;
                }
                if (!decimal.TryParse(contribution, out decimal xf))
                {
                    res.FileSummary.Failure++;
                    dr["Remark"] = "Invalid value specified for 'Contribution'";
                    wb.Worksheet(1).Row(rowCount).Cell(dt.Columns["GPF Contribution"].Ordinal + 1).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    wb.Worksheet(1).Row(rowCount).Cell(remarkIndex).SetValue("Invalid value specified for 'GPF Contribution'");
                    continue;
                }
                if (!DateTime.TryParse("01-" + month + "-" + year, out DateTime xd))
                {
                    res.FileSummary.Failure++;
                    dr["Remark"] = "Invalid value specified for 'Month' & 'Year'";
                    wb.Worksheet(1).Row(rowCount).Cell(dt.Columns["Month"].Ordinal + 1).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    wb.Worksheet(1).Row(rowCount).Cell(dt.Columns["Year"].Ordinal + 1).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    wb.Worksheet(1).Row(rowCount).Cell(remarkIndex).SetValue("Invalid value specified for 'Month' & 'Year'");
                    continue;
                }
                wb.Worksheet(1).Row(rowCount).Cell(remarkIndex).SetValue("Ok");
                res.FileSummary.Success++;
                var emp = _empInfo.GetEmployee(code);
                res.Entries.Add(new MonthlyGPFProcessedEntry
                {
                    EmpCode = code,
                    EmpId = emp.BasicDetails.EmployeeCode,
                    EmpName = emp.Name,
                    //EmpType = emp.BasicDetails.EmployeeType,
                    Designation = emp.BasicDetails.Designation,
                    GPFContribution = xf,
                    Month_Year = xd,
                    OrganisationCode = emp.OrganisationCode,

                });
            }
            using (XLWorkbook wb1 = new XLWorkbook())
            using (MemoryStream ms1 = new MemoryStream())
            {
                wb1.AddWorksheet(wb.Worksheet(1));
                wb1.SaveAs(ms1);
                res.FileSummary.ProcessedExcelBytes = ms1.ToArray();
            }
            wb.Dispose();
            return res;
        }

        public byte[] SampleExcel(string orgCode)
        {
            byte[] res;
            var path = _env.WebRootPath + Path.DirectorySeparatorChar + "files" + Path.DirectorySeparatorChar + "GPF.xlsx";
            using XLWorkbook wb = new XLWorkbook(path);
            var ws = wb.Worksheet(1);
            int rowCount = 0;
            var dt = ImportExceltoDatatable(wb);
            var empData = _empInfo.Get(orgCode, "all");
            Func<string, int> co = (string col) => dt.Columns[col].Ordinal + 1;
            foreach (var emp in empData)
            {
                rowCount++;
                ws.Row(rowCount + 1).Cell(co("Sr. No")).SetValue(rowCount);
                ws.Row(rowCount + 1).Cell(co("Employee Code")).SetValue(emp.ExternalCode);
                ws.Row(rowCount + 1).Cell(co("Employee Name")).SetValue(emp.EmployeeName);
                ws.Row(rowCount + 1).Cell(co("Organization")).SetValue(emp.Organisation);
                ws.Row(rowCount + 1).Cell(co("Designation")).SetValue(emp.Designation);
                ws.Row(rowCount + 1).Cell(co("Month")).SetValue(DateTime.Now.ToString("MMM"));
                ws.Row(rowCount + 1).Cell(co("Year")).SetValue(DateTime.Now.Year);
            }
            using (XLWorkbook wb1 = new XLWorkbook())
            using (MemoryStream ms1 = new MemoryStream())
            {
                wb1.AddWorksheet(wb.Worksheet(1));
                wb1.SaveAs(ms1);
                res = ms1.ToArray();
            }
            return res;
        }

        public MonthlyGPFProcessedEntry ProcessEntry(MonthlyGPFProcessedEntry entry, DateTime processingDate, decimal interestRate, string employeeType)
        {
            entry.ProcessingDate = processingDate;
            entry.InterestRate = interestRate;
            entry.MemberInterest = interestRate * entry.GPFContribution / 100;
            entry.MonthlyGPFAmount = entry.GPFContribution + entry.MemberInterest;
            entry.GPFAccountBalance = entry.GPFContribution + entry.MemberInterest;
            entry.EmpType = employeeType;
            return entry;
        }

        public bool SaveEntry(MonthlyGPFProcessModel model)
        {
            var dParams = _dapper.GetDynamicParams(model, "GPFProcessingId", new List<string> { "CreatedDate", "ModifiedBy", "ModifiedDate" });
            var id = _dapper.Execute("SaveGPFProcessing_2", dParams);
            return id > 0;
        }

        public decimal GetGPFSum(string orgCode, string empCode)
        {
            var dbparams = new Dictionary<string, object> {
                { "@orgCode", orgCode},
                { "@empCode", empCode},
            };
            var sql = $@"select ISNULL(sum(GPFAmount), 0) from GPFProcessing where Employer = @orgCode and EmployeeNumber = @empCode";
            var ds = _dapper.GetDataSet(sql, dbparams, CommandType.Text);
            return (decimal)ds.Tables[0].Rows[0][0];
        }

        private DataTable ImportExceltoDatatable(XLWorkbook workBook)
        {
            IXLWorksheet workSheet = workBook.Worksheet(1);
            DataTable dt = new DataTable();
            bool firstRow = true;
            foreach (IXLRow row in workSheet.Rows())
            {
                if (firstRow)
                {
                    foreach (IXLCell cell in row.Cells())
                        dt.Columns.Add(cell.Value.ToString());
                    firstRow = false;
                }
                else
                {
                    if (row.FirstCellUsed() == null || row.LastCellUsed() == null) continue;
                    dt.Rows.Add();
                    int i = 0;
                    foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                        i++;
                    }
                }
            }
            return dt;
        }


        #region Report 

        public IEnumerable<MonthlyGPFProcessDataModel> GetData(string orgCode,string empCode, DateTime? dtFrom, DateTime? dtTo)
        {
            if (!dtFrom.HasValue) dtFrom = new DateTime(1900, 1, 1);
            if (!dtTo.HasValue) dtTo = new DateTime(1900, 1, 1);
            var dParams = new DynamicParameters();
            dParams.Add("@orgCode", orgCode, DbType.String);
            dParams.Add("@empCode", empCode, DbType.String);
            dParams.Add("@dtFrom", dtFrom, DbType.Date);
            dParams.Add("@dtTo", dtTo, DbType.Date);

            var sql = $@"
                select z.*, e.EMP_CODE 'EmployeeCode', (
	                select sum(GPFAmount) 
	                from GPFProcessing 
	                where Employer = z.Employer and EmployeeNumber = z.EmployeeNumber 
		                and Year * 100 + Month <= z.Year * 100 + z.Month
                ) 'GPFSum'
                from GPFProcessing z
                left join EMPLOYEE_DEFINATION e on z.EmployeeNumber = e.EXTERNAL_CODE
                where (@orgCode = 'all' or z.Employer = @orgCode)
                    and (@empCode = 'all' or z.EmployeeNumber = @empCode)
                    and (@dtFrom = '1900-01-01' or DATEFROMPARTS(z.Year, z.Month, 1) >= @dtFrom)
                    and (@dtTo = '1900-01-01' or DATEFROMPARTS(z.Year, z.Month, 1) <= @dtTo)
            ";

            IEnumerable<MonthlyGPFProcessDataModel> data = _dapper.GetAll<MonthlyGPFProcessDataModel>(sql, dParams, CommandType.Text);
            return data;
        }

        #endregion


        #region Summary 

        public IEnumerable<GPFSummary> GetSummary(string orgCode, DateTime? dtFrom, DateTime? dtTo)
        {
            if (!dtFrom.HasValue) dtFrom = new DateTime(1900, 1, 1);
            if (!dtTo.HasValue) dtTo = new DateTime(1900, 1, 1);
            var dParams = new DynamicParameters();
            dParams.Add("@orgCode", orgCode, DbType.String);
            dParams.Add("@dtFrom", dtFrom, DbType.Date);
            dParams.Add("@dtTo", dtTo, DbType.Date);

            var sql = $@"
                select Employer 'OrganisationCode', Month, Year , SUM(MemberContribution) MemberContribution, SUM(MemberInterest) MemberInterest
                from GPFProcessing
                where (@orgCode = 'all' or Employer = @orgCode)
                    and (@dtFrom = '1900-01-01' or DATEFROMPARTS(Year, Month, 1) >= @dtFrom)
                    and (@dtTo = '1900-01-01' or DATEFROMPARTS(Year, Month, 1) <= @dtTo)
                group by Employer, Year, Month
            ";

            IEnumerable<GPFSummary> data = _dapper.GetAll<GPFSummary>(sql, dParams, CommandType.Text);
            return data;
        }

        public CurrentGPFBalance GetCurrentBalance(string orgCode, string accNo, int year)
        {
            var dtStart = new DateTime(year - 1, 04, 01);
            var dtEnd = new DateTime(year, 03, 31);
            var empData = _empInfo.GetEmployee(accNo);
            var report = GetData(orgCode, accNo, dtStart, dtEnd);
            var res = new CurrentGPFBalance
            {
                OrganisationCode = empData.OrganisationCode,
                EmployeeCode = empData.BasicDetails.EmployeeCode,
                AccountNo = "",
                DOJ = empData.BasicDetails.DateOfJoining,
                EmployeeName = empData.Name,
                FinancialYear = "FY " + (dtStart.Year % 100) + "-" + (dtEnd.Year % 100),
                GPFBalance = report.Any() ? report.Select(q => q.GPFSum).Max() : 0
            };
            return res;
        }

        #endregion


        #region Statement 

        public GPFStatement GetStatement(string orgCode, string accNo, int year)
        {
            var dtStart = new DateTime(year - 1, 04, 01);
            var dtEnd = new DateTime(year, 03, 31);
            var empData = _empInfo.GetEmployee(accNo);
            var report = GetData(orgCode, accNo, dtStart, dtEnd);
            var min = report.Where(q => q.Year * 100 + q.Month == report.Min(q => q.Year * 100 + q.Month)).OrderBy(q => q.GPFSum).FirstOrDefault();
            var max = report.Where(q => q.Year * 100 + q.Month == report.Max(q => q.Year * 100 + q.Month)).OrderByDescending(q => q.GPFSum).FirstOrDefault();
            var res = new GPFStatement
            {
                OrganisationCode = empData.OrganisationCode,
                EmployeeCode = empData.BasicDetails.EmployeeCode,
                AccountNo = "",
                DOB = empData.BasicDetails.DateOfBirth,
                DOJ = empData.BasicDetails.DateOfJoining,
                EmployeeName = empData.Name,
                FinancialYear = "FY " + (dtStart.Year % 100) + "-" + (dtEnd.Year % 100),
                Email = empData.Email,
                FatherName = empData.BasicDetails.FatherName,
                Opening = min != null ? min.GPFSum - min.GPFAmount : 0,
                Closing = max != null ? max.GPFSum - max.GPFAmount : 0,
                GPFBalance = report.Any() ? report.Select(q => q.GPFSum).Max() : 0,
                MonthlyData = report
            };
            return res;
        }

        #endregion

    }
}
