using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;

namespace MakeYourOwnPizza.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponse
            {
                Id = u.Id,
                firstName = u.firstName,
                lastName = u.lastName,
                email = u.email,
                phone = u.phone,
                role = u.role,
                isActive = u.isActive,
                isDeleted = u.isDeleted
            }).ToList();
        }

        public async Task<UserResponse?> GetByIdAsync(Guid id)
        {
            var u = await _userRepository.GetByIdAsync(id);
            if (u == null) return null;

            return new UserResponse
            {
                Id = u.Id,
                firstName = u.firstName,
                lastName = u.lastName,
                email = u.email,
                phone = u.phone,
                role = u.role,
                isActive = u.isActive,
                isDeleted = u.isDeleted
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUserRequest request)
        {
            return await _userRepository.UpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
