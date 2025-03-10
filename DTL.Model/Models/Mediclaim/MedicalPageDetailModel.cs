using System;

namespace DTL.Model.Models.Mediclaim
{
    public class MedicalPageDetailModel : BaseModel
    {
		public int PageDetailId { get; set; }
		public int PageNumber { get; set; }
		public string EmployeeNumber { get; set; }
		public string Name  { get; set; }
		public string PPONumber { get; set; }
		public string Designation { get; set; }
		public string Department { get; set; }
		public string CardCategory { get; set; }
		public string MobileNumber { get; set; }
		public DateTime DateOfRetirement { get; set; }
		public string SpouseName { get; set; }
		public string LBP { get; set; }
		public string Dispensary { get; set; }
        public bool IsDelete { get; set; }
		public string NameOfDependent { get; set; }
		public string RelationWithRetiree { get; set; }
		public DateTime DependentDob { get; set; }
		public string DependentDobYYYYMMDD
		{
			get { return DependentDob.ToString("yyyy-MM-dd"); }
		}
		public string DateOfRetirementYYYYMMDD
		{
			get { return DateOfRetirement.ToString("yyyy-MM-dd"); }
		}
	}
}
