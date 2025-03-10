using DTL.Model.Models.Mediclaim;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.EmpaneledHospital
{
    public interface IEmpaneledHospital
    {
        List<EmpaneledHospitalModel> GetEmpaneledHospitals();
    }
}
