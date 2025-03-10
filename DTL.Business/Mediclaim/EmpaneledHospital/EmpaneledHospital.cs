using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.Mediclaim;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DTL.Business.Mediclaim.EmpaneledHospital
{
    public class EmpaneledHospital : IEmpaneledHospital
    {
        public readonly IDapperService _dapper;
        public EmpaneledHospital(IDapperService dapper)
        {
            _dapper = dapper;
        }
        /// <summary>
        /// Get list of Empaneled Hospitals
        /// </summary>
        /// <returns>List of EmpaneledHospital Model</returns>
        public List<EmpaneledHospitalModel> GetEmpaneledHospitals()
        {
            var dbparams = new DynamicParameters();
            var _list = _dapper.GetAll<EmpaneledHospitalModel>("GetEmpaneledHospitals", dbparams, CommandType.StoredProcedure);
            return _list.ToList();
        }
    }
}
