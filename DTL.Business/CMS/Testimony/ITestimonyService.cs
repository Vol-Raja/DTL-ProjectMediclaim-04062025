using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.Testimony
{
    public interface ITestimonyService
    {
         public string AddEditTestimonyData(TestimonyModel TestimonyModel, bool isEdit);
        public IEnumerable<TestimonyModel> GetTestimonyModelByParam(Guid? Id, bool? IsDeleted);

    }
}
