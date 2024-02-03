using IdentityF.Data.Entities.Internal;

namespace IdentityF.Data.Entities
{
    public class LoginAttempt : BaseModel<int>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public ClientInfo Client { get; set; }
        public LocationInfo Location { get; set; }
        public bool IsSuccess { set; get; }
        public string SessionId { set; get; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
