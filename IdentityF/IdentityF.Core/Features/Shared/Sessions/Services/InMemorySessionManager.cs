using IdentityF.Core.Features.Shared.Sessions.Dtos;
using IdentityF.Data;
using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YaMu.Helpers;

namespace IdentityF.Core.Features.Shared.Sessions.Services
{
    public class InMemorySessionManager : ISessionManager
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly List<SessionModel> _sessions;

        public InMemorySessionManager(IServiceScopeFactory serviceScopeFactory)
        {
            using var scope = serviceScopeFactory.CreateScope();
            _dateTimeProvider = scope.ServiceProvider.GetRequiredService<IDateTimeProvider>();
            _sessions = GetActualTokensFromDb(scope.ServiceProvider.GetRequiredService<IdentityFContext>());
        }

        public void AddSession(SessionModel sessionModel)
        {
            var session = _sessions.FirstOrDefault(s => s.Id == sessionModel.Id);
            if (session == null)
            {
                _sessions.Add(sessionModel);
            }
            else
            {
                _sessions.Remove(session);
                _sessions.Add(sessionModel);
            }
        }

        public void AddToken(Guid sessionId, TokenModel tokenModel)
        {
            var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
            if (session == null)
                return;
            session.Tokens.Add(tokenModel);
        }

        public bool IsActiveSession(string accessToken)
        {
            var now = _dateTimeProvider.UtcNow;

            var session = _sessions.Where(s => s.Tokens.FirstOrDefault(s => s.Token == accessToken) != null).FirstOrDefault();

            if (session == null)
                return false;

            var token = session.Tokens.FirstOrDefault(s => s.Token == accessToken);

            if (token.ExpiredAt < now)
            {
                session.Tokens.Remove(token);
                return false;
            }

            return true;
        }

        public void RemoveRangeTokens(IEnumerable<string> tokens)
        {
            foreach (var token in tokens)
            {
                RemoveToken(token);
            }
        }

        public void RemoveSession(Guid sessionId)
        {
            var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
            if (session == null)
                return;
            _sessions.Remove(session);
        }

        public void RemoveSessions(IEnumerable<Guid> sessionIds)
        {
            foreach (var sessionId in sessionIds)
            {
                RemoveSession(sessionId);
            }
        }

        public void RemoveToken(string token)
        {
            var session = _sessions.Where(s => s.Tokens.FirstOrDefault(s => s.Token == token) != null).FirstOrDefault();
            if (session == null)
                return;

            var tokenForRemove = session.Tokens.FirstOrDefault(s => s.Token == token);
            if (tokenForRemove == null)
                return;

            session.Tokens.Remove(tokenForRemove);
        }

        private List<SessionModel> GetActualTokensFromDb(IdentityFContext db)
        {
            var sessions = new List<SessionModel>();
            var now = _dateTimeProvider.UtcNow;

            var activeSessions = db.Sessions.AsNoTracking().Where(s => s.Status == SessionStatus.Active).ToList();

            var activeTokens = db.Tokens.AsNoTracking().Where(s => activeSessions.Select(s => s.Id).Contains(s.SessionId) && s.ExpiredAt > now).ToList();

            foreach (var session in activeSessions)
            {
                var newSession = new SessionModel
                {
                    Id = session.Id,
                    UserId = session.UserId,
                    Tokens = activeTokens.Where(s => s.SessionId == session.Id).Select(s => new TokenModel { Token = s.JwtToken, RefreshToken = s.RefreshToken, ExpiredAt = s.ExpiredAt }).ToList()
                };
                _sessions.Add(newSession);
            }

            return sessions;
        }
    }
}
