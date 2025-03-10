using Dapper;
using DTL.Business.Dapper;
using System.Data;
using DTL.Model.Models;

namespace DTL.Business.Form5
{
    public class Form5 : IForm5
    {
        private readonly IDapperService _dapper;

        public Form5(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }

        
    }
}
