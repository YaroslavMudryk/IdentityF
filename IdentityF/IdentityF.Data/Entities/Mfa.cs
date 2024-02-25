using System.Text.Json.Serialization;

namespace IdentityF.Data.Entities
{
    public class Mfa : BaseModel<int>
    {
        public DateTime? Activated { get; set; }
        public Guid? ActivatedBySessionId { get; set; }
        public bool IsActivated { get; set; }
        public string Secret { get; set; }
        public string EntryCode { get; set; }
        public string QrCodeBase64 { get; set; }
        public string[] RestoreCodes { get; set; }
        public DateTime? DiactivedAt { get; set; }
        public Guid? DiactivedBySessionId { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
