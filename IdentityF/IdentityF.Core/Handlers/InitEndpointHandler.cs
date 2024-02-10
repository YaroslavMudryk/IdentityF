using IdentityF.Core.Constants;
using IdentityF.Core.Features.SignUp.Dtos;
using IdentityF.Core.Helpers;
using IdentityF.Core.Options;
using IdentityF.Core.Responses;
using IdentityF.Core.Seeder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using YaMu.Helpers.Extensions;

namespace IdentityF.Core.Handlers
{
    public class InitEndpointHandler : BaseEndpointHandler, IEndpointHandler
    {
        public InitEndpointHandler()
        {

        }
        public InitEndpointHandler(string action, EndpointOptions endpoint)
        {
            Action = action;
            Endpoint = endpoint;
        }
        public string Action { get; } = HttpActions.InitDbAction;

        public EndpointOptions Endpoint { get; }

        public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
        {
            var currentOption = options[Action];
            return new InitEndpointHandler(Action, currentOption);
        }

        public async Task HandleAsync(HttpContext context)
        {
            var dbService = context.RequestServices.GetRequiredService<IDatabaseService>();

            await dbService.CreateDbAsync();

            var tablesCount = await dbService.SeedSystemAsync();

            await context.Response.WriteAsJsonAsync(ApiResponse.Success(200, new {countTables = tablesCount }));
        }
    }
}
