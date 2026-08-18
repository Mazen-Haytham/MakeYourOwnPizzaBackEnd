using System;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Application.Contracts.APIs
{
    public interface IAuthApi
    {
        Task<Guid?> GetUserIdAsync(string email);
        Task<bool> SetUserStatus(Guid userId, bool isActive);
    }
}
