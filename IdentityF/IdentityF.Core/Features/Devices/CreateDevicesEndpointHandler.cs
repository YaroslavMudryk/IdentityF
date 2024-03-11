using IdentityF.Core.Constants;
using IdentityF.Core.Handlers;
using IdentityF.Core.Options;
using Microsoft.AspNetCore.Http;

namespace IdentityF.Core.Features.Devices;

public class CreateDevicesEndpointHandler : IEndpointHandler
{
    public CreateDevicesEndpointHandler()
    {

    }
    public CreateDevicesEndpointHandler(string action, EndpointOptions endpoint)
    {
        Action = action;
        Endpoint = endpoint;
    }
    public string Action { get; } = HttpActions.DevicesAction;

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

