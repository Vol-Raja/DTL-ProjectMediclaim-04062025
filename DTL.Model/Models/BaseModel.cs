using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
   public class BaseModel
    {
        [Key]
        public Guid ID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifideBy { get; set; }
        public DateTime ModifideDate { get; set; }
    }
}
