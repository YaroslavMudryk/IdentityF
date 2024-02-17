using IdentityF.Core.Features.Sessions.Dtos;

namespace IdentityF.Core.Features.Sessions.Services
{
    public interface ISessionService
    {
        Task<List<SessionDto>> GetUserSessionsAsync(int q, int page);
    }
}
