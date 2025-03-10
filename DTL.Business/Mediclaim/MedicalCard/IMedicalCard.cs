using DTL.Model.Models.Mediclaim;
using System.Collections.Generic;

namespace DTL.Business.Mediclaim.MedicalCard
{
    public interface IMedicalCard
    {
        int SaveMedicalCard(MedicalCardModel medicalCardModel);
        IEnumerable<MedicalCardModel> GetMedicalCardsByCreatedBy(string MedicalCardId);
        IEnumerable<MedicalCardModel> GetMedicalCardsByParam(int MedicalCardId, string CardNo, string EmployeeNo, string PPONo);
        int DeleteMedicalCard(MedicalCardModel medicalCardModel);
        int UpdateMedicalCard(MedicalCardModel medicalCardModel);
    }
}