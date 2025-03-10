using DTL.Model.Models.GPF;
using System.Collections.Generic;

namespace DTL.Business.GPF.Settlement
{
    public interface ISettlement
    {
        GPF_Settlement SaveSettlementApplication(GPF_Settlement obj);
        int SaveSettlementApplicationDoc(GPF_Settlement_Documents obj);
        IEnumerable<GPF_Settlement_View> GetSettlementApplications(string status, string applicationNumber = "", string employeeCode = "", string organization = "");
        IEnumerable<GPF_Settlement_View_Disburs> GetSettlementApplicationsDisburs();
        GPF_Settlement GetSettlementApplication(string applicationNumber);
        int UpdateSettlementApplicationStatus(GPF_Settlement Settlement);
        int UpdateSettlementApplicationPaid(GPF_Settlement Settlement);
    }
}
