using System;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Application.Verification
{
    public interface IVerificationService
    {
        Task<bool> VerifyEmailAsync(Guid userId, string otp);
        Task<bool> AddEmailVerificationAsync(Guid userId, string otp);
    }
}
