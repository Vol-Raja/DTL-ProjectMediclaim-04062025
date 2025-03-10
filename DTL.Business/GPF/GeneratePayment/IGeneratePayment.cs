using DTL.Model.Models.GPF;
using System.Collections.Generic;

namespace DTL.Business.GPF.GeneratePayment
{
    public interface IGeneratePayment
    {
        IEnumerable<GPFGeneratePaymentModel> GetGeneratePaymentByParam(int? paymentId, string applicationtype, string loanType);
        IEnumerable<GPFGeneratePaymentModel> GetArchiveGeneratePaymentByParam(int? paymentId, string applicationtype, string loanType);
        IEnumerable<GPFWithdrawalModel> GetGPFWithdrawalByParam(string applicationID);
        int SaveGeneratePayment(GPFGeneratePaymentModel generatePaymentModel);
        int DeleteGeneratePayment(int paymentId, string modifiedBy);
        
    }
}
