using System;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Auth.Register;
using MakeYourOwnPizza.Application.Auth.Login;

namespace MakeYourOwnPizza.Application.Auth
{
    public interface IAuthService
    {
        Task<RegisterResult> RegisterUserAsync(RegisterRequest request);
        Task<string?> LoginUserAsync(LoginRequest request);
        Task<Guid?> CheckEmailExistsAsync(string email);
        Task<bool> ActivateUser(Guid userId, bool isActive);
        Task LogoutUserAsync(Guid userId);
    }
}
