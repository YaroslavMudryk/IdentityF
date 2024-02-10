using IdentityF.Core.Features.SignUp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.SignUp
{
    public static class SignUpDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ISignUpService, SignUpService>();
        }
    }
}
