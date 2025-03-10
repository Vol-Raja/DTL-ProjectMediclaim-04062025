using System;

namespace DTL.Model.Models.Mediclaim
{
    public class DependentInformation
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime DateOfBirth { get; set; }
		public int Age { get; set; }
		public string RelationWithRetire { get; set; }
		public int NonCashlessClaimId { get; set; }
		public bool Active { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedDate { get; set; }

		public string DateOfBirthYYYYMMDD
		{
			get
			{
				return DateOfBirth.ToString("yyyy-MM-dd");
			}
		}
	}
}
