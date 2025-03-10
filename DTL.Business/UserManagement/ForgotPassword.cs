using Dapper;
using DTL.Business.Dapper;
using DTL.Model.Models.UserManagement;
using System.Data;

namespace DTL.Business.UserManagement
{
    public class ForgotPassword : IForgotPassword
    {
        private readonly IDapperService _dapper;
        public ForgotPassword(IDapperService dapper)
        {
            _dapper = dapper;
        }
        public ForgotPasswordModel GetOTPDetail(string email)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@UserEmail", email, System.Data.DbType.String);
            return _dapper.Get<ForgotPasswordModel>("GetOTPDetailByParam", dbparams);
        }

        public int SaveOTPDetail(ForgotPasswordModel forgotPasswordModel)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Otp", forgotPasswordModel.OTP, DbType.String);
            dbparams.Add("@OtpValidationToken", forgotPasswordModel.OtpValidationToken, DbType.String);
            dbparams.Add("@UserEmail", forgotPasswordModel.UserEmail, DbType.String);
            return _dapper.Execute("SaveOTPDetail", dbparams);
        }
    }
}
