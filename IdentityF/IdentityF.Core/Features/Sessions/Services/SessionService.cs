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
        private readonly ISessionManager _sessionManager;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ICurrentUserContext _currentUserContext;
        public SessionService(IdentityFContext db, ISessionManager sessionManager, IDateTimeProvider dateTimeProvider, ICurrentUserContext currentUserContext)
        {
            _db = db;
            _sessionManager = sessionManager;
            _dateTimeProvider = dateTimeProvider;
            _currentUserContext = currentUserContext;
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

            var sessionDtos = sessions.MapToDto(Guid.Parse(sessionId)).OrderByDescending(s => s.Current).ThenByDescending(s => s.CreatedAt).ToList();

            return sessionDtos;
        }
    }
}
