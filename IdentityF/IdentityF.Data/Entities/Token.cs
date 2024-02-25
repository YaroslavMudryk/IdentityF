using System.Text.Json.Serialization;

namespace IdentityF.Data.Entities
{
    public class Token : BaseModel<Guid>
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public bool RefreshTokenUsed { get; set; }
        public DateTime ExpiredAt { set; get; }
        public Guid SessionId { set; get; }
        [JsonIgnore]
        public Session Session { set; get; }
    }
}
