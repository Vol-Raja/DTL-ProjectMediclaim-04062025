using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTL.Model.Models;


namespace DTL.Business.Grievance
{
    public interface IAddGrievance
    {

        string AddEditGrievance(GrievanceModel grievanceModel, bool isEdit);
        GrievanceModel GetGrievanceDetailsById(Guid grvId);
        Task<IEnumerable<GrievanceModel>> GetGrievanceDetails();
        int DeleteGrievance(Guid grvId);
        int GrievanceReplyMessage(GrievanceModel grievanceModel);
        GrievanceModel GetGrievanceDetailsByGrievanceId(int grvId);
    }
}
