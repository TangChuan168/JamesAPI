using JamesAPI.Auth;
using JamesAPI.Domian.Contracts;
using JamesAPI.Models;
using JamesAPI.ViewModels;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace JamesAPI.Domian.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private IRepository<User> _userRepo;
        private JwtAuthManager _jwtAuthManager;

        public AuthorizeService(
            IRepository<User> user,
            JwtAuthManager authManager

            )
        {
            _userRepo = user;
            _jwtAuthManager = authManager;
        }


        public async Task<AuthenticationResult> Register(RegisterModel Register)
        {
            try
            {
                if (Register == null) throw new ApplicationException("Incomplete register request - user is null");
                if (Register.Email == null) throw new ApplicationException("Incomplete register request - user's email is required");
                if (Register.Password == null || Register.Password.Length <= 8) throw new ApplicationException("Incomplete register request - Password atleast 8 chart long");
                //if user exist
                var IsUserExist = await _userRepo.FindUserByEmail(Register.Email);
                if(IsUserExist)
                {
                    return new AuthenticationResult()
                    {
                        ErrorMessage = "Email address has been used in registration."
                    };               
                };
                //create user
                var Uid1 = Guid.NewGuid();
                var newUser = new User()
                {
                    Uid = Uid1,
                    LastName = Register.LastName,
                    FirstName = Register.FirstName,
                    Email = Register.Email,
                    Password = Register.Password,
                    MobilePhone = Register.MobilePhone,                 
                };
                //create token for user
                var data = _jwtAuthManager.Create(newUser);
                //add Token for User
                newUser.AuthCode = data;
                //add user to DB
                await _userRepo.Add(newUser);

                var result = new AuthenticationResult()
                {
                    Token = data.ToString(),
                    Success = true,
                    ErrorMessage = "null"

                };
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Register error - " + ex.Message);
            }
        }

        public async Task<LoginResponse> LoginAsync(Login login)
        {
            var passwordVarification = await VerifyPassword(login.UserEmail, login.Password);

            if (!passwordVarification.IsSuccess)
            {
                return new LoginResponse { IsSuccess = false, Message = passwordVarification.Message };
            }

            return new LoginResponse { IsSuccess = true, Token= passwordVarification.Token};
        }

        public async Task<LoginResponse> VerifyPassword(string email, string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ApplicationException("Login fail - Password is null");

            var user = await _userRepo.GetByEmailAsync(email); 
            
            if (user.Password != password) {
                //throw new ApplicationException("Login fail -wrong Email or password");
                return new LoginResponse
                {
                    IsSuccess = false, 
                    Message = "Current Email is not match password"
                };
            };
            return new LoginResponse
            {
                IsSuccess = true,
                Token=user.AuthCode,
                LastName = user.LastName,   
                Email = user.Email,
                Message ="200"
            };
        }


    }
}
