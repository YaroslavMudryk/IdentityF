using IdentityF.Core.Features.SignIn.Dtos;
using IdentityF.Data.Entities;

namespace IdentityF.Core.Features.SignIn.Services
{
    public interface ITokenService
    {
        Task<Token> GetUserTokenAsync(UserTokenDto userToken);
    }
}
