namespace Elearning.Contracts.Services
{
    using Elearning.Contracts.Common;
    using Elearning.Shared.DTOs.IdentityUserDTOs;

    public interface IAuthService
    {
        Task<GenericResponseModel<object>> Registeration(RegisterModel model);
        Task<GenericResponseModel<object>> Login(LoginModel model);
        Task<GenericResponseModel<object>> RegisterAdmin(RegisterModel model);
        Task<UserProfileDto?> GetUserProfile(string email);
        Task<GenericResponseModel<object>?> UpdateUserProfile(string email, UserProfileDto userProfileDto);
        
        Task<bool> VerifyOTP(string otp, string email);
        Task<object> ForgotPassword(ForgotPasswordRequest model);
        Task<object> ChangePassword(ForgotPasswordRequest model);
        Task<object> ResetPassword(ResetPasswordModel model);
    }
}
