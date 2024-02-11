using IdentityF.Core.Features.Shared.Email.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.Shared.Email
{
    public static class EmailDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IEmailService, FakeEmailService>();
        }
    }
}
