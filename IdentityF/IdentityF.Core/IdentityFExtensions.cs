using IdentityF.Core.Features.Shared.Email;
using IdentityF.Core.Features.Shared.Managers;
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

            services.AddScoped<IEndpointHandler, InitEndpointHandler>();
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
