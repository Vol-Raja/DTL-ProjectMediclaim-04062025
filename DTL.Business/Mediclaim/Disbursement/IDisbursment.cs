using DTL.Model.Models.Mediclaim;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.Disbursement
{
    public interface IDisbursment
    {

        #region Cashless
        IEnumerable<CashlessModel> GetDisbursedCashlessClaims(bool disbursed, bool? paid);
        int PayCashlessClaim(int claimId, bool paid, string modifiedBy);
        // add by nirbhay 27/02/2025
        IEnumerable<CashlessModel> GetDisbusCashlessByPPO(string ppono, bool paid, bool Disbus);
        //end
        #endregion

        #region NonCashless
        IEnumerable<NonCashlessModel> GetDisbursedNonCashlessClaims(bool disbursed, bool? paid);
        int PayNonCashlessClaim(int claimId, bool paid, string modifiedBy);
        #endregion

    }
}
