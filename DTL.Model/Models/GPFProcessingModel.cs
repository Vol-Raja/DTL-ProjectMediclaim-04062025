using DTL.Model.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class GPFProcessingModel : BaseModel
    {
        public string EmployeeName { get; set; }

        public string Employer { get; set; }

        public int EmployeeID { get; set; }

        public string Designation { get; set; }

        public decimal Membercontribution { get; set; }

        public decimal MemberInterest { get; set; }

        public decimal GPFAmount { get; set; }

        public DateTime Month { get; set; }

        public DateTime ProcessingDate { get; set; }

        public IEnumerable<DTLOfficeList> DTLOfficeList { get; set; }
    }
}
