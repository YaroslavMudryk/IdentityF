using IdentityF.Core.Constants;
using IdentityF.Core.Exceptions.Extensions;
using IdentityF.Core.Features.Sessions.Services;
using IdentityF.Core.Handlers;
using IdentityF.Core.Options;
using IdentityF.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.Sessions
{
    public class SessionsEndpointHandler : IEndpointHandler
    {
        public SessionsEndpointHandler()
        {

        }
        public SessionsEndpointHandler(string action, EndpointOptions endpoint)
        {
            Action = action;
            Endpoint = endpoint;
        }
        public string Action { get; } = HttpActions.SessionsAction;

        public EndpointOptions Endpoint { get; }

        public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
        {
            var currentOption = options[Action];
            return new SessionsEndpointHandler(Action, currentOption);
        }

        public async Task HandleAsync(HttpContext context)
        {
            int query = Convert.ToInt32(context.Request.Query["q"]);

            int page = Convert.ToInt32(context.Request.Query["page"]);

            page = page == 0 ? 1 : page;

            var authService = context.RequestServices.GetRequiredService<ISessionService>();

            var result = await authService.GetUserSessionsAsync(query, page);

            await context.Response.WriteAsJsonAsync(ApiResponse.Success(200, result));
        }
    }
}
