using BLL.Services;
using DAL.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHPE.PL.Area.identity
{
    [Route("api/auth/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthanication _authanication;

        public AccountsController(IAuthanication authanication)
        {
            _authanication = authanication;
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest) { 
            var Result =await _authanication.RegisterAsync(registerRequest);
            if (!Result.Success) { 
            return BadRequest(Result);
            }
            
            return Ok(Result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var Result = await _authanication.LoginAsync(loginRequest);
            if (!Result.Success)
            {
                return BadRequest(Result);
            }

            return Ok(Result);
        }

    }

}
