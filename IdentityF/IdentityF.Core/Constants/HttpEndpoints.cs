using IdentityF.Core.Options;

namespace IdentityF.Core.Constants
{
    public static class HttpEndpoints
    {
        public const string SignUp = "/api/v1/identity/signup";

        public static Dictionary<string, EndpointOptions> Default = new Dictionary<string, EndpointOptions>
        {
            { HttpActions.SignUpAction, new EndpointOptions { Endpoint = SignUp, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = false } },
        };
    }
}
