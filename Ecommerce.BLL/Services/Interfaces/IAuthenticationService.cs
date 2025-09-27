using Ecommerce.DAL.DTO.Requests;
using Ecommerce.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest loginRequest);
        Task<UserResponse> RegisterAsync(RegisterRequest registerRequest);
         Task<string> ConfirmEmail(string token, string userId);
        Task<string> ForgetPassword(ForgetPasswordRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);
    }
}
