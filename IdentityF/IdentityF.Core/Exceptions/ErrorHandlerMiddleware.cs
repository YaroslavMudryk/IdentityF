using IdentityF.Core.Exceptions.Extensions;
using Microsoft.AspNetCore.Http;

namespace IdentityF.Core.Exceptions
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
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
