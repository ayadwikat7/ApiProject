using DAL.DTOs.Request;
using DAL.DTOs.Response;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MangeUserService : IMangeUserService
    {
        private readonly UserManager<ApplicationUsers> _userManager;

        public MangeUserService(UserManager<ApplicationUsers> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseResponse> BlockedUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            await _userManager.UpdateAsync(user);
            return new BaseResponse()
            {
                Success = true,
                Message = "User blocked successfully"
            };
        }

        public async Task<BaseResponse> ChangeUserRoleAsync(ChangeRoleUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, request.Role);
            return new BaseResponse()
            {
                Success = true,
                Message = "User role changed successfully"
            };
        }

        public async Task<List<UserListResponse>> GetUserAsync()
        {
            var users= await _userManager.Users.ToListAsync();
            var result =users.Adapt<List<UserListResponse>>();
            for(int i=0;i<users.Count;i++)
            {
                var roles = await _userManager.GetRolesAsync(users[i]);
                result[i].Roles = roles.ToList();
            }
            return result;
        }

        public Task<UserDetaliesResponse> GetUserDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> UnBlockedUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            await _userManager.SetLockoutEnabledAsync(user, false);
            await _userManager.SetLockoutEndDateAsync(user,null);
            await _userManager.UpdateAsync(user);
            return new BaseResponse()
            {
                Success = true,
                Message = "User blocked successfully"
            };
        }
    }
}
