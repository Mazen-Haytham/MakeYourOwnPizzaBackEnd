using System;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Application.Abstractions.Authentication
{
    public interface IPasswordHasher
    {
        string HashPassword(User user, string password);
        bool VerifyHashedPassword(User user, string hashedPassword, string providedPassword);
    }
}
