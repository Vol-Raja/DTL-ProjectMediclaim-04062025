using DTL.Model.Models.GPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.EmployeeDashboard.Data
{
    public interface IGPF
    {

        IEnumerable<GPFEmployeeInfoModel> GetEmployeeGPF();
        IEnumerable<GPFEmployeeInfoModel> Get(string ppo_no, string orgCode, string retirementStatus);
        GPFEmployeeDetail GetEmployee(string externalCode, string ppo);
    }
}
