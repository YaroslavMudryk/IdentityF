using IdentityF.Core.Exceptions;
using IdentityF.Core.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace IdentityF.Core.Handlers
{
    public interface IEndpointHandler
    {
        string Action { get; }

        EndpointOptions Endpoint { get; }

        IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options);

        Task HandleAsync(HttpContext context);

        virtual bool CanHandle(HttpContext context)
        {
            return context.Request.Path.Equals(Endpoint.Endpoint, StringComparison.OrdinalIgnoreCase) && context.Request.Method == Endpoint.HttpMethod && Endpoint.IsAvailable;
        }

        virtual async Task CheckAuthorizeStatusAsync(HttpContext context)
        {
            if (!Endpoint.IsSecure)
                return;

            var token = await context.GetTokenAsync("access_token");

            if (!context.User.Identity.IsAuthenticated)
                throw new UnauthorizedException();
        }
    }
}
