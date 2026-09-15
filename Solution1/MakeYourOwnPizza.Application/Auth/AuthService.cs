using System;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Auth.Register;
using MakeYourOwnPizza.Application.Auth.Login;
using MakeYourOwnPizza.Application.Abstractions.Authentication;
using MakeYourOwnPizza.Application.Abstractions.Communication;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Application.Abstractions.Services;
using MakeYourOwnPizza.Application.Contracts.APIs;
using MakeYourOwnPizza.Domain.Entities;
using MakeYourOwnPizza.Domain.Enums;

namespace MakeYourOwnPizza.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IOtpService _otpService;
        private readonly IVerificationApi _verificationApi;
        private readonly IEmailService _emailService;

        public AuthService(
            IPasswordHasher passwordHasher,
            IAuthRepository authRepository,
            ITokenService tokenService,
            IOtpService otpService,
            IVerificationApi verificationApi,
            IEmailService emailService)
        {
            _passwordHasher = passwordHasher;
            _authRepository = authRepository;
            _tokenService = tokenService;
            _otpService = otpService;
            _verificationApi = verificationApi;
            _emailService = emailService;
        }

        public async Task<RegisterResult> RegisterUserAsync(RegisterRequest request)
        {
            if (request.role != Role.Customer && request.role != Role.Delivery)
            {
                return RegisterResult.InvalidRole;
            }

            bool emailExists = await _authRepository.UserExistsAsync(request.email);
            if (emailExists)
            {
                return RegisterResult.EmailAlreadyExists;
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                firstName = request.firstName,
                lastName = request.lastName,
                email = request.email,
                phone = request.phone,
                isActive = false,
                isDeleted = false,
                role = request.role
            };

            user.password = _passwordHasher.HashPassword(user, request.password);

            bool created = await _authRepository.CreateUserAsync(user);
            if (!created)
            {
                return RegisterResult.EmailAlreadyExists;
            }

            try
            {
                var otp = _otpService.GenerateOtp();
                if (_verificationApi != null)
                {
                    await _verificationApi.AddEmailVerificationAsync(user.Id, otp);
                    await _emailService.SendVerificationEmailAsync(user.email, otp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Verification error: {ex.Message}");
                throw;
            }

            return RegisterResult.Success;
        }

        public async Task<string?> LoginUserAsync(LoginRequest request)
        {
            var user = await _authRepository.GetUserByEmailAsync(request.email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            bool isPasswordValid = _passwordHasher.VerifyHashedPassword(user, user.password, request.password);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var token = _tokenService.GenerateToken(user);
            return token;
        }

        public Task LogoutUserAsync(Guid userId)
        {
            return Task.CompletedTask;
        }

        public async Task<Guid?> CheckEmailExistsAsync(string email)
        {
            var user = await _authRepository.GetUserByEmailAsync(email);
            return user?.Id;
        }

        public async Task<bool> ActivateUser(Guid userId, bool isActive)
        {
            return await _authRepository.UpdateUserStatus(userId, isActive);
        }
    }
}
