using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.Business.Models
{
    public class EmployeeRegistrationModel
    {
        public string Prefix { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DOB{ get; set; }
        public string Gender{ get; set; }

        public string DTLOffice{ get; set; }
        public string Designation{ get; set; }
        public DateTime ServiceStartDate{ get; set; }
        public DateTime ServiceEndDate { get; set; }
        public string RAddress { get; set; }
        public string PAddress { get; set; }
        public string RState { get; set; }
        public string PState { get; set; }
        public string RDistrict { get; set; }
        public string PDistrict { get; set; }
        public string RZipcode { get; set; }
        public string PZipcode { get; set; }
        public string EmailAddress { get; set; }
        public int Mobile { get; set; }
        public int Phone { get; set; }
    }
}
