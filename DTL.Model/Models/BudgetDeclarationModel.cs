using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTL.Model.CommonModels;

namespace DTL.Model.Models
{

    public class BudgetDeclarationModel : BaseModel
    {
        [Required]
        
        
        public string FinancialYear { get; set; }
        
        public string AllocationProgram { get; set; }
        
        public decimal AllocatedFunds { get; set; }

        public string DisbursementPeriod { get; set; }
        public string DisbursementAuthority { get; set; }

        public decimal DisbursedAmount { get; set; }

        public decimal OutStanding { get; set; }

        public IEnumerable<BudgetAllocationProgramList> BudgetAllocationProgramList { get; set; }

        public IEnumerable<DisbursementPeriodList> DisbursementPeriodList { get; set; }

        public string Type { get; set; }
    }
}
