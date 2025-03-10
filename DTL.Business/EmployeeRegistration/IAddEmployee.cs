using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTL.Business.EmployeeRegistration
{
    public interface IAddEmployee
    {
        string AddEditEmployeeData(EmployeeProfileModel employeeRegistrationModel,bool isEdit);
        Task<IEnumerable<EmployeeProfileModel>> GetAllEmployeeAsync(string role, bool IsExistingEmployee = false);
        Task<IEnumerable<EmployeeProfileModel>> GetAllArchiveEmployeeAsync(Guid employeeId);
        EmployeeProfileModel GetEmployeeById(Guid employeeId);
        EmployeeProfileModel GetArchiveEmployeeById(Guid employeeId);
        EmployeeModel GetEmployeeDetailsById(Guid employeeId);
        int AddForm5Data(Form5Model form5Model,bool isEdit);
        int AddNominee(NomineeDetailsModel nomineeDetailsModel);
        int RemoveNominee(Guid employeeId);
        string AddPensionSlip(PensionSlipModel pensionSlipModel,bool isEdit);
        string AddServiceHistory(ServiceHistoryModel serviceHistoryModel,bool isEdit);
        int UpatePesionAppStatus(Guid empId,int status);
        IEnumerable< EmployeeProfileModel> GetEmployeeByDTLOffice(String DtlOffice,String Type,String SearchVal);

        int UpdateRejectionStatusRemarks(Guid empId,string Role, int status,string Remarks);

        int DeleteEmp(Guid employeeId,string deleteReason);

        int UpatePesionOrderID(Guid empId);
    }

}
