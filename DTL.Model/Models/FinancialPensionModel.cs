using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class FinancialPensionModel : BaseModel
    {
        [Required]
        public decimal Pension { get; set; }
        public decimal Commutation { get; set; }
        public decimal Gratuity { get; set; }

        public decimal DA { get; set; }

        public decimal AQP { get; set; }

        public decimal LeaveEncashment { get; set; }

    }
}
