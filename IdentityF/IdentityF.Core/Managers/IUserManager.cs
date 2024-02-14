using IdentityF.Data.Entities;
using System.Linq.Expressions;

namespace IdentityF.Core.Managers
{
    public interface IUserManager
    {
        Task<bool> IsExistUsersAsync(Expression<Func<User, bool>> expression);
    }
}
