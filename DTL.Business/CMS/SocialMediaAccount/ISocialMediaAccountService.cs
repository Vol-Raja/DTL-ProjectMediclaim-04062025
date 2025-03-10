using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.SocialMediaAccount
{
    public interface ISocialMediaAccountService
    {
        public string AddEditSocialMediaAccountData(SocialMediaAccountModel model, bool isEdit);
        public IEnumerable<SocialMediaAccountModel > GetSocialMediaAccountModelByParam(Guid? Id, bool? IsDeleted);
    }
}
