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
    public class TurnOffMfaEndpointHandler : IEndpointHandler
    {
        public TurnOffMfaEndpointHandler()
        {

        }
        public TurnOffMfaEndpointHandler(string action, EndpointOptions endpoint)
        {
            Action = action;
            Endpoint = endpoint;
        }

        public string Action { get; } = HttpActions.TurnOffMfaAction;

        public EndpointOptions Endpoint { get; }

        public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
        {
            var currentOption = options[Action];
            return new TurnOffMfaEndpointHandler(Action, currentOption);
        }

        public async Task HandleAsync(HttpContext context)
        {
            string code = context.Request.Query["code"];

            var mfaService = context.RequestServices.GetRequiredService<IMfaService>();

            var result = await mfaService.DisableMfaAsync(code);

            await context.Response.WriteAsJsonAsync(ApiResponse.Success(200, result), JsonOptionsDefault.Default);
        }
    }
}
