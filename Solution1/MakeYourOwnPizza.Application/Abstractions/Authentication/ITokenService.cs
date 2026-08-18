using System;

namespace MakeYourOwnPizza.Application.Abstractions.Authentication
{
    public interface ITokenService
    {
        string GenerateToken(Domain.Entities.User user);
    }
}
