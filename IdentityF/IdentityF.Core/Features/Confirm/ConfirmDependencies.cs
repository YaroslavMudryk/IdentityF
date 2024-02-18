using IdentityF.Core.Features.Confirm.Services;
using IdentityF.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.Confirm
{
    public static class ConfirmDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IConfirmAccountService, ConfirmAccountService>();
            services.AddScoped<IEndpointHandler, ConfirmEndpointHandler>();
        }
    }
}
