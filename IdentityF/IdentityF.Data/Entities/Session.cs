using IdentityF.Data.Entities.Internal;

namespace IdentityF.Data.Entities
{
    public class Session : BaseModel<Guid>
    {
        public AppInfo App { get; set; }
        public LocationInfo Location { set; get; }
        public ClientInfo Client { set; get; }
        public SessionType Type { get; set; }
        public bool ViaMFA { get; set; }
        public SessionStatus Status { set; get; }
        public string Language { set; get; }
        public Guid? DeactivatedBySessionId { set; get; }
        public DateTime? DeactivatedAt { set; get; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Guid? DeviceId { get; set; }
        public Device Device { get; set; }
        public List<Token> Tokens { get; set; }
    }

    public enum SessionStatus
    {
        New = 1,
        Active,
        Close
    }

    public enum SessionType
    {
        Password = 1,
        ExternalService,
        Qr
    }
}
