using DTL.Model.CommonModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class AddQtmPaymentModel : BaseModel
    {
        #region Properties

        [Required]
        public string PensionerName { get; set; }

        public Guid EmployeeRegistrationId { get; set; }
        public DateTime DOB { get; set; }
        public int EmployeeID { get; set; }

        public string EmployerName { get; set; }

        public int CurrentAge { get; set; }

        public string AgeGroup { get; set; }

        public decimal MonthlyPension { get; set; }

        public decimal ApplicableQuantam { get; set; }

        public decimal IncrementAmount { get; set; }

        public string IncrementPercentage { get; set; }

        public decimal AQPMonthlyPension { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public IEnumerable<DTLOfficeList> DTLOfficeList { get; set; }

        public IEnumerable<AgeGroupList> AgeGroupList { get; set; }
        
        #endregion
    }

    
}
