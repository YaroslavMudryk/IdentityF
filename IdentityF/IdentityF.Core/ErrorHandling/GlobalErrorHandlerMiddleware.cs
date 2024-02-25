using IdentityF.Core.ErrorHandling.Exceptions;
using IdentityF.Core.Exceptions.Extensions;
using Microsoft.AspNetCore.Http;

namespace IdentityF.Core.ErrorHandling
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
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
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
