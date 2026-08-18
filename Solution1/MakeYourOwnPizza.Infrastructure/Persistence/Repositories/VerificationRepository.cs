using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MakeYourOwnPizza.Domain.Entities;
using MakeYourOwnPizza.Application.Abstractions.Persistence;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Repositories
{
    public class VerificationRepository : IVerificationRepository
    {
        private readonly AppDbContext _context;

        public VerificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmailVerification?> GetEmailVerificationByUserIdAsync(Guid userId)
        {
            return await _context.EmailVerification.FirstOrDefaultAsync(ev => ev.UserId == userId);
        }

        public async Task AddEmailVerificationAsync(EmailVerification emailVerification)
        {
            await _context.EmailVerification.AddAsync(emailVerification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmailVerificationAsync(EmailVerification emailVerification)
        {
            _context.EmailVerification.Update(emailVerification);
            await _context.SaveChangesAsync();
        }
    }
}
