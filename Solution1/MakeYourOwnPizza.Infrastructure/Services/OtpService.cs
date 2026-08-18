using System.Security.Cryptography;
using System.Text;
using MakeYourOwnPizza.Application.Abstractions.Services;

namespace MakeYourOwnPizza.Infrastructure.Services
{
    public class OtpService : IOtpService
    {
        public string GenerateOtp(int length = 6)
        {
            if (length <= 0) length = 6;
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int digit = RandomNumberGenerator.GetInt32(0, 10);
                sb.Append(digit);
            }
            return sb.ToString();
        }
    }
}
