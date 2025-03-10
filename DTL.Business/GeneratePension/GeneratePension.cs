using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.GeneratePension
{
    public class GeneratePension : IGeneratePension
    {
        private readonly IDapperService _dapper;

        public GeneratePension(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public IEnumerable<GeneratePensionModel> GetGeneratePension(int? month, int? year, string bank, string employer, int? ppono)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Employer", employer, DbType.String);
            dbparams.Add("@Month", month, DbType.Int32);
            dbparams.Add("@Year", year, DbType.Int32);
            dbparams.Add("@Bank", bank, DbType.String);
            dbparams.Add("@PPONo", ppono, DbType.Int32);
            return _dapper.GetAll<GeneratePensionModel>("GetGeneratePensionByParam", dbparams);
        }
    }
}
