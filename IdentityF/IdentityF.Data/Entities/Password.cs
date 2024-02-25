using System.Text.Json.Serialization;

namespace IdentityF.Data.Entities
{
    public class Password : BaseModel<int>
    {
        public string PasswordHash { set; get; }
        public string Hint { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActivatedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
