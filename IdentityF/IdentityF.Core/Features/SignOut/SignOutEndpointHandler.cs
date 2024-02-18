using IdentityF.Core.Constants;
using IdentityF.Core.Exceptions.Extensions;
using IdentityF.Core.Features.SignOut.Services;
using IdentityF.Core.Handlers;
using IdentityF.Core.Options;
using IdentityF.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.SignOut
{
    public class SignOutEndpointHandler : IEndpointHandler
    {
        public SignOutEndpointHandler()
        {

        }
        public SignOutEndpointHandler(string action, EndpointOptions endpoint)
        {
            Action = action;
            Endpoint = endpoint;
        }

        public string Action { get; } = HttpActions.SignOutAction;

        public EndpointOptions Endpoint { get; }

        public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
        {
            var currentOption = options[Action];
            return new SignOutEndpointHandler(Action, currentOption);
        }

        public async Task HandleAsync(HttpContext context)
        {
            var authService = context.RequestServices.GetRequiredService<ISignOutService>();

            var result = await authService.LogoutAsync();

            await context.Response.WriteAsJsonAsync(ApiResponse.Success(200, result));
        }
    }
}
