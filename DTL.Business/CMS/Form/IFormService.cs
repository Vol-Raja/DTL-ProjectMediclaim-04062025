using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.Form
{
    public interface IFormService
    {
        public string AddEditFormData(FormModel model, bool isEdit);
        public IEnumerable<FormModel > GetFormModelByParam(Guid? Id, bool? IsDeleted);
    }
}
