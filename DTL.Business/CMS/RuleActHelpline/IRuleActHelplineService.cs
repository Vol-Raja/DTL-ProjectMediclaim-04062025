using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.RuleActHelpline
{
    public interface IRuleActHelplineService
    {
        public string AddEditRuleActHelplineData(RuleActHelplineModel model, bool isEdit);
        public IEnumerable<RuleActHelplineModel> GetRuleActHelplineModelByParam(Guid? Id, bool? IsDeleted, bool? IsHelpline);
    }
}
