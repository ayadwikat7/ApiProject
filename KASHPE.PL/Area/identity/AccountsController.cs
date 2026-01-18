using BLL.Services;
using DAL.DTOs.Request;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHPE.PL.Area.identity
{
    [Route("api/auth/[controller]")]
    [ApiController]
    
    public class AccountsController : ControllerBase
    {
        private readonly IAuthanication _authanication;

        public AccountsController(IAuthanication authanication)
        {
            _authanication = authanication;
        }
        [HttpPost("Registration")]
        [AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> Register(RegisterRequest registerRequest) { 
            var Result =await _authanication.RegisterAsync(registerRequest);
            if (!Result.Success) { 
            return BadRequest(Result);
            }
            
            return Ok(Result);
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var Result = await _authanication.LoginAsync(loginRequest);
            if (!Result.Success)
            {
                return BadRequest(Result);
            }

            return Ok(Result);
        }

        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]

        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            var Result = await _authanication.ConfirmEmailAsync(token,userId);
            

            return Ok(Result);
        }

        [HttpPost("SendCode")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest request)
        {
            var Result = await _authanication.RequestPassowrdReset(request);
            if (!Result.Success)
            {
                return BadRequest(Result);
            }
            return Ok(Result);
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(RestPasswordRequest request)
        {
            var Result = await _authanication.PassowrdReset(request);
            if (!Result.Success)
            {
                return BadRequest(Result);
            }
            return Ok(Result);
        }
        [HttpPatch("RefreshToken")]
        public async Task<IActionResult> RefreshToekn(TokenApiModel request)
        {
            var result = await _authanication.RefreshTokenAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }

}
