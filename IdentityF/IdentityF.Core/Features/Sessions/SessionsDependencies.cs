using IdentityF.Core.Features.Sessions.Services;
using IdentityF.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.Sessions
{
    public static class SessionsDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IEndpointHandler, SessionsEndpointHandler>();
            services.AddScoped<ISessionService, SessionService>();
        }
    }
}
