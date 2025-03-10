using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.AimVision
{
    public interface IAimVisionService
    {
        public string AddEditAimVisionData(AimVisionModel AimVisionModel, bool isEdit);
        public IEnumerable<AimVisionModel> GetAimVisionModelByParam(Guid? TenderId, bool? IsDeleted, bool? IsArchieved);
    }
}
