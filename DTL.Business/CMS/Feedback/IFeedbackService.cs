using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.Feedback
{
    public interface IFeedbackService
    {
        public string AddEditFeedbackData(FeedbackModel model, bool isEdit);
        public IEnumerable<FeedbackModel > GetFeedbackModelByParam(Guid? Id, bool? IsDeleted);
    }
}
