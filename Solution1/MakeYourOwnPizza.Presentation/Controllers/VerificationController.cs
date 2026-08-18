using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Verification;
using MakeYourOwnPizza.Application.Contracts.APIs;

namespace MakeYourOwnPizza.Presentation.Controllers
{
    [Route("api/Verify")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly IVerificationService _verifyService;
        private readonly IAuthApi _authApi;

        public VerificationController(IVerificationService verifyService, IAuthApi authApi)
        {
            _verifyService = verifyService;
            _authApi = authApi;
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyEmailRequest request)
        {
            Guid? userId = await _authApi.GetUserIdAsync(request.email);
            if (userId == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var result = await _verifyService.VerifyEmailAsync(userId.Value, request.otp);
            if (result)
            {
                return Ok(new { message = "Email verified successfully" });
            }

            return BadRequest(new { message = "Invalid OTP" });
        }
    }
}
