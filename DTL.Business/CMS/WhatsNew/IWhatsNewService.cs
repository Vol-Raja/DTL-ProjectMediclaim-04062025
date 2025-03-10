using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.WhatsNew
{
    public interface IWhatsNewService
    {
        public string AddEditWhatsNewData(WhatsNewModel model, bool isEdit);
        public IEnumerable<WhatsNewModel > GetWhatsNewModelByParam(Guid? Id, bool? IsDeleted, bool? IsArchieved);
    }
}
