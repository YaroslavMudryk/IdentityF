using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Managers
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
