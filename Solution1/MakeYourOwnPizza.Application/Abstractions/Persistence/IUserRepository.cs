using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MakeYourOwnPizza.Domain.Entities;
using MakeYourOwnPizza.Application.Users;

namespace MakeYourOwnPizza.Application.Abstractions.Persistence
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UpdateUserRequest dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
