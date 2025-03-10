using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DTL.Model.CommonModels;

namespace DTL.Model.Models
{
   public class InvestmentDeclarationModel : BaseModel
    {
        public string FinancialYear { get; set; }
        public string InvestmentType { get; set; }
        public string InvestmentId { get; set; }
        public string InvestmentTitle { get; set; }
        public string ReferenceNo { get; set; }
        public decimal InvestedAmount { get; set; }
        public string ClosingPeriod { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal ExpectedProfitMargin { get; set; }
        public decimal ExpectedROI { get; set; }
        public decimal ExpectedProfitAmount { get; set; }
        public decimal ActualReturnOnInvestment { get; set; }
        public int InvestmentClose { get; set; }

        public string FromMonthYr { get; set; }

        public string ToMonthYr { get; set; }
        public string Others { get; set; }


        public IEnumerable<InvestmentTypeList> InvestmentTypeList { get; set; }
    }
}
