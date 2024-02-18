using IdentityF.Core.Features.RefreshToken.Services;
using IdentityF.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.RefreshToken
{
    public static class RefreshTokenDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IEndpointHandler, RefreshTokenEndpointHandler>();
        }
    }
}
