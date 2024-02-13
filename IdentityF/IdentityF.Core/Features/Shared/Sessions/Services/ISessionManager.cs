﻿using IdentityF.Core.Features.Shared.Sessions.Dtos;

namespace IdentityF.Core.Features.Shared.Sessions.Services
{
    public interface ISessionManager
    {
        void AddSession(SessionModel sessionModel);
        void AddToken(Guid sessionId, TokenModel tokenModel);
        bool IsActiveSession(string token);
        void RemoveSession(Guid sessionId);
        void RemoveSessions(IEnumerable<Guid> sessionIds);
        void RemoveToken(string token);
        void RemoveRangeTokens(IEnumerable<string> tokens);
    }
}
