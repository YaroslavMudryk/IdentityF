using IdentityF.Core.Features.SignOut.Services;
using IdentityF.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.SignOut
{
    public static class SignOutDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ISignOutService, SignOutService>();
            services.AddScoped<IEndpointHandler, SignOutEndpointHandler>();
        }
    }
}
