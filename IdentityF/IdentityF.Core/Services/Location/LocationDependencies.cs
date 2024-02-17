using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Services.Location
{
    public static class LocationDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ILocationService, IpInfoLocationService>();
        }
    }
}
