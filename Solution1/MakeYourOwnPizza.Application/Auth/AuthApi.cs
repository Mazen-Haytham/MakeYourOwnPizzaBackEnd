using System;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Contracts.APIs;

namespace MakeYourOwnPizza.Application.Auth
{
    public class AuthApi : IAuthApi
    {
        private readonly IAuthService _authService;

        public AuthApi(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Guid?> GetUserIdAsync(string email)
        {
            return await _authService.CheckEmailExistsAsync(email);
        }

        public async Task<bool> SetUserStatus(Guid userId, bool isActive)
        {
            return await _authService.ActivateUser(userId, isActive);
        }
    }
}
