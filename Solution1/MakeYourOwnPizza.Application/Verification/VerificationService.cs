using System;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Application.Abstractions.Authentication;
using MakeYourOwnPizza.Application.Contracts.APIs;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Application.Verification
{
    public class VerificationService : IVerificationService
    {
        private readonly IVerificationRepository _verificationRepository;
        private readonly Lazy<IAuthApi> _authApi;
        private readonly IPasswordHasher _passwordHasher;

        public VerificationService(
            IVerificationRepository verificationRepository, 
            Lazy<IAuthApi> authApi, 
            IPasswordHasher passwordHasher)
        {
            _verificationRepository = verificationRepository;
            _authApi = authApi;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> VerifyEmailAsync(Guid userId, string otp)
        {
            var emailVerification = await _verificationRepository.GetEmailVerificationByUserIdAsync(userId);
            if (emailVerification == null)
            {
                return false;
            }

            if (emailVerification.IsUsed)
            {
                return false;
            }

            if (emailVerification.ExpiresAt < DateTime.UtcNow)
            {
                return false;
            }

            var dummyUser = new User { Id = userId };
            var verifyResult = _passwordHasher.VerifyHashedPassword(dummyUser, emailVerification.OtpHash, otp);
            
            if (verifyResult)
            {
                emailVerification.IsUsed = true;
                await _verificationRepository.UpdateEmailVerificationAsync(emailVerification);
                await _authApi.Value.SetUserStatus(userId, true);
                return true;
            }

            return false;
        }

        public async Task<bool> AddEmailVerificationAsync(Guid userId, string otp)
        {
            var dummyUser = new User { Id = userId };
            var hashed = _passwordHasher.HashPassword(dummyUser, otp);

            var existing = await _verificationRepository.GetEmailVerificationByUserIdAsync(userId);
            if (existing == null)
            {
                var ev = new EmailVerification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    OtpHash = hashed,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                    IsUsed = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _verificationRepository.AddEmailVerificationAsync(ev);
                return true;
            }

            existing.OtpHash = hashed;
            existing.ExpiresAt = DateTime.UtcNow.AddMinutes(15);
            existing.IsUsed = false;
            existing.CreatedAt = DateTime.UtcNow;
            await _verificationRepository.UpdateEmailVerificationAsync(existing);
            return true;
        }
    }
}
