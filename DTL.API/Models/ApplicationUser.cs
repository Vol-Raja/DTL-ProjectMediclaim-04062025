using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTL.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string fullName { get; set; }
        public bool IsTempPassword { get; set; }
        [NotMapped]
        public string Discriminator { get; set; }
    }
}
