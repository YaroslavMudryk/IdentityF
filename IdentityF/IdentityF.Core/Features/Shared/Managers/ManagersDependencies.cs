using IdentityF.Core.Features.Shared.Managers.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.Shared.Managers
{
    public static class ManagersDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IRoleManager, RoleManager>();
        }
    }
}
