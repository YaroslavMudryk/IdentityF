using Extensions.DeviceDetector;
using IdentityF.Core.Features.SignIn.Services;
using IdentityF.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.SignIn
{
    public static class SignInDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddDeviceDetector();
            services.AddScoped<IEndpointHandler, SignInEndpointHandler>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISignInService, SignInService>();
        }
    }
}
