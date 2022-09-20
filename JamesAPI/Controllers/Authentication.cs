using JamesAPI.Auth;
using JamesAPI.Domian.Contracts;
using JamesAPI.Domian.Services;
using JamesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JamesAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {

        private IAuthorizeService _authorizeService;
        public Authentication(IAuthorizeService authorizeService)
        {
            this._authorizeService = authorizeService;
        }


        //# Register method
        [AllowAnonymous]
        [HttpPost("UserRegister")]
        public async Task<IActionResult> AuthUser([FromBody] RegisterModel user)
        {
            
            if(user.Password != user.ConfirmPSW)
            {
                return null;
            }
            var token = await _authorizeService.Register(user);
            if (!token.Success)
            {
                return BadRequest();
                //return Unauthorized();    
            }
            return Ok(new AuthenticationResult { Token = token.Token, Success=true});
        }


        //# Login method
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login.Password == null) return BadRequest("password is null");
            if (login.UserEmail == null) return BadRequest("email is null");

            var result = await _authorizeService.LoginAsync(login);

            //direct to 
            //make a login result
            return Ok(result);
        }
        
    }

}
