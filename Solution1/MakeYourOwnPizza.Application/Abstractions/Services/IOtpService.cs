using System;

namespace MakeYourOwnPizza.Application.Abstractions.Services
{
    public interface IOtpService
    {
        string GenerateOtp(int length = 6);
    }
}
