using DAL.DTOs.Request;
using DAL.DTOs.Response;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class Authanication : IAuthanication
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IConfiguration _configuration;

        public Authanication(UserManager<ApplicationUsers> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            try {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (user == null)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Invalid Email",

                    };
                }
                var Result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);


                if (!Result) {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Error Password",

                    };
                }
                return new LoginResponse()
                {
                    Success = true,
                    Message = "Sucess Login",
                    AccessToken = await GenerateToke(user)

                };
            }
            catch (Exception ex) {

                return new LoginResponse()
                {
                    Success = false,
                    Message = "an unExpected error",
                    error = new List<string>() { ex.Message },
                    



                }; 
            }
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                var user = registerRequest.Adapt<ApplicationUsers>();
                var result = await _userManager.CreateAsync(user, registerRequest.Password);
                if (!result.Succeeded)
                {

                    return new RegisterResponse()
                    {
                        Success = false,
                        Message = "User Creation filed",
                        error = result.Errors.Select(e => e.Description).ToList()

                    };
                }
                await _userManager.AddToRoleAsync(user, "User");
                return new RegisterResponse()
                {
                    Success = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponse()
                {
                    Success = false,
                    Message = "an unExpected error",
                    error = new List<string>() { ex.Message }
                };
            }
        }
    

    private async Task<string> GenerateToke(ApplicationUsers user)
        {
            var userClime = new List<Claim>() { 
            
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Name,user.FullName)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClime,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
