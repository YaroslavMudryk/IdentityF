using IdentityF.Core.Options;

namespace IdentityF.Core.Constants
{
    public static class HttpEndpoints
    {
        public const string SignUp = "/api/v1/identity/signup";
        public const string SignIn = "/api/v1/identity/signin";
        public const string Sessions = "/api/v1/identity/sessions";
        public const string Confirm = "/api/v1/identity/confirm";

        public static Dictionary<string, EndpointOptions> Default = new Dictionary<string, EndpointOptions>
        {
            { HttpActions.SignUpAction, new EndpointOptions { Endpoint = SignUp, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = false } },
            { HttpActions.SignInAction, new EndpointOptions { Endpoint = SignIn, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = false } },
            { HttpActions.SessionsAction, new EndpointOptions { Endpoint = Sessions, IsAvailable = true, HttpMethod = HttpMethod.Get.Method, IsSecure = true } },
            { HttpActions.CloseSessionsAction, new EndpointOptions { Endpoint = Sessions, IsAvailable = true, HttpMethod = HttpMethod.Delete.Method, IsSecure = true } },
            { HttpActions.ConfirmAction, new EndpointOptions { Endpoint = Confirm, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = false } }
        };
    }
}
