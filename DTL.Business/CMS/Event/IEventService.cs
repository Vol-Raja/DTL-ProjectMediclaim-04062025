using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.Event
{
    public interface IEventService
    {
        public string AddEditEventData(EventModel model, bool isEdit);
        public IEnumerable<EventModel > GetEventModelByParam(Guid? Id, bool? IsDeleted);
    }
}
