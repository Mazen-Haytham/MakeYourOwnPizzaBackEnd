using System.ComponentModel.DataAnnotations;

namespace MakeYourOwnPizza.Application.Verification
{
    public class VerifyEmailRequest
    {
        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;
        
        [Required]
        public string otp { get; set; } = string.Empty;
    }
}
