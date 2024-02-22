using IdentityF.Core.Constants;
using IdentityF.Core.Features.Mfa.Services;
using IdentityF.Core.Handlers;
using IdentityF.Core.Helpers;
using IdentityF.Core.Options;
using IdentityF.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.Mfa
{
    public class TurnOnMfaEndpointHandler : IEndpointHandler
    {
        public TurnOnMfaEndpointHandler()
        {

        }
        public TurnOnMfaEndpointHandler(string action, EndpointOptions endpoint)
        {
            Action = action;
            Endpoint = endpoint;
        }

        public string Action { get; } = HttpActions.TurnOnMfaAction;

        public EndpointOptions Endpoint { get; }

        public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
        {
            var currentOption = options[Action];
            return new TurnOnMfaEndpointHandler(Action, currentOption);
        }

        public async Task HandleAsync(HttpContext context)
        {
            string code = context.Request.Query["code"];

            var mfaService = context.RequestServices.GetRequiredService<IMfaService>();

            var result = await mfaService.EnableMfaAsync(code);

            await context.Response.WriteAsJsonAsync(ApiResponse.Success(200, result), JsonOptionsDefault.Default);
        }
    }
}
