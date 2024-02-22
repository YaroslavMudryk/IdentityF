using IdentityF.Core.Features.Mfa.Dtos;

namespace IdentityF.Core.Features.Mfa.Services
{
    public interface IMfaService
    {
        Task<bool> DisableMfaAsync(string code);
        Task<MfaDto> EnableMfaAsync(string code = null);
    }
}
