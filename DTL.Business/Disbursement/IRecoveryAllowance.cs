using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Disbursement
{
    public interface IRecoveryAllowance
    {
        //string InsertUpdateRecoveryDetails(RecoveryAllowanceModel recoveryAllowanceModel, bool isUpdate);
        string InsertUpdateRecoveryAllowanceDetails(RecoveryAllowanceModel recoveryAllowanceModel, bool isUpdate);

        Task<IEnumerable<RecoveryAllowanceModel>> GetPensionersAllAsync();
    }
}
