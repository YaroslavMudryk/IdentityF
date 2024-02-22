using IdentityF.Core.Features.SignIn.Dtos;

namespace IdentityF.Core.Features.SignIn.Services
{
    public interface ISignInService
    {
        Task<JwtTokenDto> SignInByPasswordAsync(SignInDto signInDto);
        Task<JwtTokenDto> SignInByMfaAsync(SignInMfaDto signInMfa);
    }
}
