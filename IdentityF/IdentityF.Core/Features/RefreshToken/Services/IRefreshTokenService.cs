using IdentityF.Core.Features.SignIn.Dtos;

namespace IdentityF.Core.Features.RefreshToken.Services
{
    public interface IRefreshTokenService
    {
        Task<JwtTokenDto> RefreshTokenAsync(string refreshToken);
    }
}
