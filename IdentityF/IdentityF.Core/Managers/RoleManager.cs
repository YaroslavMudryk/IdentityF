using IdentityF.Data;
using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityF.Core.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IdentityFContext _db;
        public RoleManager(IdentityFContext db)
        {
            _db = db;
        }

        public async Task<Role> GetDefaultRoleAsync()
        {
            return await _db.Roles.FirstOrDefaultAsync(role => role.IsDefault);
        }
    }
}
