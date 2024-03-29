﻿using IdentityF.Core.ErrorHandling;
using IdentityF.Core.Features.Confirm;
using IdentityF.Core.Features.Devices;
using IdentityF.Core.Features.Mfa;
using IdentityF.Core.Features.RefreshToken;
using IdentityF.Core.Features.Sessions;
using IdentityF.Core.Features.SignIn;
using IdentityF.Core.Features.SignOut;
using IdentityF.Core.Features.SignUp;
using IdentityF.Core.Managers;
using IdentityF.Core.Middlewares;
using IdentityF.Core.Options;
using IdentityF.Core.Services.Auth;
using IdentityF.Core.Services.ChangeHistory;
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
        public static IServiceCollection AddIdentityFServices(this IServiceCollection services)
        {
            return services.AddIdentityFCore(new IdentityFOptions());
        }

        public static IServiceCollection AddIdentityFServices(this IServiceCollection services, Action<IdentityFOptions> optionsAction)
        {
            var identityOptions = new IdentityFOptions();
            if (optionsAction != null)
            {
                services.Configure(optionsAction);
                optionsAction(identityOptions);
            }
            return services.AddIdentityFCore(identityOptions);
        }

        private static IServiceCollection AddIdentityFCore(this IServiceCollection services, IdentityFOptions identityOptions)
        {
            SmsDependencies.Register(services);
            EmailDependencies.Register(services);
            ManagersDependencies.Register(services);
            SignUpDependencies.Register(services);
            AuthDependencies.Register(services);
            SignInDependencies.Register(services);
            LocationDependencies.Register(services);
            SessionsDependencies.Register(services);
            ConfirmDependencies.Register(services);
            RefreshTokenDependencies.Register(services);
            SignOutDependencies.Register(services);
            MfaDependencies.Register(services);
            ChangeHistoryDependencies.Register(services);
            DevicesDependencies.Register(services);

            services.AddScoped<IDatabaseService, IdentityDatabaseService>();

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

            services.AddDateTimeProvider();
            services.AddHttpContextAccessor();
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

            var db = identityOptions.DbConnection;
            services.AddDbContext<IdentityFContext>(options =>
            {
                if (db.DatabaseProvider == DatabaseProviders.Sqlite)
                {
                    options.UseSqlite(db.ConnectionString);
                }
                if (db.DatabaseProvider == DatabaseProviders.SqlServer)
                {
                    options.UseSqlServer(db.ConnectionString);
                }
                if (db.DatabaseProvider == DatabaseProviders.Postgres)
                {
                    options.UseNpgsql(db.ConnectionString);
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                }
                if (db.DatabaseProvider == DatabaseProviders.MySql)
                {
                    options.UseMySql(db.ConnectionString, ServerVersion.AutoDetect(db.ConnectionString));
                }
            });
            return services;
        }


        public static void UseIdentityF(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandlerMiddleware>();
            builder.UseMiddleware<IdentityFMiddleware>();
        }
    }
}
