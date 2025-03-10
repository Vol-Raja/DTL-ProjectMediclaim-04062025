using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.Tender
{
    public interface ITenderService
    {
        public string AddEditTenderData(TenderModel tenderModel, bool isEdit);
        public IEnumerable<TenderModel> GetTenderModelByParam(Guid? TenderId, bool? IsDeleted, bool? IsArchieved);
    }
}
