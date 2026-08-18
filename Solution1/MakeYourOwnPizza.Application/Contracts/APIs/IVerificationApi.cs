using System;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Application.Contracts.APIs
{
    public interface IVerificationApi
    {
        Task<bool> AddEmailVerificationAsync(Guid userId, string otp);
    }
}
