using IdentityF.Core.ErrorHandling.Exceptions;
using IdentityF.Core.Features.Sessions.Dtos;
using IdentityF.Core.Features.Sessions.Mappings;
using IdentityF.Core.Services.Auth;
using IdentityF.Data;
using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using YaMu.Helpers;

namespace IdentityF.Core.Features.Sessions.Services
{
    public class SessionService : ISessionService
    {
        private readonly IdentityFContext _db;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly ISessionManager _sessionManager;
        private readonly IDateTimeProvider _dateTimeProvider;
        public SessionService(IdentityFContext db, ISessionManager sessionManager, IDateTimeProvider dateTimeProvider, ICurrentUserContext currentUserContext)
        {
            _db = db;
            _sessionManager = sessionManager;
            _dateTimeProvider = dateTimeProvider;
            _currentUserContext = currentUserContext;
        }

        public async Task<int> CloseSessionsByIdsAsync(string[] ids)
        {
            var now = _dateTimeProvider.UtcNow;
            var userId = _currentUserContext.User.Id;
            var currentSessionId = _currentUserContext.User.SessionId;

            var sessionsToClose = await _db.Sessions.AsNoTracking().Where(s => ids.Select(s => Guid.Parse(s)).Contains(s.Id) && s.UserId == userId).ToListAsync();

            sessionsToClose.ForEach(session =>
            {
                if (session.Status == SessionStatus.Close)
                    throw new BadRequestException($"Session on device {session.Client.Device.Brand} {session.Client.Device.Model} already closed");

                session.Status = SessionStatus.Close;
                session.DeactivatedAt = now;
                session.DeactivatedBySessionId = currentSessionId;
            });

            _db.Sessions.UpdateRange(sessionsToClose);
            var affectedSessions = await _db.SaveChangesAsync();

            _sessionManager.RemoveSessions(sessionsToClose.Select(s => s.Id));

            return affectedSessions;
        }

        public async Task<List<SessionDto>> GetUserSessionsAsync(int q, int page)
        {
            var userId = _currentUserContext.User.Id;
            var sessionId = _currentUserContext.User.SessionId;

            var query = (IQueryable<Session>)_db.Sessions;

            query = query.Where(s => s.UserId == userId);
            query = query.Where(s => q == 0 ? s.Status == SessionStatus.Active || s.Status == SessionStatus.New : s.Status == SessionStatus.Close);
            query = query.Skip((page - 1) * 10).Take(10);
            query = query.OrderByDescending(s => s.CreatedAt);
            var sessions = await query.ToListAsync();

            var sessionDtos = sessions.MapToDto(sessionId).OrderByDescending(s => s.Current).ThenByDescending(s => s.CreatedAt).ToList();

            return sessionDtos;
        }
    }
}
