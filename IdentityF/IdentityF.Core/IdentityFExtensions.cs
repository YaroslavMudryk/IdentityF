using IdentityF.Core.ErrorHandling;
using IdentityF.Core.Features.Sessions;
using IdentityF.Core.Features.SignIn;
using IdentityF.Core.Features.SignUp;
using IdentityF.Core.Managers;
using IdentityF.Core.Middlewares;
using IdentityF.Core.Options;
using IdentityF.Core.Services.Auth;
using IdentityF.Core.Services.Db;
using IdentityF.Core.Services.Email;
using IdentityF.Core.Services.Location;
using IdentityF.Core.Services.Sms;
using IdentityF.Data;
using IdentityF.Data.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YaMu.Helpers;

namespace IdentityF
{
    public static class IdentityFExtensions
    {
        public static IServiceCollection AddIdentityFServices(this IServiceCollection services, DatabaseProviders provider, Action<IdentityFOptions> optionsAction = null)
        {
            var identityOptions = new IdentityFOptions();
            if (optionsAction != null)
                optionsAction(identityOptions);

            SmsDependencies.Register(services);
            EmailDependencies.Register(services);
            ManagersDependencies.Register(services);
            SignUpDependencies.Register(services);
            AuthDependencies.Register(services);
            SignInDependencies.Register(services);
            LocationDependencies.Register(services);
            SessionsDependencies.Register(services);

            services.AddHttpContextAccessor();

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

            var connectionString = identityOptions.ConnectionString;

            services.AddDbContext<IdentityFContext>(options =>
            {
                if (provider == DatabaseProviders.Sqlite)
                {
                    options.UseSqlite(connectionString);
                }
                if (provider == DatabaseProviders.SqlServer)
                {
                    options.UseSqlServer(connectionString);
                }
                if (provider == DatabaseProviders.Postgres)
                {
                    options.UseNpgsql(connectionString);
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                }
                if (provider == DatabaseProviders.MySql)
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                }
            });
            services.AddScoped<IDatabaseService, IdentityDatabaseService>();
            services.AddDateTimeProvider();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(jwt =>
                        {
                            jwt.RequireHttpsMetadata = false;
                            jwt.SaveToken = true;
                            jwt.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidIssuer = identityOptions.Token.Issuer,
                                ValidateAudience = false,
                                ValidateLifetime = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(identityOptions.Token.SecretKey)),
                                ValidateIssuerSigningKey = true
                            };
                        });

            services.Configure(optionsAction);
            return services;
        }


        public static void UseIdentityF(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandlerMiddleware>();
            builder.UseMiddleware<IdentityFMiddleware>();
        }
    }
}
