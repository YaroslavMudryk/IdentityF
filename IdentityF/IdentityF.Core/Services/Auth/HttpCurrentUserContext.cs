using IdentityF.Core.Constants;
using IdentityF.Data;
using Microsoft.AspNetCore.Http;

namespace IdentityF.Core.Services.Auth
{
    public class HttpCurrentUserContext : ICurrentUserContext
    {
        private readonly HttpContext _httpContext;

        public HttpCurrentUserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public CurrentUser User => new CurrentUser
        {
            Id = !IsAuth() ? 0 : Convert.ToInt32(_httpContext.User.Claims.First(s => s.Type == ConstantsClaimTypes.UserId)),
            Roles = !IsAuth() ? Enumerable.Empty<string>() : _httpContext.User.Claims.Where(s => s.Type == ConstantsClaimTypes.Role).Select(s => s.Value),
        };

        public string GetBearerToken()
        {
            var bearerWord = "Bearer ";
            var bearerToken = _httpContext.Request.Headers["Authorization"].ToString();
            if (bearerToken.StartsWith(bearerWord, StringComparison.OrdinalIgnoreCase))
            {
                return bearerToken.Substring(bearerWord.Length).Trim();
            }
            return bearerToken;
        }

        public string GetIp()
        {
            return _httpContext.Connection.RemoteIpAddress.ToString();
        }

        public bool IsAdmin()
        {
            if (!IsAuth())
                return false;
            return User.Roles.Any(s => s.Contains(DefaultsRoles.Administrator));
        }

        private bool IsAuth()
        {
            return _httpContext.User.Identity.IsAuthenticated;
        }
    }
}
