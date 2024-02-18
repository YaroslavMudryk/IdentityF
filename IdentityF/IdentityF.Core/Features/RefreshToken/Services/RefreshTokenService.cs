using IdentityF.Core.Constants;
using IdentityF.Core.ErrorHandling.Exceptions;
using IdentityF.Core.Features.SignIn.Dtos;
using IdentityF.Core.Features.SignIn.Services;
using IdentityF.Core.Services.Auth;
using IdentityF.Core.Services.Auth.Dtos;
using IdentityF.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityF.Core.Features.RefreshToken.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IdentityFContext _db;
        private readonly ITokenService _tokenService;
        private readonly ISessionManager _sessionManager;
        public RefreshTokenService(IdentityFContext db, ITokenService tokenService, ISessionManager sessionManager)
        {
            _db = db;
            _tokenService = tokenService;
            _sessionManager = sessionManager;
        }

        public async Task<JwtTokenDto> RefreshTokenAsync(string refreshToken)
        {
            var expiredToken = await _db.Tokens.AsNoTracking().Include(s => s.Session).ThenInclude(s => s.User).FirstOrDefaultAsync(s => s.RefreshToken == refreshToken);
            if (expiredToken == null)
                throw new BadRequestException("Invalid refresh token");

            if (expiredToken.Session.Status == Data.Entities.SessionStatus.Close)
                throw new BadRequestException("Current session already closed");

            var jwtToken = await _tokenService.GetUserTokenAsync(new UserTokenDto
            {
                AuthType = AuthType.Password,
                UserId = expiredToken.Session.UserId,
                User = expiredToken.Session.User,
                Lang = expiredToken.Session.Language,
                SessionId = expiredToken.Id,
                Session = expiredToken.Session
            });
            jwtToken.Session = null;

            await _db.Tokens.AddAsync(jwtToken);
            await _db.SaveChangesAsync();

            _sessionManager.AddToken(expiredToken.Session.Id, new TokenModel { ExpiredAt = jwtToken.ExpiredAt, RefreshToken = jwtToken.RefreshToken, Token = jwtToken.JwtToken });

            return new JwtTokenDto
            {
                ExpiredAt = jwtToken.ExpiredAt,
                RefreshToken = jwtToken.RefreshToken,
                Token = jwtToken.JwtToken
            };
        }
    }
}
