using IdentityF.Core.Constants;
using IdentityF.Core.Features.SignUp.Dtos;
using IdentityF.Core.Features.SignUp.Services;
using IdentityF.Core.Handlers;
using IdentityF.Core.Helpers;
using IdentityF.Core.Options;
using IdentityF.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using YaMu.Helpers.Extensions;

namespace IdentityF.Core.Features.SignUp
{
    public class SignUpEndpointHandler : BaseEndpointHandler, IEndpointHandler
    {
        public SignUpEndpointHandler()
        {

        }
        public SignUpEndpointHandler(string action, EndpointOptions endpoint)
        {
            Action = action;
            Endpoint = endpoint;
        }
        public string Action { get; } = HttpActions.SignUpAction;

        public EndpointOptions Endpoint { get; }

        public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
        {
            var currentOption = options[Action];
            return new SignUpEndpointHandler(Action, currentOption);
        }

        public async Task HandleAsync(HttpContext context)
        {
            var registerBody = await JsonSerializer.DeserializeAsync<SignUpDto>(context.Request.Body, JsonOptionsDefault.Default);

            ValidateModel(registerBody);

            var signUpService = context.RequestServices.GetRequiredService<ISignUpService>();

            await signUpService.SignUpAsync(registerBody);

            await context.Response.WriteAsJsonAsync(ApiResponse.Success(200, null));
        }
    }
}
