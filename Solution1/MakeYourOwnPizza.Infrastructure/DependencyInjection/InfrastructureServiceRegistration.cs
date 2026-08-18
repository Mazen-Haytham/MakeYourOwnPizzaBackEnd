using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MakeYourOwnPizza.Domain.Entities;
using MakeYourOwnPizza.Application.Abstractions.Authentication;
using MakeYourOwnPizza.Application.Abstractions.Communication;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Application.Abstractions.Services;
using MakeYourOwnPizza.Infrastructure.Authentication;
using MakeYourOwnPizza.Infrastructure.Email;
using MakeYourOwnPizza.Infrastructure.Persistence;
using MakeYourOwnPizza.Infrastructure.Persistence.Repositories;
using MakeYourOwnPizza.Infrastructure.Services;

namespace MakeYourOwnPizza.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVerificationRepository, VerificationRepository>();

            services.AddScoped<IPasswordHasher<User>, Microsoft.AspNetCore.Identity.PasswordHasher<User>>();
            services.AddScoped<IPasswordHasher, Authentication.PasswordHasher>();
            services.AddScoped<ITokenService, JwtTokenService>();
            
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOtpService, OtpService>();

            return services;
        }
    }
}
