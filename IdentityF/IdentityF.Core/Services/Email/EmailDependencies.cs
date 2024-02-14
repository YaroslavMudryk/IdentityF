using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Services.Email
{
    public static class EmailDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IEmailService, FakeEmailService>();
        }
    }
}
