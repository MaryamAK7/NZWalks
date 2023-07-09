using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager) {
            _userManager = userManager;
        }
        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register (RegisterRequest registerRequest)
        {
            var identityUser = new IdentityUser {
                UserName = registerRequest.Username,
                Email = registerRequest.Username
            };
         var identityResult = await  _userManager.CreateAsync(identityUser, registerRequest.Password);
            if(identityResult.Succeeded)
            {
                if(registerRequest.Roles != null && registerRequest.Roles.Any()) {
                identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequest.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("Registered successfully please sign in!");
                    }
                }
            }
            return BadRequest("Something went wrong.");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LogIn ([FromBody] LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Username);
            if(user != null)
            {
               var response = await _userManager.CheckPasswordAsync(user,loginRequest.Password);
                if (response)
                {
                    //Create Token
                    return Ok();
                }
            }
            return BadRequest("Usernam or Password is incorrect");
        }
    }
}
