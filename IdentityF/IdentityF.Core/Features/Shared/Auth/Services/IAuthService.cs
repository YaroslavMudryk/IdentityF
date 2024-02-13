using IdentityF.Core.Options;

namespace IdentityF.Core.Features.Shared.Auth.Services
{
    public interface IAuthService
    {
        Task CheckAuthorizationAsync(EndpointOptions endpointOptions);
    }
}
