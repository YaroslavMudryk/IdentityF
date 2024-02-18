using IdentityF.Core.ErrorHandling.Exceptions;
using IdentityF.Data;
using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using YaMu.Helpers;

namespace IdentityF.Core.Features.Confirm.Services
{
    public class ConfirmAccountService : IConfirmAccountService
    {
        private readonly IdentityFContext _db;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        public ConfirmAccountService(IdentityFContext db, ICurrentUserContext currentUserContext, IDateTimeProvider dateTimeProvider)
        {
            _db = db;
            _currentUserContext = currentUserContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<bool> ConfirmAccountAsync(string code, int userId)
        {
            var confirmRequest = await _db.Confirms.AsNoTracking()
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Code == code && s.UserId == userId && s.Type == ConfirmType.Account);

            if (confirmRequest == null)
                throw new NotFoundException("Code not found");

            if (confirmRequest.IsActivated)
                throw new BadRequestException("Code already activated");

            var now = _dateTimeProvider.UtcNow;

            if (!confirmRequest.IsActualyRequest(now))
                throw new BadRequestException("Code was expired. Please request new");

            confirmRequest.IsActivated = true;
            confirmRequest.ActivetedAt = now;

            var user = confirmRequest.User;
            user.IsConfirmed = true;

            _db.Confirms.Update(confirmRequest);
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
