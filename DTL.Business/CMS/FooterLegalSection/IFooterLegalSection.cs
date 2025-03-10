using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.FooterLegalSection
{
    public interface IFooterLegalSectionService
    {
        public string AddEditFooterLegalSectionData(FooterLegalSectionModel model, bool isEdit);
        public IEnumerable<FooterLegalSectionModel > GetFooterLegalSectionModelByParam(Guid? Id, bool? IsDeleted);
    }
}
