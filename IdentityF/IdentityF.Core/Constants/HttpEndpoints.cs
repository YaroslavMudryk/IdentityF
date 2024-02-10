using IdentityF.Core.Options;

namespace IdentityF.Core.Constants
{
    public static class HttpEndpoints
    {
        public const string InitDb = "/init/db";
        public const string SignUp = "/api/v1/identity/signup";

        public static Dictionary<string, EndpointOptions> Default = new Dictionary<string, EndpointOptions>
        {
            { HttpActions.InitDbAction, new EndpointOptions { Endpoint = InitDb, IsAvailable = true, HttpMethod = HttpMethod.Get.Method, IsSecure = false } },
            { HttpActions.SignUpAction, new EndpointOptions { Endpoint = SignUp, IsAvailable = true, HttpMethod = HttpMethod.Post.Method, IsSecure = false } },
        };
    }
}
