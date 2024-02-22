using IdentityF.Core.Features.Mfa.Services;
using IdentityF.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.Mfa
{
    public static class MfaDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IMfaService, MfaService>();
            services.AddScoped<IEndpointHandler, TurnOnMfaEndpointHandler>();
            services.AddScoped<IEndpointHandler, TurnOffMfaEndpointHandler>();
        }
    }
}
