using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using YaMu.Helpers;

namespace IdentityF.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void ApplyAuditInfo(this DbContext dbContext, ICurrentUserContext currentUserContext, IDateTimeProvider dateTimeProvider)
        {
            var now = dateTimeProvider.UtcNow;

            var entries = dbContext.ChangeTracker.Entries().Where(s => s.Entity is BaseModel && (s.State == EntityState.Added || s.State == EntityState.Modified));

            entries.ForEach(entry =>
            {
                var entity = (BaseModel)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                    entity.CreatedBy = currentUserContext != null ? currentUserContext.User.Id.ToString() : DefaultsAudit.CreatedBy;
                    entity.CreatedFromIp = currentUserContext != null ? currentUserContext.GetIp() : DefaultsAudit.CreatedFromIP;
                }

                if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = now;
                    entity.UpdatedBy = currentUserContext != null ? currentUserContext.User.Id.ToString() : DefaultsAudit.CreatedBy;
                    entity.UpdatedFromIp = currentUserContext != null ? currentUserContext.GetIp() : DefaultsAudit.CreatedFromIP;
                }

                if (entry.State == EntityState.Deleted)
                {
                    entity.DeletedAt = now;
                    entity.IsDeleted = true;
                    entity.DeletedBy = currentUserContext != null ? currentUserContext.User.Id.ToString() : DefaultsAudit.CreatedBy;
                    entry.State = EntityState.Modified;
                }

                entity.Version++;
            });
        }
    }
}
