using IdentityF.Data.Entities;
using System.Linq.Expressions;

namespace IdentityF.Core.Features.Shared.Managers.Services
{
    public interface IUserManager
    {
        Task<bool> IsExistUsersAsync(Expression<Func<User, bool>> expression);
    }
}
