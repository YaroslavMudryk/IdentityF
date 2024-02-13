using IdentityF.Core.Features.SignUp.Services;
using IdentityF.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.SignUp
{
    public static class SignUpDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IEndpointHandler, SignUpEndpointHandler>();
            services.AddScoped<ISignUpService, SignUpService>();
        }
    }
}
