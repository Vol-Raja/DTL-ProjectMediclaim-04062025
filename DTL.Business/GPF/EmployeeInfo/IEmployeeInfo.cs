using DTL.Model.Models.GPF;
using System.Collections.Generic;

namespace DTL.Business.GPF.EmployeeInfo
{
    public interface IEmployeeInfo
    {
        IEnumerable<GPFEmployeeInfoModel> Get(string orgCode, string retirementStatus);
        GPFEmployeeDetail GetEmployee(string externalCode);
        bool EmployeeExists(string externalCode);
        IEnumerable<object> EmployeeAutocomplete(string orgCode, string term);

    }
}
