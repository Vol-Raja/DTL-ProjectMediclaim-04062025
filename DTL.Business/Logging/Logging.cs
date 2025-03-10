using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System.Data;

namespace DTL.Business.Logging
{
    public class Logging : ILogging
    {
        private readonly IDapperService _dapper;
        public Logging(IDapperService dapper)
        {
            _dapper = dapper;
        } 

        public void Savelog(string methodname, string text)
        {  
            var dbparams = new DynamicParameters();
            dbparams.Add("@Method", methodname, DbType.String);
            dbparams.Add("@ErrorText", text, DbType.String); 

            _dapper.Execute("SaveLog", dbparams);
        }
    }
}
