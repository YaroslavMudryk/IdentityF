using System.Text.Json.Serialization;

namespace IdentityF.Data.Entities
{
    public class Claim : BaseModel<int>
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public string Issuer { get; set; }
        public string DisplayText { get; set; }
        [JsonIgnore]
        public List<RoleClaim> RoleClaims { get; set; }
        [JsonIgnore]
        public List<AppClaim> AppClaims { get; set; }
    }
}
