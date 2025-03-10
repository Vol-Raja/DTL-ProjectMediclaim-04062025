using System;

namespace DTL.Model.Models.UserManagement
{
    public class ForgotPasswordModel
    {
        public string UserEmail { get; set; }
        public string OTP { get; set; }
        public DateTime OtpExpiry { get; set; } 
        public string OtpValidationToken { get; set; }
    }
}
