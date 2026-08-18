using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MakeYourOwnPizza.Domain.Entities;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Application.Users;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUserRequest dto)
        {
            var existing = await _context.User.FindAsync(id);
            if (existing == null) return false;

            if (!string.IsNullOrEmpty(dto.firstName)) existing.firstName = dto.firstName;
            if (!string.IsNullOrEmpty(dto.lastName)) existing.lastName = dto.lastName;
            if (!string.IsNullOrEmpty(dto.phone)) existing.phone = dto.phone;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.User.FindAsync(id);
            if (existing == null) return false;
            
            existing.isDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
