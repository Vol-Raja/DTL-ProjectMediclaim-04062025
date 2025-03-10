using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class EmployeeModel
    {
        public EmployeeModel()
        {
            //employeeProfile = new EmployeeProfileModel();
            form5 = new Form5Model();
            nomineeDetails = new List<NomineeDetailsModel>();
            serviceHistory = new ServiceHistoryModel();
            pensionSlip = new PensionSlipModel();
        }
        public EmployeeProfileModel employeeProfile { get; set; }
        public Form5Model form5 { get; set; }
        public List<NomineeDetailsModel> nomineeDetails { get; set; }
        public ServiceHistoryModel serviceHistory { get; set; }
        public PensionSlipModel pensionSlip { get; set; }
    }
}
