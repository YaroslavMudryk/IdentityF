using IdentityF.Core.Exceptions;
using IdentityF.Core.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IdentityF.Core.Features.Shared.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpContext _httpContext;
        private readonly IdentityFOptions _options;
        private readonly ISessionManager _sessionManager;

        public AuthService(IHttpContextAccessor httpContextAccessor, IOptions<IdentityFOptions> options, ISessionManager sessionManager)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _options = options.Value;
            _sessionManager = sessionManager;
        }

        public async Task CheckAuthorizationAsync(EndpointOptions endpoint)
        {
            if (!endpoint.IsSecure)
                return;

            var token = await _httpContext.GetTokenAsync("access_token");

            if (!_httpContext.User.Identity.IsAuthenticated)
                throw new UnauthorizedException();

            if (_options.Token.UseSessionManager)
                if (_sessionManager.IsActiveSession(token))
                    throw new UnauthorizedException();
        }
    }
}
