using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Auth;
using MakeYourOwnPizza.Application.Auth.Register;
using MakeYourOwnPizza.Application.Auth.Login;

namespace MakeYourOwnPizza.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _authService.RegisterUserAsync(request);
                if (result != RegisterResult.Success)
                {
                    return BadRequest(new { message = result.ToString() });
                }
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _authService.LoginUserAsync(request);
                return Ok(new { accessToken = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] Guid userId)
        {
            try
            {
                await _authService.LogoutUserAsync(userId);
                return Ok(new { message = "User logged out successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
