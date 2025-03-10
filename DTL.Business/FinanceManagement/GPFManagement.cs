using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.FinanceManagement
{
    public class GPFManagement : IGPFManagement
    {
        private readonly IDapperService _dapper;
        public GPFManagement(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }
        public IEnumerable<GPFManagementModel> GetGPFManagementReport(DateTime dt, string EmployerName, string EmployeeID, string operation)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Date", dt, DbType.Date);
            dbparams.Add("@EmployerName", EmployerName, DbType.String);
            dbparams.Add("@EmployeeID", EmployeeID, DbType.String);
            return _dapper.GetAll<GPFManagementModel>("usp_GPFReport_GetAll", dbparams);
        }
    }
}
