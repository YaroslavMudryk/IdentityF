using IdentityF.Core.Features.Sessions.Dtos;
using IdentityF.Data.Entities;

namespace IdentityF.Core.Features.Sessions.Mappings
{
    public static class SessionMapping
    {
        public static IEnumerable<SessionDto> MapToDto(this IEnumerable<Session> sessions, Guid? currentSessionId = null)
        {
            return sessions.Select(session => MapToDto(session, currentSessionId));
        }

        public static SessionDto MapToDto(this Session session, Guid? currentSessionId = null)
        {
            if (session == null)
                return null;
            return new SessionDto
            {
                Id = session.Id,
                CreatedAt = session.CreatedAt,
                Status = session.Status,
                WithMfa = session.ViaMFA,
                Current = currentSessionId.HasValue ? session.Id == currentSessionId : false,
                Language = session.Language,
                App = session.App,
                Location = session.Location,
                Client = session.Client,
                DeactivatedAt = session.DeactivatedAt
            };
        }

        public static IEnumerable<Guid> MapSessionIds(this IEnumerable<Session> sessions)
        {
            return sessions.Select(s => s.Id);
        }
    }
}
