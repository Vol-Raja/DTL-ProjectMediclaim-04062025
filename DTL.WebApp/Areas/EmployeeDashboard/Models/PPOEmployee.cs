using DTL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.EmployeeDashboard.Models
{
    public class PPOEmployee : BaseModel
    {
        public string EMP_Name { get; set; }
        public int ppo_no { get; set; }
        public DateTime DOB { get; set; }
        public string email_id { get; set; }
        public string Perm_add { get; set; }
        public string Mobile_no { get; set; }
        public string Sex { get; set; }
    }
}
