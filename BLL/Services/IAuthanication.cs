using DAL.DTOs.Request;
using DAL.DTOs.Response;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAuthanication
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<bool> ConfirmEmailAsync(string token,string userId);
        Task<ForgetPasswordResponse> RequestPassowrdReset(ForgetPasswordRequest request);
        Task<ResetPasswordResponse> PassowrdReset(RestPasswordRequest request);
        Task<LoginResponse> RefreshTokenAsync(TokenApiModel request);
    }
}
