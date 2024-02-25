using IdentityF.Core.ErrorHandling.Exceptions;
using IdentityF.Core.Services.ChangeHistory.Dtos;
using IdentityF.Data;
using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityF.Core.Services.ChangeHistory
{
    public class ChangeHistoryService : IChangeHistoryService
    {
        private readonly IdentityFContext _db;
        public ChangeHistoryService(IdentityFContext db)
        {
            _db = db;
        }

        public async Task<List<ChangeLogDto>> GetChangeHistoryAsync(object id, Type entityType)
        {
            var item = await _db.FindAsync(entityType, id);

            if (item == null)
                throw new NotFoundException();

            var dbItem = item as BaseModel;

            return await _db.ChangeLogs
                .AsNoTracking()
                .Where(s => s.EntitySignature == dbItem.Signature)
                .OrderByDescending(s => s.ChangeDate)
                .Select(s => new ChangeLogDto
                {
                    Id = s.Id,
                    ChangeDate = s.ChangeDate,
                    ChangeType = s.ChangeType,
                    Data = s.Data,
                    Version = s.Version,
                })
                .ToListAsync();
        }
    }
}
