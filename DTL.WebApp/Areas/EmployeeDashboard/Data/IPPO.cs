using DTL.WebApp.Areas.EmployeeDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.EmployeeDashboard.Data
{
   public interface IPPO
    {
        IEnumerable<PPOEmployee> GetEmployeePPO(string ppo_no);
        //IEnumerable<PPOEmployee> ViewEmployee(string ppo_no);
       // int ViewEmployee(PPOEmployee ppoemp);
        PPOEmployee ViewEmployee(string ppo_no);
    }
}
