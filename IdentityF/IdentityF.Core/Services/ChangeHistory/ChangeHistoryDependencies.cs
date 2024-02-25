using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Services.ChangeHistory
{
    public static class ChangeHistoryDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IChangeHistoryService, ChangeHistoryService>();
        }
    }
}
