using System;
using System.Threading.Tasks;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Application.Abstractions.Persistence
{
    public interface IVerificationRepository
    {
        Task<EmailVerification?> GetEmailVerificationByUserIdAsync(Guid userId);
        Task AddEmailVerificationAsync(EmailVerification emailVerification);
        Task UpdateEmailVerificationAsync(EmailVerification emailVerification);
    }
}
