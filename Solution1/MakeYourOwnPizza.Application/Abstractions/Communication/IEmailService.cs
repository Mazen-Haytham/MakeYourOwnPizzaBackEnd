using System;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Application.Abstractions.Communication
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string email, string otp);
    }
}
