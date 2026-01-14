 using DAL.DTOs.Request;
using DAL.DTOs.Response;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUsers> _signInManager;

        public Authanication(UserManager<ApplicationUsers> userManager, IConfiguration configuration,

            IEmailSender emailSender, SignInManager<ApplicationUsers> signInManager
            )


        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (user == null)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Invalid Email",

                    };
                }
                else if (await _userManager.IsLockedOutAsync(user))
                {

                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Account is Locked tray agin later",

                    };
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);

                if (result.IsLockedOut)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Account is Locked duto multiple attemp",

                    };

                }
                else if (result.IsNotAllowed)
                {

                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "plz confirm your email",

                    };
                }
                else if (!result.Succeeded)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Invalid Password"

                    };
                }

                return new LoginResponse()
                {
                    Success = true,
                    Message = "Sucess Login",
                    AccessToken = await GenerateToke(user)

                };
            }
            catch (Exception ex)
            {

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
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = Uri.EscapeDataString(token);
                var emailURL = $"https://localhost:7229/api/auth/Accounts/ConfirmEmail?token={token}&userId={user.Id}";
                await _emailSender.SendEmailAsync(user.Email, "Welcome to KASHPE",
                    $"<h1> Hello {user.FullName} </h1><p> Thank you for registering at KASHPE. We're excited to have you on board!</p>" +
                    $"<a href='{emailURL}'>confirm email</a>");
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
        public async Task<bool> ConfirmEmailAsync(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            var Result = await _userManager.ConfirmEmailAsync(user, token);
            if (Result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GenerateToke(ApplicationUsers user)
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


        public async Task<ForgetPasswordResponse> RequestPassowrdReset(ForgetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ForgetPasswordResponse()
                {

                    Success = false,
                    Message = "Invalid Email",

                };

            }
            var random = new Random();
            var code = random.Next(1000, 9999).ToString();
            user.PasswordResetCode = code;
            user.PasswodResretCodeExpirty = DateTime.Now.AddMinutes(15);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(user.Email, "Password Reset Code",
                $"<h1> Hello {user.FullName} </h1><p> Your password reset code is: <strong>{code}</strong></p>" +
                $"<p>This code will expire in 15 minutes.</p>");
            return new ForgetPasswordResponse
            {

                Success = true,
                Message = "Password reset code sent to your email."
            };
        }

        public async Task<ResetPasswordResponse>PassowrdReset(RestPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ResetPasswordResponse()
                {

                    Success = false,
                    Message = "Invalid Email",

                };

            }
            if (user.PasswordResetCode != request.ResetCode)
            {
                return new ResetPasswordResponse()
                {

                    Success = false,
                    Message = "Code does not match",

                };

            }
            if (user.PasswodResretCodeExpirty < DateTime.UtcNow)
            {
                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message = "Code has expired",
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (!result.Succeeded)
            {
                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message = "Password reset failed",
                    error = result.Errors.Select(e => e.Description).ToList()
                };
            }
            await _emailSender.SendEmailAsync(user.Email, "Password Reset Successful",
                $"<h1> Hello {user.FullName} </h1><p> Your password has been successfully reset.</p>");

            return new ResetPasswordResponse
            {

                Success = true,
                Message = "Password Reset Sucssefully."
            };
        }

    }
}

