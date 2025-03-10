using DTL.Model.Models.UserManagement;

namespace DTL.Business.UserManagement
{
    public interface IForgotPassword
    {
        ForgotPasswordModel GetOTPDetail(string email);
        int SaveOTPDetail(ForgotPasswordModel forgotPasswordModel);
    }
}
