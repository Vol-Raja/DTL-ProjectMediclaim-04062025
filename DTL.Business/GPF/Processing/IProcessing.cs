using DTL.Model.Models.GPF;
using System.Collections.Generic;

namespace DTL.Business.GPF.Processing
{
    public interface IProcessing
    {
        IEnumerable<GPFProcessing> GetGPFProcessingSummaryByParam(string employer, string employeName, string employeeNumber);
        IEnumerable<GPFProcessing> GetDetailByParam(string employer, int? month, int? year, string employeeNUmber, string employeeName);
        void SaveGPFProcessing(IList<GPFProcessing> gpfProcessingModel, string createdBy);
    }
}
