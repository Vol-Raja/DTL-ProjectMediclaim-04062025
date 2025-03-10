using DTL.Model.Models.GPF;
using System.Collections.Generic;

namespace DTL.Business.GPF.EDLIS
{
    public interface IEDLIS 
    {
        GPF_EDLIS SaveEDLISApplication(GPF_EDLIS obj);
        int SaveEDLISApplicationDoc(GPF_EDLIS_Documents obj);
        IEnumerable<GPF_EDLIS_View> GetEDLISApplications(string status, string applicationNumber = "", string employeeCode = "", string organization = "");
        IEnumerable<GPF_EDLIS_View_Disburs> GetEDLISApplicationsDisburs();
        GPF_EDLIS GetEDLISApplication(string applicationNumber);
        int UpdateEDLISApplicationStatus(GPF_EDLIS edlis);
        int UpdateEDLISApplicationPaid(GPF_EDLIS edlis);
    }
}
