using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Disbursement
{
    public class DisbursementReport : IDisbursementReport
    {

        private readonly IDapperService _dapper;

        public DisbursementReport(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }
        public IEnumerable<DisbursementReportModel> GetDisbursementReportData(DateTime dt, string EmployerName, string EmployeeID)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Date", dt, DbType.Date);
            dbparams.Add("@EmployerName", EmployerName, DbType.String);
            dbparams.Add("@EmployeeID", EmployeeID, DbType.String);
            return  _dapper.GetAll<DisbursementReportModel>("usp_DisbursementReport_GetAll", dbparams);
        }
    }
}
