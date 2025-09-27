using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.DTO.Requests;
using Ecommerce.DAL.DTO.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest registerRequest)
        {
           var result =  await authenticationService.RegisterAsync(registerRequest);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest loginRequest)
        {
           var result = await authenticationService.LoginAsync(loginRequest);
            return Ok(result);
        }


        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery]string token,[FromQuery]string userId)
        {
           var result = await authenticationService.ConfirmEmail(token,userId);
            return Ok(result);
        }

        [HttpPost("forget-password")]
        public async Task<ActionResult<string>> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
          var result=  await authenticationService.ForgetPassword(request);
            return Ok(result);
        }


        [HttpPatch("reset-password")]
        public async Task<ActionResult<string>> ResetPassword(ResetPasswordRequest request)
        {
            var result = await authenticationService.ResetPassword(request);
            return Ok(result);
        }

    }
}
