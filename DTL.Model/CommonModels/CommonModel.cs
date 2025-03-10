using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Model.CommonModels
{
    public class CommonModel
    {
        public IEnumerable<DTLOfficeList> DTLOfficeList { get; set; }

        public IEnumerable<AgeGroupList> AgeGroupList { get; set; }

        public IEnumerable<ChangeTypeList> ChangeTypeList { get; set; }

        public IEnumerable<ReasonList> ReasonList { get; set; }

        public IEnumerable<DesignationList> DesignationList { get; set; }

        public IEnumerable<GPFWithdrawalPurposeList> GPFWithdrawalPurposeList { get; set; }

        public IEnumerable<RecoveryOptionList> RecoveryOptionList { get; set; }

        public IEnumerable<TypeOfRecoveryList> TypeOfRecoveryList { get; set; }

        public IEnumerable<BudgetAllocationProgramList> BudgetAllocationProgramList { get; set; }

        public IEnumerable<DisbursementPeriodList> DisbursementPeriodList { get; set; }

        public IEnumerable<InvestmentTypeList> InvestmentTypeList { get; set; }

        public IEnumerable<CourtTypeList> CourtTypeList { get; set; }

        public IEnumerable<LegalCaseStatusTypeList> LegalCaseStatusTypeList { get; set; }

        public IEnumerable<GrievanceDepartmentList> GrievanceDepartmentList { get; set; }

        public IEnumerable<EmailConnectionList> EmailConnectionList { get; set; }

        public IEnumerable<MediclaimCashlessDocumentList> MediclaimCashlessDocumentList { get; set; }

    }
    public class DTLOfficeList
    {
        public string DTLOffice { get; set; }
    }

    public class AgeGroupList
    {
        public string AgeGroup { get; set; }
    }

    public class ChangeTypeList
    {
        public string ChangeType { get; set; }
    }

    public class ReasonList
    {
        public string Reason { get; set; }
    }

    public class DesignationList
    {
        public string Designation { get; set; }
    }
    public class GPFWithdrawalPurposeList
    {
        public string GPFWithdrawalPurpose { get; set; }
    }
    public class RecoveryOptionList
    {
        public string RecoveryOption { get; set; }
    }

    public class BudgetAllocationProgramList
    {
        public string BudgetAllocationProgram { get; set; }
    }

    public class DisbursementPeriodList
    {
        public string DisbursementPeriod { get; set; }
    }
    public class InvestmentTypeList
    {
        public string InvestmentType { get; set; }
    }
    public class TypeOfRecoveryList
    {
        public string TypeOfRecovery { get; set; }
    }
    public class GrievanceDepartmentList
    {
        public string GrievanceDepartment { get; set; }
    }

    public class CourtTypeList
    {
        public string CourtType { get; set; }
    }
    public class LegalCaseStatusTypeList
    {
        public string StatusType { get; set; }
    }

    public class EmailConnectionList
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }


    public class MediclaimCashlessDocumentList
    {
        public string Doc1 { get; set; }
        public string Doc2 { get; set; }
        public string Doc3 { get; set; }
        public string Doc4 { get; set; }
        public string Doc5 { get; set; }
        public string Doc6 { get; set; }
        public string Doc7 { get; set; }
        public string Doc8 { get; set; }
        public string Doc9 { get; set; }
        public string Doc10 { get; set; }

    }

}
