using IdentityF.Core.Exceptions.Extensions;
using Microsoft.AspNetCore.Http;

namespace IdentityF.Core.Exceptions
{
    public class IdentityFErrorHandler
    {
        private readonly RequestDelegate _next;

        public IdentityFErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpResponseException ex)
            {
                await context.Response.WriteAsJsonAsync(ex);
            }
            catch
            {
                throw;
            }
        }
    }
}
