using IdentityF.Core.Handlers;
using IdentityF.Core.Options;
using IdentityF.Core.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IdentityF.Core.Middlewares
{
    public class IdentityFMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IdentityFOptions _options;
        private IEnumerable<IEndpointHandler> _endpointHandlers;

        public IdentityFMiddleware(RequestDelegate next, IOptions<IdentityFOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, IServiceScopeFactory serviceScopeFactory)
        {
            using var scope = serviceScopeFactory.CreateScope();
            _endpointHandlers = scope.ServiceProvider.GetRequiredService<IEnumerable<IEndpointHandler>>().Select(eh => eh.CreateFromOptions(_options.Endpoints));

            foreach (var handler in _endpointHandlers)
            {
                if (handler.CanHandle(context))
                {
                    var authService = scope.ServiceProvider.GetRequiredService<IEndpointService>();
                    await authService.CheckHandlerAuthorizationAsync(handler.Endpoint);
                    await handler.HandleAsync(context);
                    break;
                }
            }

            await _next(context).ConfigureAwait(false);
        }
    }
}
