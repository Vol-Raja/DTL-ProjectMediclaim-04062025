using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.Link
{
    public interface ILinkService
    {
        public string AddEditLinkData(LinkModel linkModel, bool isEdit);
        public IEnumerable<LinkModel> GetLinkModelByParam(Guid? TenderId, bool? IsDeleted, bool? IsArchieved);
    }
}
