using IdentityF.Data;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Services.Auth
{
    public static class AuthDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ICurrentUserContext, HttpCurrentUserContext>();
            services.AddScoped<IEndpointService, EndpointService>();
        }
    }
}
