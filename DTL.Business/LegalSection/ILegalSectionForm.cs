using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.LegalSection
{
    public interface ILegalSectionForm
    {
        IEnumerable<LegalSectionModel> GetAllCases();
        string AddEditLegalCaseDetails(LegalSectionModel legalSectionModel, bool isEdit);
        LegalSectionModel GetCaseDetailsById(Guid Id);
        int ApproveLegalCases(Guid id, string modifiedBy);
        int DeleteLegalCases(Guid id, string modifiedBy);

        int GetLegalCaseCount();
        IEnumerable<LegalSectionModel> GetCaseDetailsByCaseId(string CaseId);
    }
}
