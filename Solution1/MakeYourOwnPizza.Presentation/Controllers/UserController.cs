using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using MakeYourOwnPizza.Application.Users;

namespace MakeYourOwnPizza.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPut("me")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim)) return Unauthorized();
            if (!Guid.TryParse(idClaim, out var userId)) return Unauthorized();

            var updated = await _userService.UpdateAsync(userId, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim)) return Unauthorized();
            if (!Guid.TryParse(idClaim, out var userId)) return Unauthorized();

            var u = await _userService.GetByIdAsync(userId);
            if (u == null) return NotFound();
            
            return Ok(u);
        }

        [HttpDelete("me")]
        public async Task<IActionResult> DeleteMe()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim)) return Unauthorized();
            if (!Guid.TryParse(idClaim, out var userId)) return Unauthorized();

            var deleted = await _userService.DeleteAsync(userId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
