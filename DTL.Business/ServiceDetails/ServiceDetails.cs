using Dapper;
using DTL.Business.Dapper;
using System.Data;
using DTL.Model.Models;

namespace DTL.Business.ServiceDetails
{
    public class ServiceDetails : IServiceDetails
    {
        private readonly IDapperService _dapper;

        public ServiceDetails(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }

        
    }
}
