namespace IdentityF.Core.Services.Db
{
    public interface IDatabaseService
    {
        Task<bool> CreateDbAsync();
        Task<int> SeedSystemAsync();
    }
}
