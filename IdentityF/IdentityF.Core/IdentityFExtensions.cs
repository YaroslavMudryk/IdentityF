using IdentityF.Core.Features.Shared.Email;
using IdentityF.Core.Features.Shared.Managers;
using IdentityF.Core.Features.Shared.Sessions.Services;
using IdentityF.Core.Features.Shared.Sms;
using IdentityF.Core.Features.SignUp;
using IdentityF.Core.Handlers;
using IdentityF.Core.Middlewares;
using IdentityF.Core.Options;
using IdentityF.Core.Seeder;
using IdentityF.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YaMu.Helpers;

namespace IdentityF
{
    public static class IdentityFExtensions
    {
        public static IServiceCollection AddIdentityFServices(this IServiceCollection services, Action<IdentityFOptions> optionsAction = null)
        {
            var identityOptions = new IdentityFOptions();
            if (optionsAction != null)
                optionsAction(identityOptions);

            SmsDependencies.Register(services);
            EmailDependencies.Register(services);
            ManagersDependencies.Register(services);
            SignUpDependencies.Register(services);

            var sessionManager = identityOptions.Token.SessionManager;
            if (sessionManager.Implementation != null && typeof(ISessionManager).IsAssignableFrom(sessionManager.Implementation))
            {
                if (sessionManager.Lifetime == ServiceLifetime.Transient)
                    services.AddTransient(typeof(ISessionManager), sessionManager.Implementation);
                if (sessionManager.Lifetime == ServiceLifetime.Scoped)
                    services.AddScoped(typeof(ISessionManager), sessionManager.Implementation);
                if (sessionManager.Lifetime == ServiceLifetime.Singleton)
                    services.AddSingleton(typeof(ISessionManager), sessionManager.Implementation);
            }
            else
            {
                services.AddSingleton<ISessionManager, InMemorySessionManager>();
            }

            services.AddScoped<IEndpointHandler, SignUpEndpointHandler>();
            services.AddDbContext<IdentityFContext>(o =>
            {
                o.UseSqlite(identityOptions.ConnectionString);
            });
            services.AddScoped<IDatabaseService, IdentityDatabaseService>();
            services.AddDateTimeProvider();
            return services;
        }


        public static void UseIdentityF(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<IdentityFMiddleware>();
        }
    }
}
