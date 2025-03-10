using DTL.Model.Models;
using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.Visitor
{
    public interface IVisitorService
    {
        public string AddEditVisitorData(VisitorModel VisitorModel);
        IEnumerable<VisitorModel> GetVisitor();
    }
}
