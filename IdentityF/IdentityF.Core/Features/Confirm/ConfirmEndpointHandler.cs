using IdentityF.Core.Constants;
using IdentityF.Core.Features.Confirm.Services;
using IdentityF.Core.Handlers;
using IdentityF.Core.Options;
using IdentityF.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityF.Core.Exceptions.Extensions;

namespace IdentityF.Core.Features.Confirm
{
    public class ConfirmEndpointHandler : IEndpointHandler
    {
        public ConfirmEndpointHandler()
        {

        }
        public ConfirmEndpointHandler(string action, EndpointOptions endpoint)
        {
            Action = action;
            Endpoint = endpoint;
        }

        public string Action { get; } = HttpActions.ConfirmAction;

        public EndpointOptions Endpoint { get; }

        public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
        {
            var currentOption = options[Action];
            return new ConfirmEndpointHandler(Action, currentOption);
        }

        public async Task HandleAsync(HttpContext context)
        {
            string code = context.Request.Query["code"];

            int userId = Convert.ToInt32(context.Request.Query["userId"]);

            var confirmService = context.RequestServices.GetRequiredService<IConfirmAccountService>();

            var result = await confirmService.ConfirmAccountAsync(code, userId);

            await context.Response.WriteAsJsonAsync(ApiResponse.Success(200, result));
        }
    }
}
