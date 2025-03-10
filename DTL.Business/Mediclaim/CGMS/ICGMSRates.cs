using DTL.Model.Models.Mediclaim;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.CGMS
{
    public interface ICGMSRates
    {
        List<CGMSModel> GetCHMSRates();
    }
}
