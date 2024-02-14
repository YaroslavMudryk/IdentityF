using IdentityF.Core.Options;

namespace IdentityF.Core.Services.Auth
{
    public interface IEndpointService
    {
        Task CheckHandlerAuthorizationAsync(EndpointOptions endpointOptions);
    }
}
