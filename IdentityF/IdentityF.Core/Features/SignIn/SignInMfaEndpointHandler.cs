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
using IdentityF.Core.Exceptions.Extensions;

namespace IdentityF.Core.Features.SignIn
{
    public class SignInMfaEndpointHandler : IEndpointHandler
    {
        public SignInMfaEndpointHandler()
        {

        }
        public SignInMfaEndpointHandler(string action, EndpointOptions endpoint)
        {
            Action = action;
            Endpoint = endpoint;
        }


        public string Action { get; } = HttpActions.SignInMfaAction;

        public EndpointOptions Endpoint { get; }

        public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
        {
            var currentOption = options[Action];
            return new SignInMfaEndpointHandler(Action, currentOption);
        }

        public async Task HandleAsync(HttpContext context)
        {
            var loginMfaBody = await JsonSerializer.DeserializeAsync<SignInMfaDto>(context.Request.Body, JsonOptionsDefault.Default);

            Validation.ValidateModel(loginMfaBody);

            var authService = context.RequestServices.GetRequiredService<ISignInService>();

            var result = await authService.SignInByMfaAsync(loginMfaBody);

            await context.Response.WriteAsJsonAsync(ApiResponse.Success(200, result));
        }
    }
}
