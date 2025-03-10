using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class ServiceHistoryModel : BaseModel
    {
        [Required]
        public Guid EmployeeRegistrationId { get; set; }
        public string DTLDepartmentName { get; set; }
        //public string CategoryOfEmployeement { get; set; }
        public string ReasonOfRetirement { get; set; }
        public string IsMedicalCardRequired { get; set; }
        public int TotalServiceYear { get; set; }
        public int TotalServiceMonth { get; set; }
        public int TotalServiceDays { get; set; }
        public int AdditionalServiceYears { get; set; }
        public int AdditionalServiceMonth { get; set; }
        public int AdditionalServiceDays { get; set; }
        public int ServiceNotCountedYear { get; set; }
        public int ServiceNotCountedMonth { get; set; }
        public int ServiceNotCountedDays { get; set; }
        public int QualifyingServiceYear { get; set; }
        public int QualifyingServiceMonth { get; set; }
        public int QualifyingServiceDays { get; set; }
        public string ServiceDetails { get; set; }
        public string Perticulars { get; set; }
        public string HalfYear { get; set; }
        public decimal BasicPay { get; set; }
        public decimal DAPercent { get; set; }
        public decimal DA { get; set; }
        public decimal NPA { get; set; }
        public byte[] PaySlip1 { get; set; }
        public byte[] PaySlip2 { get; set; }
        public byte[] PaySlip3 { get; set; }

        public string PaySlip1Path { get; set; }
        public string PaySlip2Path { get; set; }
        public string PaySlip3Path { get; set; }

        public string Designation { get; set; }

        public int LevelPayment { get; set; }

        /*
        public DateTime? DOB { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        */
    }
}
