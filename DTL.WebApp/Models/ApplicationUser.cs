using DTL.Model.Models.UserManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string fullName { get; set; }
        public bool IsTempPassword { get; set; }
        public string Designation { get; set; }
        [NotMapped]
        public string Discriminator { get; set; }
    }
}
