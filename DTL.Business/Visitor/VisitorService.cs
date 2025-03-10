using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models;
using System.Collections.Generic;
using System.Data;

namespace DTL.Business.Visitor
{
    public class VisitorService : IVisitorService
    {
        private readonly IDapperService _dapper;

        public VisitorService(IDapperService dapper)
        {
            _dapper = dapper;
            _dapper.GetDbconnection();

        }
        public string AddEditVisitorData(VisitorModel VisitorModel)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@Id", VisitorModel.Id, DbType.Guid);
            dbparams.Add("@VisitDate", VisitorModel.VisitDate, DbType.DateTime);
            dbparams.Add("@Note", VisitorModel.Note, DbType.String);

            dbparams.Add("@ReturnMsg", "", DbType.String, direction: ParameterDirection.Output);
            _dapper.Execute("AddEditVisitor", dbparams);
            var result = dbparams.Get<string>("@ReturnMsg");
            return result;
        }

    

        public IEnumerable<VisitorModel> GetVisitor()
        {
            var dbparams = new DynamicParameters();
         
            
            return _dapper.GetAll<VisitorModel>("GetVisitor", dbparams);
        }

        
    }
}

