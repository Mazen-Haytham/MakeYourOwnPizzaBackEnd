using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Application.Users
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UpdateUserRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
