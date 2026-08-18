using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeYourOwnPizza.Domain.Entities;
namespace MakeYourOwnPizza.Application.Abstractions.Persistence
{
    public interface IAuthRepository
    {
        Task<bool> CreateUserAsync(User user);
        Task<bool> UserExistsAsync(string email);

        Task<User?> GetUserByEmailAsync(string email);

        Task<User?> GetUserByIdAsync(Guid userId);

        Task<bool> UpdateUserStatus(Guid userId, bool isActive);
    }
}
