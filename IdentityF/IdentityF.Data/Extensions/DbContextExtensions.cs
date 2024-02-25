using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using YaMu.Helpers;

namespace IdentityF.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void ApplyAuditInfo(this DbContext dbContext, ICurrentUserContext currentUserContext, IDateTimeProvider dateTimeProvider)
        {
            var now = dateTimeProvider.UtcNow;

            var entries = dbContext.ChangeTracker.Entries().Where(s => s.Entity is BaseModel);

            entries.ForEach(entry =>
            {
                var entity = (BaseModel)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                    entity.CreatedBy = currentUserContext != null ? currentUserContext.User.Id.ToString() : DefaultsAudit.CreatedBy;
                    entity.CreatedFromIp = currentUserContext != null ? currentUserContext.GetIp() : DefaultsAudit.CreatedFromIP;
                    entity.Signature = Guid.NewGuid().ToString("N");
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

        public static IReadOnlyList<ChangeLog> DetectLogHistory(this DbContext dbContext, IDateTimeProvider dateTimeProvider)
        {
            var now = dateTimeProvider.UtcNow;

            var entries = dbContext.ChangeTracker.Entries().Where(s => s.Entity is BaseModel);

            var historyLogs = new List<ChangeLog>();

            entries.ForEach(entry =>
            {
                var entity = entry.Entity.GetType();

                var d = (BaseModel)entry.Entity;

                var newLog = new ChangeLog
                {
                    ChangeDate = now,
                    EntityName = entity.Name,
                    EntitySignature = d.Signature
                };

                if (entry.State == EntityState.Added)
                    newLog.ChangeType = ChangeType.Insert.ToString();

                if (entry.State == EntityState.Modified)
                    newLog.ChangeType = ChangeType.Update.ToString();

                newLog.Data = JsonSerializer.Serialize(entry.Entity,
                    new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
                    });

                newLog.Version = d.Version;

                historyLogs.Add(newLog);
            });

            return historyLogs;
        }
    }
}
