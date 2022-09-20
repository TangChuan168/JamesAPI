
using JamesAPI.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JamesAPI.Auth
{
    public class JwtAuthManager
    {
        //key declaration
        private readonly IConfiguration _configuration;

        public JwtAuthManager(IConfiguration config)
        {
            _configuration = config;
        }

        public string Create(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //Ecoding and produce token

            var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Token"]);
            //decript what token look like to return to the user
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email,user.Email)

             }),

            SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var result = tokenHandler.WriteToken(token);
            return result;  
        }
        

    }
}
