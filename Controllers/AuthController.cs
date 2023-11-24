using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoAPP_API.Auth;
using ToDoAPP_API.Models.Entities;
using ToDoAPP_API.Models.Requests;

namespace ToDoAPP_API.Controllers
{
    [ApiController]
    [Route("auth/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly TokenGenerator _tokenGenerator;
        public AuthController(UserManager<UserEntity> userManager, TokenGenerator tokenGenerator)
        {
            _userManager = userManager; 
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserRequest request)
        {
            var entity = new UserEntity();
            entity.Email = request.Email;
            entity.UserName = request.Email;
            var result = await _userManager.CreateAsync(entity, request.Password);
            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                return BadRequest(firstError.Description);
            }
            return Ok();
        }
        [HttpPost("login")]
        public  async Task<IActionResult> LoginUser([FromBody]LogInUserRequest request)
        {
            
            var user = await _userManager.FindByEmailAsync(request.Email);
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {

                return BadRequest("Email or Password is incorrect");
            }
            else return Ok(_tokenGenerator.Generate(user.Id.ToString()));
        }
        [HttpPost("request-reset-password")]
        public async Task<IActionResult> RequestResetPassword([FromBody] GeneratePasswordResetTokenRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return StatusCode(404, "User not found");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return Ok(token);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                return StatusCode(404, "User not found");
            }
            var result = await _userManager.ResetPasswordAsync(user, request.ResetPasswordToken, request.Password);
            if (!result.Succeeded)
            {
               var firstError = result.Errors.First();
               return StatusCode(404, firstError.Description);
            }
            return Ok();
        }


    }
}
