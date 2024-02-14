using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Services.Sms
{
    public static class SmsDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ISmsService, FakeSmsService>();
        }
    }
}
