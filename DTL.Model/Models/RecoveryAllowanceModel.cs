using DTL.Model.CommonModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.Models
{
    public class RecoveryAllowanceModel : BaseModel
    {
        #region Properties

        [Required]
        public string PensionerName { get; set; }

        public int EmployeeID { get; set; }

        public Guid EmployeeRegistrationId { get; set; }

        public string EmployerName { get; set; }

        public string ChangeType { get; set; }

        public string Reason { get; set; }

        public string TypeOfRecovery { get; set; }

        public decimal RecoveryAmount { get; set; }

        public string RecoveryOption { get; set; }
        public decimal MonthlyPension { get; set; }

        public decimal ApplicableAmount { get; set; }

        public decimal MonthlyPensionAfter { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public IEnumerable<DTLOfficeList> DTLOfficeList { get; set; }

        public IEnumerable<ChangeTypeList> ChangeTypeList { get; set; }

        public IEnumerable<ReasonList> ReasonList { get; set; }

        public IEnumerable<RecoveryOptionList> RecoveryOptionList { get; set; }

        public IEnumerable<TypeOfRecoveryList> TypeOfRecoveryList { get; set; }

        #endregion
    }
}
