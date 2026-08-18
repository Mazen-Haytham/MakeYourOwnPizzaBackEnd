using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MakeYourOwnPizza.Domain.Entities;
using MakeYourOwnPizza.Application.Abstractions.Persistence;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var res = await _context.Database.ExecuteSqlInterpolatedAsync($"""
            INSERT IGNORE INTO User
            (
                Id,
                firstName,
                lastName,
                email,
                password,
                phone,
                role
            )
            VALUES
            (
                {user.Id},
                {user.firstName},
                {user.lastName},
                {user.email},
                {user.password},
                {user.phone},
                {user.role.ToString()}
            );
            """);
            return res == 1;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.User.AnyAsync(u => u.email == email);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> UpdateUserStatus(Guid userId, bool _isActive)
        {
            User? user = await this.GetUserByIdAsync(userId);

            if (user == null)
                return false;
            user.isActive = _isActive;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
