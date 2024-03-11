using IdentityF.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityF.Core.Features.Devices;

public class DevicesDependencies
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IEndpointHandler, GetDevicesEndpointHandler>();
        services.AddScoped<IEndpointHandler, CreateDevicesEndpointHandler>();
        services.AddScoped<IEndpointHandler, CheckDeviceEndpointHandler>();
    }
}
