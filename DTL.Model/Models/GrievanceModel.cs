using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTL.Model.CommonModels;

namespace DTL.Model.Models
{

    public class GrievanceModel : BaseModel
    {
        [Required]

        public String Department { get; set; }
        public String PPONo { get; set; }
        public Int64 EmployeeId { get; set; }
        public String Subject { get; set; }
        public String MobileNo { get; set; }
        public String GrievanceDetails { get; set; }
        public String Reply { get; set; }

        public long GrievanceID { get; set; }

        public string UserType { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }
        public string Office { get; set; }
        public string Remarks { get; set; }

        public string Status { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] AttachmentFileInEnglish { get; set; }

        public string EnglishFileName { get; set; }
        public string EnglishContentType { get; set; }

        public string EmailID { get; set; }

        public string Description { get; set; }
        public string Address { get; set; }

        public bool IsReadOnly { get; set; }
        public byte[] GrievanceHeadAttachmentFileInEnglish { get; set; }

        public string GrievanceHeadEnglishFileName { get; set; }
        public string GrievanceHeadEnglishContentType { get; set; }
        public IEnumerable<GrievanceDepartmentList> GrievanceDepartmentList { get; set; }
    }
}
