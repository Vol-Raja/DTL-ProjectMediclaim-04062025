using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.ContactUs
{
    public interface IContactUsService
    {
        public string AddEditContactUsData(ContactUsModel model, bool isEdit);
        public IEnumerable<ContactUsModel > GetContactUsModelByParam(Guid? Id, bool? IsDeleted);
    }
}
