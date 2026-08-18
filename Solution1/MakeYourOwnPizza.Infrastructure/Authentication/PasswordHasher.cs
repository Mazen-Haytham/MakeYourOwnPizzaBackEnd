using Microsoft.AspNetCore.Identity;
using MakeYourOwnPizza.Domain.Entities;
using MakeYourOwnPizza.Application.Abstractions.Authentication;

namespace MakeYourOwnPizza.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordHasher(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
