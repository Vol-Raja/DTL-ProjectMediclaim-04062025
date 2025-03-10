using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.Mediclaim;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DTL.Business.Mediclaim.CGMS
{
    public class CGMSRates : ICGMSRates
    {

        public readonly IDapperService _dapper;
        public CGMSRates(IDapperService dapper)
        {
            _dapper = dapper;
        }
        public List<CGMSModel> GetCHMSRates()
        {
            var dbparams = new DynamicParameters();
            var _list = _dapper.GetAll<CGMSModel>("GetCGMSRates", dbparams, CommandType.StoredProcedure);
            return _list.ToList();
        }
    }
}
