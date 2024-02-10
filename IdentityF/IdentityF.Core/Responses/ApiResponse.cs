using System.Text.Json.Serialization;

namespace IdentityF.Core.Responses
{
    public class ApiResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonPropertyName("data")]
        public object Data { get; set; }

        public static ApiResponse Success(int statusCode, object data)
        {
            return new ApiResponse { StatusCode = statusCode, Data = data, Error = null };
        }
    }
}
