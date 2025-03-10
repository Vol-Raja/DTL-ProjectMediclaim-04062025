using DTL.Model.Models.Mediclaim;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.MedicalPageDetail
{
    public interface IMedicalPageDetail
    {
        IEnumerable<MedicalPageDetailModel> GetMedicalPageDetailsByParam(int? pageDetailId, string employeeNumber, string ppoNumber, string department, int? pageNumber, string createdBy);
        int SaveMedicalPageDetail(MedicalPageDetailModel medicalPageDetailModel);
        int DeleteMedicalPageDetail(MedicalPageDetailModel medicalPageDetailModel);
        int UpdateMedicalPageDetail(MedicalPageDetailModel medicalPageDetailModel);
    }
}
