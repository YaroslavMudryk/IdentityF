using Google.Authenticator;
using IdentityF.Core.ErrorHandling.Exceptions;
using IdentityF.Core.Features.Mfa.Dtos;
using IdentityF.Core.Helpers;
using IdentityF.Core.Options;
using IdentityF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YaMu.Helpers;

namespace IdentityF.Core.Features.Mfa.Services
{
    public class MfaService : IMfaService
    {
        private readonly IdentityFContext _db;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IdentityFOptions _identityFOptions;
        public MfaService(IdentityFContext db, ICurrentUserContext currentUserContext, IDateTimeProvider dateTimeProvider, IOptions<IdentityFOptions> identityFOptions)
        {
            _db = db;
            _currentUserContext = currentUserContext;
            _dateTimeProvider = dateTimeProvider;
            _identityFOptions = identityFOptions.Value;
        }

        public async Task<bool> DisableMfaAsync(string code)
        {
            var userId = _currentUserContext.User.Id;

            var userForDisableMfa = await _db.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == userId);

            if (userForDisableMfa == null)
                throw new NotFoundException("User not found");

            if (!userForDisableMfa.Mfa)
                throw new BadRequestException("MFA already diactivated");

            var twoFactor = new TwoFactorAuthenticator();

            var currentPin = twoFactor.GetCurrentPIN(userForDisableMfa.MfaSecretKey);

            if (!currentPin.Equals(code))
                throw new BadRequestException("Code is incorrect");

            userForDisableMfa.Mfa = false;
            userForDisableMfa.MfaSecretKey = null;

            var activeMfa = await _db.Mfas.FirstOrDefaultAsync(s => s.UserId == userId && s.IsActivated);
            if (activeMfa == null)
                throw new BadRequestException("Some error, please contact support");

            activeMfa.DiactivedAt = _dateTimeProvider.UtcNow;
            activeMfa.DiactivedBySessionId = _currentUserContext.User.SessionId;

            _db.Users.Update(userForDisableMfa);
            _db.Mfas.UpdateRange(activeMfa);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<MfaDto> EnableMfaAsync(string code = null)
        {
            var userId = _currentUserContext.User.Id;

            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == userId);

            if (user == null)
                throw new NotFoundException("User not found");

            if (code == null)
            {
                var existMFA = await _db.Mfas.FirstOrDefaultAsync(s => s.UserId == userId && !s.IsActivated);
                if (existMFA == null)
                {
                    var secretKey = Guid.NewGuid().ToString("N");
                    var twoFactor = new TwoFactorAuthenticator();
                    var setupInfo = twoFactor.GenerateSetupCode(_identityFOptions.Name, user.Login, secretKey, false, 3);

                    user.MfaSecretKey = secretKey;
                    user.Mfa = false;

                    var newMfa = new Data.Entities.Mfa
                    {
                        UserId = userId,
                        EntryCode = setupInfo.ManualEntryKey,
                        QrCodeBase64 = setupInfo.QrCodeSetupImageUrl,
                        Secret = secretKey,
                        IsActivated = false,
                        Activated = null,
                        ActivatedBySessionId = null
                    };

                    _db.Users.Update(user);
                    await _db.Mfas.AddAsync(newMfa);
                    await _db.SaveChangesAsync();

                    return new MfaDto
                    {
                        QrCodeImage = setupInfo.QrCodeSetupImageUrl,
                        ManualEntryKey = setupInfo.ManualEntryKey,
                    };
                }
                else
                {
                    return new MfaDto
                    {
                        QrCodeImage = existMFA.QrCodeBase64,
                        ManualEntryKey = existMFA.EntryCode
                    };
                }
            }
            else
            {
                if (string.IsNullOrEmpty(user.MfaSecretKey))
                    throw new BadRequestException("Unable to activate MFA");

                var mfaToActivate = await _db.Mfas.AsNoTracking().FirstOrDefaultAsync(s => s.UserId == userId && !s.IsActivated);

                if (mfaToActivate == null)
                    throw new BadRequestException("Unable to activate MFA");

                if (mfaToActivate.Secret != user.MfaSecretKey)
                    throw new BadRequestException("Please write to support as soon as possible");

                var twoFactor = new TwoFactorAuthenticator();

                var currentPin = twoFactor.GetCurrentPIN(mfaToActivate.Secret);

                if (!currentPin.Equals(code))
                    throw new BadRequestException("Code is incorrect");

                user.Mfa = true;

                mfaToActivate.IsActivated = true;
                mfaToActivate.Activated = _dateTimeProvider.UtcNow;
                mfaToActivate.ActivatedBySessionId = _currentUserContext.User.SessionId;
                mfaToActivate.RestoreCodes = Generator.GetRestoreCodes();

                _db.Users.Update(user);
                _db.Mfas.Update(mfaToActivate);
                await _db.SaveChangesAsync();

                return new MfaDto
                {
                    RestoreCodes = mfaToActivate.RestoreCodes,
                };
            }
        }
    }
}
