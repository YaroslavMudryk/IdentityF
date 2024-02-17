using IdentityF.Data.Entities.Internal;
using IdentityF.Data.Entities;

namespace IdentityF.Core.Features.Sessions.Dtos
{
    public class SessionDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Current { get; set; }
        public SessionStatus Status { get; set; }
        public string Language { get; set; }
        public AppInfo App { get; set; }
        public LocationInfo Location { get; set; }
        public ClientInfo Client { get; set; }
        public DateTime? DeactivatedAt { get; set; }
    }
}
