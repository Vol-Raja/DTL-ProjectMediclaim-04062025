using System;
using System.Collections.Generic;

namespace DTL.Model.Models.GPF
{
    public class GPFProcessingModel : BaseModel
    {
        public string EmployerName { get; set; }
        public DateTime ProcessingDate { get; set; }
        public string Interest { get; set; }
        public string EmployeeType { get; set; }
        public List<GPFProcessing> GPFProcessingList { get; set; }
    }

    public class GPFProcessing
    {
        public int GPFId { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Employer { get; set; }
        public string Designation { get; set; }
        public decimal MemberContribution { get; set; }
        public decimal MemberInterest { get; set; }
        public decimal GPFAmount { get; set; }
        public decimal LoanAmount { get; set; }
        public string EmployeeType { get; set; } 
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
