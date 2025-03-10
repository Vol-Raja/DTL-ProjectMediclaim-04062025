using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DTL.Model.CommonModels;
using Microsoft.AspNetCore.Http;

namespace DTL.Model.Models
{
    public class EmployeeProfileModel : BaseModel
    {
        [Required]
        public Int64 EmployeeId { get; set; }
        public Int64? PPOOrderId { get; set; }
        public byte[] ProfileImage { get; set; }
        public string Prefix { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string DTLOffice { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public DateTime ServiceStartDate { get; set; }
        [Required]
        public DateTime ServiceEndDate { get; set; }
        [Required]
        public string ResidentialAddress { get; set; }
        public string PermanentAddress { get; set; }
        [Required]
        public string RState { get; set; }
        public string PState { get; set; }
        [Required]
        public string RDistrict { get; set; }
        public string PDistrict { get; set; }
        [Required]
        public string RZipcode { get; set; }
        public string PZipcode { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public int PPOStatusId { get; set; }
        public string PPOStatusName { get; set; }
        public IFormFile files { get; set; }
        public IEnumerable<DTLOfficeList> DTLOfficeList { get; set; }
        public int age { get; set; }
        public string MobileCountryCode { get; set; }
        public string PhoneCountryCode { get; set; }
        public Guid TDSId { get; set; }
        public string ReasonOfRetirement { get; set; }
        public bool IsDelete { get; set; }
        public string DeleteReason { get; set; }
        public string RejectReason { get; set; }
        public string SpouseName { get; set; }
        public string FatherName { get; set; }
        public bool IsExistingEmployee { get; set; }
    }
}