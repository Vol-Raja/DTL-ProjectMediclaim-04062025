using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.CMSHospital
{
    public interface ICMSHospitalService
    {
        public string AddEditCMSHospitalData(CMSHospitalModel model, bool isEdit);
        public IEnumerable<CMSHospitalModel > GetCMSHospitalModelByParam(Guid? Id, bool? IsDeleted);
    }
}
