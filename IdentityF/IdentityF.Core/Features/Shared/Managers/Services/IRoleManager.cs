using IdentityF.Data.Entities;

namespace IdentityF.Core.Features.Shared.Managers.Services
{
    public interface IRoleManager
    {
        Task<Role> GetDefaultRoleAsync();
    }
}
