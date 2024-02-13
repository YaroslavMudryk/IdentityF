using IdentityF.Core.Responses;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IdentityF.Core.Exceptions.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteAsJsonAsync(this HttpResponse response, int statusCode, string error)
        {
            response.ContentType = MediaTypeNames.Application.Json;
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonSerializer.Serialize(new ApiResponse
            {
                StatusCode = statusCode,
                Error = error,
            }, new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull })).ConfigureAwait(true);
        }

        public static Task WriteAsJsonAsync(this HttpResponse response, HttpResponseException exception)
        {
            return WriteAsJsonAsync(response, exception.StatusCode, exception.Error);
        }
    }
}
