using IdentityF.Data;
using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdentityF.Core.Features.Shared.Managers.Services
{
    public class UserManager : IUserManager
    {
        private readonly IdentityFContext _db;
        public UserManager(IdentityFContext db)
        {
            _db = db;
        }

        public async Task<bool> IsExistUsersAsync(Expression<Func<User, bool>> expression)
        {
            return await _db.Users.AnyAsync(expression);
        }
    }
}
