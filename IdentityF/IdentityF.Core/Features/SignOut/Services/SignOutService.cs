using IdentityF.Core.ErrorHandling.Exceptions;
using IdentityF.Core.Services.Auth;
using IdentityF.Data;
using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using YaMu.Helpers;

namespace IdentityF.Core.Features.SignOut.Services
{
    public class SignOutService : ISignOutService
    {
        private readonly IdentityFContext _db;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ISessionManager _sessionManager;
        public SignOutService(IdentityFContext db, ICurrentUserContext currentUserContext, IDateTimeProvider dateTimeProvider, ISessionManager sessionManager)
        {
            _db = db;
            _currentUserContext = currentUserContext;
            _dateTimeProvider = dateTimeProvider;
            _sessionManager = sessionManager;
        }

        public async Task<bool> LogoutAsync()
        {
            var token = _currentUserContext.GetBearerToken();

            if (!_sessionManager.IsActiveToken(token))
                throw new BadRequestException("Session is already expired or closed");

            var currentSessionId = _currentUserContext.User.SessionId;

            var sessionToClose = await _db.Sessions.AsNoTracking().FirstOrDefaultAsync(s => s.Id == currentSessionId);
            if (sessionToClose == null)
                throw new NotFoundException("Session not found");

            var now = _dateTimeProvider.UtcNow;

            sessionToClose.Status = SessionStatus.Close;
            sessionToClose.DeactivatedAt = now;
            sessionToClose.DeactivatedBySessionId = currentSessionId;

            _db.Sessions.Update(sessionToClose);
            await _db.SaveChangesAsync();

            _sessionManager.RemoveSession(currentSessionId);

            return true;
        }
    }
}
