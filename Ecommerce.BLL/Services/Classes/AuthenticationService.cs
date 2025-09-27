using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.DTO.Requests;
using Ecommerce.DAL.DTO.Responses;
using Ecommerce.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IEmailSender emailSender;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IConfiguration configuration,IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.emailSender = emailSender;
            this.configuration = configuration;
        }

        

        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if(user is null)
            {
                throw new Exception("Invalid Email or Password");
            }

            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception("Please Confirm Your Email");
            }


            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid Email or Password");
            }

            return new UserResponse()
            {
                Token = await CreateTokenAsync(user)
            };
        }


        public async Task<string> ConfirmEmail(string token,string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("User Not Found");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if(result.Succeeded) return "Email Confirmed Successfully";
            else
                throw new Exception("Email Confirmation Failed");
        }
        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            var user = new ApplicationUser()
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
                PhoneNumber = registerRequest.PhoneNumber
            };
            var result = await userManager.CreateAsync(user,registerRequest.Password);
            if(result.Succeeded)
            {

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken=Uri.EscapeDataString(token);
                var emailUrl = $"https://localhost:7290/api/Identity/Account/ConfirmEmail?token={escapeToken}&userId={user.Id}";

                emailSender.SendEmailAsync(user.Email,"Welcome",$"<h1> Hello {user.UserName}</h1>"+
                    $"<a href='{emailUrl}'>confirm</a>");
                return new UserResponse()
                {
                    Token = registerRequest.Email,
                };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
            };
            
            var Roles = await userManager.GetRolesAsync(user);
            foreach (var role in Roles )
            {
                Claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("jwtOptions")["SecretKey"]));
            var credintials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims:Claims,
                expires:DateTime.Now.AddDays(15),
                signingCredentials:credintials);
                
            
               return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<string> ForgetPassword(ForgetPasswordRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("User Not Found");
            }
            var code =new Random().Next(1000, 9999).ToString();
            user.CodeResetPassword= code;
            user.PasswordResetCodeExpiry=DateTime.Now.AddMinutes(15);

            await userManager.UpdateAsync(user);

            await emailSender.SendEmailAsync(request.Email,"Reset password",$"<P>code is {code}</p>");

            return "Check Your Email";
        }


        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if(user is null) throw new Exception("User Not Found");

            if (user.CodeResetPassword != request.Code) return false;
            if (user.PasswordResetCodeExpiry < DateTime.Now) return false;

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var result = await userManager.ResetPasswordAsync(user,token,request.NewPassword);

            if (result.Succeeded)
            {
                await emailSender.SendEmailAsync(request.Email,"Password Reset", $"<h1>Your password has been reset</h1>");
            }
            return true;
        }
    }
}
