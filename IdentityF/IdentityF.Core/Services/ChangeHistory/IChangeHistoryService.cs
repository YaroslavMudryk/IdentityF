using IdentityF.Core.Services.ChangeHistory.Dtos;

namespace IdentityF.Core.Services.ChangeHistory
{
    public interface IChangeHistoryService
    {
        Task<List<ChangeLogDto>> GetChangeHistoryAsync(object id, Type entityType);
    }
}
