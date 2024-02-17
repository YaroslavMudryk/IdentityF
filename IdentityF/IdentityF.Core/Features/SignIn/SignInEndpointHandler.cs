using IdentityF.Core.Constants;
using IdentityF.Core.Features.SignIn.Dtos;
using IdentityF.Core.Features.SignIn.Services;
using IdentityF.Core.Handlers;
using IdentityF.Core.Helpers;
using IdentityF.Core.Options;
using IdentityF.Core.Responses;
using IdentityF.Core.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using YaMu.Helpers.Extensions;

namespace IdentityF.Core.Features.SignIn
{
    public class SignInEndpointHandler : IEndpointHandler
    {
        public SignInEndpointHandler()
        {

        }
        public SignInEndpointHandler(string action, EndpointOptions endpoint)
        {
            Action = action;
            Endpoint = endpoint;
        }
        public string Action { get; } = HttpActions.SignInAction;

        public EndpointOptions Endpoint { get; }

        public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
        {
            var currentOption = options[Action];
            return new SignInEndpointHandler(Action, currentOption);
        }

        public async Task HandleAsync(HttpContext context)
        {
            var signInBody = await JsonSerializer.DeserializeAsync<SignInDto>(context.Request.Body, JsonOptionsDefault.Default);

            Validation.ValidateModel(signInBody);

            var signInService = context.RequestServices.GetRequiredService<ISignInService>();

            var token = await signInService.SignInAsync(signInBody);

            await context.Response.WriteAsJsonAsync(ApiResponse.Success(200, token));
        }
    }
}
