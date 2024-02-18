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
    public class SendConfirmEndpointHandler : IEndpointHandler
    {
        public SendConfirmEndpointHandler()
        {

        }
        public SendConfirmEndpointHandler(string action, EndpointOptions endpoint)
        {
            Action = action;
            Endpoint = endpoint;
        }
        public string Action { get; } = HttpActions.SendConfirmAction;

        public EndpointOptions Endpoint { get; }

        public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
        {
            var currentOption = options[Action];
            return new SendConfirmEndpointHandler(Action, currentOption);
        }

        public async Task HandleAsync(HttpContext context)
        {
            int userId = Convert.ToInt32(context.Request.Query["userId"]);

            var confirmService = context.RequestServices.GetRequiredService<IConfirmAccountService>();

            var result = await confirmService.SendConfirmAsync(userId);

            await context.Response.WriteAsJsonAsync(ApiResponse.Success(200, result));
        }
    }
}
