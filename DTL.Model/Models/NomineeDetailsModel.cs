using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class NomineeDetailsModel : BaseModel
    {
        [Required]
        public Guid EmployeeRegistrationId { get; set; }
        public string MemberName { get; set; }
        public string RelationShip { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //public string ContigencyReason { get; set; }
        public string GuardianName { get; set; }
        public int Commutation { get; set; }
        public int Arrear { get; set; }
        public string MemberOf { get; set; }

        public string GuardianRelation { get; set; }

        public string GuardianAddress { get; set; }

    }
}
