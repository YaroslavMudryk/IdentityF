using System.Text.Json.Serialization;

namespace IdentityF.Core.Responses
{
    public class ApiResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonPropertyName("warning")]
        public string Warning { get; set; }
        [JsonPropertyName("data")]
        public object Data { get; set; }

        public static ApiResponse Success(int statusCode, object data)
        {
            return new ApiResponse { StatusCode = statusCode, Data = data, Error = null };
        }

        public static ApiResponse SuccessWithWarning(int statusCode, object data, string warning)
        {
            return new ApiResponse { StatusCode = statusCode, Data = data, Warning = warning, Error = null };
        }
    }
}
