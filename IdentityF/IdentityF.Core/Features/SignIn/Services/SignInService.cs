using Extensions.DeviceDetector;
using Extensions.Password;
using Google.Authenticator;
using IdentityF.Core.Constants;
using IdentityF.Core.ErrorHandling.Exceptions;
using IdentityF.Core.Extensions;
using IdentityF.Core.Features.SignIn.Dtos;
using IdentityF.Core.Options;
using IdentityF.Core.Services.Auth;
using IdentityF.Core.Services.Auth.Dtos;
using IdentityF.Core.Services.Location;
using IdentityF.Data;
using IdentityF.Data.Entities;
using IdentityF.Data.Entities.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using YaMu.Helpers;

namespace IdentityF.Core.Features.SignIn.Services
{
    public class SignInService : ISignInService
    {
        private readonly IdentityFContext _db;
        private readonly ISessionManager _sessionManager;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IDetector _detector;
        private readonly ILocationService _locationService;
        private readonly ITokenService _tokenService;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IdentityFOptions _options;
        public SignInService(IdentityFContext db, ISessionManager sessionManager, IDateTimeProvider dateTimeProvider, IDetector detector, ILocationService locationService, ITokenService tokenService, ICurrentUserContext currentUserContext, IOptions<IdentityFOptions> options)
        {
            _db = db;
            _sessionManager = sessionManager;
            _dateTimeProvider = dateTimeProvider;
            _detector = detector;
            _locationService = locationService;
            _tokenService = tokenService;
            _currentUserContext = currentUserContext;
            _options = options.Value;
        }

        public async Task<JwtTokenDto> SignInByMfaAsync(SignInMfaDto signInMfa)
        {
            var sessionId = Guid.Parse(signInMfa.SessionId);

            var session = await _db.Sessions.AsNoTracking().Include(s => s.User).FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
                throw new NotFoundException($"Session with ID: {sessionId} not found");

            if (session.Status != SessionStatus.New)
                throw new BadRequestException("Session already activated or closed");

            var secretKey = session.User.MfaSecretKey;
            var twoFactor = new TwoFactorAuthenticator();

            var currentPin = twoFactor.GetCurrentPIN(secretKey);

            if (!currentPin.Equals(signInMfa.Code))
                throw new BadRequestException("Code is incorrect");

            var jwtToken = await _tokenService.GetUserTokenAsync(new UserTokenDto
            {
                AuthType = AuthType.Password,
                UserId = session.UserId,
                User = session.User,
                Lang = session.Language,
                SessionId = session.Id,
                Session = session
            });

            session.Tokens = new List<Token> { jwtToken };

            await _db.LoginAttempts.AddAsync(new LoginAttempt
            {
                Login = session.User.Login,
                Client = session.Client,
                Location = session.Location,
                IsSuccess = true,
                UserId = session.UserId
            });

            session.Status = SessionStatus.Active;

            _db.Sessions.Update(session);

            await _db.SaveChangesAsync();

            _sessionManager.AddSession(new SessionModel
            {
                Id = session.Id,
                UserId = session.UserId,
                Tokens = new List<TokenModel> { new TokenModel { ExpiredAt = jwtToken.ExpiredAt, Token = jwtToken.JwtToken, RefreshToken = jwtToken.RefreshToken } }
            });

            return new JwtTokenDto
            {
                ExpiredAt = jwtToken.ExpiredAt,
                RefreshToken = jwtToken.RefreshToken,
                Token = jwtToken.JwtToken
            };
        }

        public async Task<JwtTokenDto> SignInByPasswordAsync(SignInDto signInDto)
        {
            if (!Regex.IsMatch(signInDto.Password, _options.Password.Regex))
                throw new PasswordRequirementsException(_options.Password.ErrorRegexMessages["en"]);

            var utcNow = _dateTimeProvider.UtcNow;

            var app = await _db.Apps.AsNoTracking().FirstOrDefaultAsync(app => app.ClientId == signInDto.App.Id && app.ClientSecret == signInDto.App.Secret);

            if (app == null)
                throw new NotFoundException();

            if (!app.IsActive || !app.IsActiveByTime(utcNow))
                throw new AppNotActivatedException();

            var location = await _locationService.GetIpInfoAsync(_currentUserContext.GetIp());

            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Login == signInDto.Login);
            if (user == null)
            {
                await _db.LoginAttempts.AddAsync(new LoginAttempt
                {
                    Login = signInDto.Login,
                    Password = signInDto.Password,
                    Client = signInDto.Device,
                    Location = location,
                    IsSuccess = false
                });
                await _db.SaveChangesAsync();
                throw new BadRequestException("Check your credentials");
            }

            if (!user.IsConfirmed)
                throw new BadRequestException("First approve your account");

            if (user.CanBeBlocked)
            {
                if (user.IsBlocked(utcNow))
                {
                    throw new BadRequestException($"Your account has been blocked up to {user.BlockedUntil.Value.ToString("HH:mm (dd.MM.yyyy)")}");
                }

                if (user.FailedLoginAttempts == 5)
                {
                    user.FailedLoginAttempts = 0;
                    user.BlockedUntil = utcNow.AddHours(1);

                    //ToDo: send notify about blocking account

                    _db.Users.Update(user);
                    await _db.SaveChangesAsync();

                    throw new BadRequestException($"Account locked up to {user.BlockedUntil.Value.ToString("HH:mm (dd.MM.yyyy)")}");
                }
            }

            if (signInDto.Device == null)
                signInDto.Device = _detector.GetClientInfo().MapToClientInfo();

            if (!signInDto.Password.VerifyPasswordHash(user.PasswordHash))
            {
                user.FailedLoginAttempts++;

                //ToDo: send notify about fail attempt login

                await _db.LoginAttempts.AddAsync(new LoginAttempt
                {
                    Login = signInDto.Login,
                    Password = signInDto.Password,
                    Client = signInDto.Device,
                    Location = location,
                    IsSuccess = false,
                    UserId = user.Id
                });

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                throw new BadRequestException("Password is incorrect");
            }

            var appInfo = new AppInfo
            {
                Id = app.Id,
                Name = app.Name,
                Description = app.Description,
                ShortName = app.ShortName,
                Version = signInDto.App.Version,
                Image = app.Image
            };

            var session = new Session
            {
                Id = Guid.NewGuid(),
                App = appInfo,
                Client = signInDto.Device,
                Location = location,
                UserId = user.Id,
                Type = SessionType.Password,
                ViaMFA = user.Mfa,
                Status = SessionStatus.New,
                Language = signInDto.Lang
            };

            user.FailedLoginAttempts = 0;

            if (user.Mfa)
            {
                _db.Users.Update(user);
                await _db.Sessions.AddAsync(session);
                await _db.SaveChangesAsync();

                return new JwtTokenDto
                {
                    SessionId = session.Id.ToString(),
                };
            }

            var sessionToken = await _tokenService.GetUserTokenAsync(new UserTokenDto
            {
                AuthType = AuthType.Password,
                UserId = user.Id,
                User = user,
                Lang = signInDto.Lang,
                SessionId = session.Id,
                Session = session
            });

            session.Status = SessionStatus.Active;
            session.Tokens = new List<Token> { sessionToken };

            await _db.LoginAttempts.AddAsync(new LoginAttempt
            {
                Login = signInDto.Login,
                Client = signInDto.Device,
                Location = location,
                IsSuccess = true,
                UserId = user.Id,
                SessionId = session.Id.ToString()
            });

            _db.Users.Update(user);
            await _db.Sessions.AddAsync(session);
            await _db.SaveChangesAsync();

            _sessionManager.AddSession(new SessionModel
            {
                Id = session.Id,
                UserId = user.Id,
                Tokens = new List<TokenModel> { new TokenModel { ExpiredAt = sessionToken.ExpiredAt, Token = sessionToken.JwtToken, RefreshToken = sessionToken.RefreshToken } }
            });

            return new JwtTokenDto
            {
                ExpiredAt = sessionToken.ExpiredAt,
                RefreshToken = sessionToken.RefreshToken,
                Token = sessionToken.JwtToken,
            };
        }
    }
}
