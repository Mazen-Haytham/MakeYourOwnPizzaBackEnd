using System.ComponentModel.DataAnnotations;

namespace MakeYourOwnPizza.Application.Auth.Login
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;

        [Required]
        public string password { get; set; } = string.Empty;
    }
}
