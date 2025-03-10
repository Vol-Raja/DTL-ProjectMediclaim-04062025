using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.About
{
    public interface IAboutService
    {
        public string AddEditAboutData(AboutModel AboutModel, bool isEdit);
        public IEnumerable<AboutModel> GetAboutModelByParam(Guid? TenderId, bool? IsDeleted, bool? IsArchieved);
        public string AddEditTrusteeData(TrusteeModel AboutModel, bool isEdit);
        public IEnumerable<TrusteeModel> GetTrusteeModelByParam(Guid? Id, bool? IsDeleted);

    }
}
