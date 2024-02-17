using System.Security.Claims;

namespace IdentityF.Core.Constants
{
    public static class ConstantsClaimTypes
    {
        public const string UserId = ClaimTypes.NameIdentifier;
        public const string Role = ClaimTypes.Role;
        public const string Login = "login";
        public const string SessionId = "sessionId";
        public const string AuthenticationMethod = ClaimTypes.AuthenticationMethod;
        public const string Language = "language";
    }
}
