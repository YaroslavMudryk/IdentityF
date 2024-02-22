using System.Text.Json.Serialization;

namespace IdentityF.Core.Features.SignIn.Dtos
{
    public class JwtTokenDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }
        [JsonPropertyName("expiredAt")]
        public DateTime? ExpiredAt { get; set; }
    }
}
