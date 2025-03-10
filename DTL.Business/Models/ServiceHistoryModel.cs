using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Models
{
    public class ServiceHistoryModel
    {
        public string DTLDepartmentName { get; set; }
        public string CategoryOfEmployeement { get; set; }
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
        public DateTime HalfYear { get; set; }
        public int BasicPay { get; set; }
        public int NPA { get; set; }
        public string UploadPaySlip { get; set; }

    }
}
