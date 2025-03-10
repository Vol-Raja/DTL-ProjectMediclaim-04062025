using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.Notice
{
    public interface INoticeService
    {
        public string AddEditNoticeData(NoticeModel model, bool isEdit);
        public IEnumerable<NoticeModel > GetNoticeModelByParam(Guid? Id, bool? IsDeleted, bool? IsArchieved);
    }
}
