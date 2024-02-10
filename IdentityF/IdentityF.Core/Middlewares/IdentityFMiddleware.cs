using IdentityF.Core.Handlers;
using IdentityF.Core.Options;
using Microsoft.AspNetCore.Http;
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

        public async Task InvokeAsync(HttpContext context, IEnumerable<IEndpointHandler> endpointHandlers)
        {
            _endpointHandlers = endpointHandlers.Select(eh => eh.CreateFromOptions(_options.Endpoints));

            foreach (var handler in _endpointHandlers)
            {
                if (handler.CanHandle(context))
                {
                    await handler.CheckAuthorizeStatusAsync(context);
                    await handler.HandleAsync(context);
                    break;
                }
            }

            await _next(context).ConfigureAwait(false);
        }
    }
}
