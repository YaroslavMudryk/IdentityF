using IdentityF.Core.Features.Shared.Sms.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.Shared.Sms
{
    public static class SmsDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ISmsService, FakeSmsService>();
        }
    }
}
