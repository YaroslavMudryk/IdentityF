using IdentityF.Core.Constants;
using IdentityF.Data;
using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using YaMu.Helpers;

namespace IdentityF.Core.Seeder
{
    public interface IDatabaseService
    {
        Task<bool> CreateDbAsync();
        Task<int> SeedSystemAsync();
    }

    public class IdentityDatabaseService : IDatabaseService
    {
        private readonly IdentityFContext _db;
        private readonly IDateTimeProvider _dateTimeProvider;
        public IdentityDatabaseService(IdentityFContext db, IDateTimeProvider dateTimeProvider)
        {
            _db = db;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<int> SeedSystemAsync()
        {
            int counter = 0;
            if (!await _db.Roles.AnyAsync())
            {
                await _db.Roles.AddRangeAsync(GetDefaultsRoles());
                counter++;
            }
            if (!await _db.Apps.AnyAsync())
            {
                await _db.Apps.AddRangeAsync(GetDefaultsApp());
                counter++;
            }

            if (counter > 0)
                await _db.SaveChangesAsync();

            return counter;
        }

        private IEnumerable<Role> GetDefaultsRoles()
        {
            yield return new Role
            {
                Name = DefaultsRoles.Administrator,
                IsDefault = false,
                NameNormalized = DefaultsRoles.Administrator.ToUpper(),
            };
            yield return new Role
            {
                Name = DefaultsRoles.User,
                IsDefault = true,
                NameNormalized = DefaultsRoles.User.ToUpper(),
            };
        }

        private IEnumerable<App> GetDefaultsApp()
        {
            var now = _dateTimeProvider.UtcNow;

            yield return new App
            {
                Name = "Test application",
                Description = "Application for development",
                IsActive = true,
                ShortName = "Test app",
                ActiveFrom = now,
                ActiveTo = now.AddYears(5),
                ClientId = "P94A3E87C1FD13H102800D",
                ClientSecret = "b2e459a6c58a472da53e47c46d6c5ad1j1ecda073aa2402b9724ac95ad65dcc4",
            };
        }

        public async Task<bool> CreateDbAsync()
        {
            return await _db.Database.EnsureCreatedAsync();
        }
    }
}
