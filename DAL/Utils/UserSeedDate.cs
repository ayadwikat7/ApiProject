using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Utils
{
    public class UserSeedDate : ISeedData
    {
        private readonly UserManager<ApplicationUsers> _userManager;

        public UserSeedDate(UserManager<ApplicationUsers> userManager)
        {
            _userManager = userManager;
        }
        public async Task DataSeed()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUsers
                {
                    UserName = "Aya",
                    FullName = "Aya Dwikat",
                    Email = "Aya@gmial.com",
                    City="Amman",
                    Street = "Sofyan",
                    EmailConfirmed = true,
                };
                var user2 = new ApplicationUsers
                {
                    UserName = "Tala",
                    FullName = "Tala Dwikat",
                    Email = "Tala@gmial.com",
                    City = "Amman",
                    Street="Sofyan",
                    EmailConfirmed = true,
                };
                var user3 = new ApplicationUsers
                {
                    UserName = "Osama",
                    FullName = "Osama Dwikat",
                    Email = "Osama@gmial.com",
                    City = "Amman",
                    Street = "Sofyan",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(user1, "Aa@12345678");
                await _userManager.CreateAsync(user2, "Aa@12355555");
                await _userManager.CreateAsync(user3, "AAA55@4567a");

                await _userManager.AddToRoleAsync(user3, "SuperAdmain");
                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "User");

            }
        }
    }
}
