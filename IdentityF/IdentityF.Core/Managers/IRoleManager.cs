using IdentityF.Data.Entities;

namespace IdentityF.Core.Managers
{
    public interface IRoleManager
    {
        Task<Role> GetDefaultRoleAsync();
    }
}
