using BLL.Services;
using DAL.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHPE.PL.Area.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ManagesController : ControllerBase
    {
        private readonly IMangeUserService _mangeUser;

        public ManagesController(IMangeUserService mangeUser)
        {
            _mangeUser = mangeUser;
        }
        [HttpGet("Usres")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mangeUser.GetUserAsync();
            return Ok(users);
        }
        [HttpPatch("BlockUser/{userId}")]
        public async Task<IActionResult> BlockUser(string userId)
        {
            var result = await _mangeUser.BlockedUserAsync(userId);
            return Ok(result);
        }
        [HttpPatch("UnBlockUser/{userId}")]
        public async Task<IActionResult> UnBlockUser(string userId)
        {
            var result = await _mangeUser.UnBlockedUserAsync(userId);
            return Ok(result);
        }
        [HttpPatch("change-role")]
        [Authorize(Roles = "SuperAdmain")]
        public async Task<IActionResult> ChangeRole(ChangeRoleUserRequest request) =>
            Ok(await _mangeUser.ChangeUserRoleAsync(request));
    }
    }
