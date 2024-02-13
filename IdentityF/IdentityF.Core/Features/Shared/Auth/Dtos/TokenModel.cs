namespace IdentityF.Core.Features.Shared.Auth.Dtos
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
