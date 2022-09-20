using JamesAPI.Auth;
using JamesAPI.Models;
using JamesAPI.ViewModels;
using Microsoft.IdentityModel.JsonWebTokens;

namespace JamesAPI.Domian.Contracts
{
    public interface IAuthorizeService
    {
        Task<AuthenticationResult> Register(RegisterModel user);
        Task<LoginResponse> VerifyPassword(string email, string password);
        Task<LoginResponse> LoginAsync(Login login);
    }
}
