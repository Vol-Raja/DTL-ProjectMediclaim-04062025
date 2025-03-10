using DTL.Model.Models.GPF;
using System.Collections.Generic;

namespace DTL.Business.GPF.LoanProcessing
{
    public interface ILoanProcessing
    {
        IEnumerable<GPFWithdrawalModel> GetGPFWithdrawalByParam(int? withdrawId, string employer, int? month, int? year, string employeeId,int? applicationStatusId, string applicationNumber, string createdBy);
        int ApproveRejectGPFApplication(ApproveRejectModel approveReject);
        int UpdatePhysicalSubmit(PhysicalSubmitModel physicalSubmit);
        IEnumerable<GPFDocumentModel> GetDocumentsByParam(int withdrawalId);

        GPF_Loan SaveLoanApplication(GPF_Loan obj);
        int SaveLoanApplicationDoc(GPF_Loan_Documents obj);
        IEnumerable<GPF_Loan_View> GetLoanApplications(string type, string status);
        IEnumerable<GPF_Loan_View> GetLoanApplicationsByEmployee(string externalCode, string type = "all", string status = "all");
        GPF_Loan GetLoanApplication(string applicationNumber);
        int UpdateLoanApplicationStatus(GPF_Loan loan);
        IEnumerable<GPF_Loan_View_Disburs> GetLoanApplicationsDisburs(string type);
        int UpdateLoanApplicationPaid(GPF_Loan loan);
    }
}
