using IdentityF.Core.Services.Auth.Dtos;

namespace IdentityF.Core.Services.Auth
{
    public interface ISessionManager
    {
        void AddSession(SessionModel sessionModel);
        void AddToken(Guid sessionId, TokenModel tokenModel);
        bool IsActiveToken(string token);
        void RemoveSession(Guid sessionId);
        void RemoveSessions(IEnumerable<Guid> sessionIds);
        void RemoveToken(string token);
        void RemoveRangeTokens(IEnumerable<string> tokens);
    }
}
