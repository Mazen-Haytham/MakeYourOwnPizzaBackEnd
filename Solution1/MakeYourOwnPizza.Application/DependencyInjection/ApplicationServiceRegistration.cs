using Microsoft.Extensions.DependencyInjection;
using MakeYourOwnPizza.Application.Auth;
using MakeYourOwnPizza.Application.Users;
using MakeYourOwnPizza.Application.Orders;
using MakeYourOwnPizza.Application.Verification;
using MakeYourOwnPizza.Application.Contracts.APIs;

namespace MakeYourOwnPizza.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IVerificationService, VerificationService>();

            services.AddScoped<IAuthApi, AuthApi>();
            services.AddScoped<IVerificationApi, VerificationApi>();

            // Register Lazy<IAuthApi> to break circular dependency:
            // AuthService -> IVerificationApi -> VerificationService -> Lazy<IAuthApi>
            // The Lazy wrapper defers resolution until first use, breaking the constructor cycle.
            services.AddScoped<Lazy<IAuthApi>>(sp => new Lazy<IAuthApi>(() => sp.GetRequiredService<IAuthApi>()));

            return services;
        }
    }
}
