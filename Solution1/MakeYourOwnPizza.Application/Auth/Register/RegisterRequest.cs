using System.ComponentModel.DataAnnotations;
using MakeYourOwnPizza.Domain.Enums;

namespace MakeYourOwnPizza.Application.Auth.Register
{
    public class RegisterRequest
    {
        [Required]
        public string firstName { get; set; } = string.Empty;
        [Required]
        public string lastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
        [Required]
        [Compare("password")]
        public string confirmPassword { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string phone { get; set; } = string.Empty;
        [Required]
        public Role role { get; set; }
    }
}
