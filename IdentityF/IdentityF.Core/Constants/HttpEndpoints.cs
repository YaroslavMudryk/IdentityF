using IdentityF.Core.Options;

namespace IdentityF.Core.Constants
{
    public static class HttpEndpoints
    {
        public const string SignUp = "/api/v1/identity/signup";
        public const string SignIn = "/api/v1/identity/signin";
        public const string Sessions = "/api/v1/identity/sessions";
        public const string Confirm = "/api/v1/identity/confirm";
        public const string SendConfirm = "/api/v1/identity/send-confirm";
        public const string RefreshToken = "/api/v1/identity/refresh";
        public const string SignOut = "/api/v1/identity/signout";
        public const string Mfa = "/api/v1/identity/mfa";

        public static Dictionary<string, EndpointOptions> Default = new Dictionary<string, EndpointOptions>
        {
            { HttpActions.SignUpAction, new EndpointOptions { Endpoint = SignUp, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = false } },
            { HttpActions.SignInAction, new EndpointOptions { Endpoint = SignIn, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = false } },
            { HttpActions.SessionsAction, new EndpointOptions { Endpoint = Sessions, IsAvailable = true, HttpMethod = HttpMethod.Get.Method, IsSecure = true } },
            { HttpActions.CloseSessionsAction, new EndpointOptions { Endpoint = Sessions, IsAvailable = true, HttpMethod = HttpMethod.Delete.Method, IsSecure = true } },
            { HttpActions.ConfirmAction, new EndpointOptions { Endpoint = Confirm, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = false } },
            { HttpActions.SendConfirmAction, new EndpointOptions { Endpoint = SendConfirm, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = false } },
            { HttpActions.RefreshTokenAction, new EndpointOptions { Endpoint = RefreshToken, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = false } },
            { HttpActions.SignOutAction, new EndpointOptions { Endpoint = SignOut, IsAvailable = true, HttpMethod = HttpMethod.Delete.Method, IsSecure = true } },
            { HttpActions.TurnOnMfaAction, new EndpointOptions { Endpoint  = Mfa, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = true } },
            { HttpActions.TurnOffMfaAction, new EndpointOptions { Endpoint  = Mfa, IsAvailable = true, HttpMethod = HttpMethod.Delete.Method, IsSecure = true } },
        };
    }
}
