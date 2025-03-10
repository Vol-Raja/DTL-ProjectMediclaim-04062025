using Dapper;
using DTL.Business.Dapper;
using System.Data;
using DTL.Model.Models;

namespace DTL.Business.NomineeDetails
{
    public class NomineeDetails : INomineeDetails
    {
        private readonly IDapperService _dapper;

        public NomineeDetails(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }

       
    }
}
