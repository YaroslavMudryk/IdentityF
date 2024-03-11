using IdentityF.Core.Constants;
using IdentityF.Core.Handlers;
using IdentityF.Core.Options;
using Microsoft.AspNetCore.Http;

namespace IdentityF.Core.Features.Devices;

public class CheckDeviceEndpointHandler : IEndpointHandler
{
    public CheckDeviceEndpointHandler()
    {

    }
    public CheckDeviceEndpointHandler(string action, EndpointOptions endpoint)
    {
        Action = action;
        Endpoint = endpoint;
    }
    public string Action { get; } = HttpActions.CheckDeviceAction;

    public EndpointOptions Endpoint { get; }

    public IEndpointHandler CreateFromOptions(Dictionary<string, EndpointOptions> options)
    {
        var currentOption = options[Action];
        return new CheckDeviceEndpointHandler(Action, currentOption);
    }

    public async Task HandleAsync(HttpContext context)
    {

    }
}
