using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Models
{
    public class NomineeDetailsModel
    {
        public string MemberName { get; set; }
        public string RelationShip { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContigencyReason { get; set; }
        public string GuardianName { get; set; }
        public int Commutation { get; set; }
        public int Arrear { get; set; }
        public string MemberOf { get; set; }
        
    }
}
