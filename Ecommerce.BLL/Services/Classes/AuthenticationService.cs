using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.DTO.Requests;
using Ecommerce.DAL.DTO.Responses;
using Ecommerce.DAL.Models;
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

namespace Ecommerce.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.configuration = configuration;
        }

        

        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if(user is null)
            {
                throw new Exception("Invalid Email or Password");
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
    }
}
