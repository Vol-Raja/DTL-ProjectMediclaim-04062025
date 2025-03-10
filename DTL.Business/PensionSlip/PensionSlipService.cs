using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.PensionSlip
{
    public class PensionSlipService : IPensionSlipService
    {
        private readonly IDapperService _dapper;
        public PensionSlipService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();
        }

        public FinancialPensionModel GetPensionDataByFinanacialYear(Guid EmpRegId, int FinYear)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@EmpRegId", EmpRegId, DbType.Guid);
            dbparams.Add("@FinYear",FinYear, DbType.Int32);
            var data = _dapper.Get<FinancialPensionModel>("sp_GetFinancialYearWisePensionDetails", dbparams);
            return data;
        }
    }
}
