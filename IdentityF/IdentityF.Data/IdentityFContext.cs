using IdentityF.Data.Entities;
using IdentityF.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using YaMu.Helpers;
using YaMu.Helpers.Db.Extensions;

namespace IdentityF.Data
{
    public class IdentityFContext : DbContext
    {
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        public IdentityFContext(DbContextOptions<IdentityFContext> contextOptions, ICurrentUserContext currentUserContext, IDateTimeProvider dateTimeProvider) : base(contextOptions)
        {
            _currentUserContext = currentUserContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public DbSet<Password> Passwords { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Mfa> Mfas { get; set; }
        public DbSet<Qr> Qrs { get; set; }
        public DbSet<Confirm> Confirms { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ExternalLogin> ExternalLogins { get; set; }
        public DbSet<LoginAttempt> LoginAttempts { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<AppClaim> AppClaims { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityFContext).Assembly);
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfo(_currentUserContext, _dateTimeProvider);
            this.DetectLogHistory(_dateTimeProvider);

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfo(_currentUserContext, _dateTimeProvider);
            var logs = this.DetectLogHistory(_dateTimeProvider);

            var saved = await base.SaveChangesAsync(cancellationToken);

            this.ChangeLogs.AddRange(logs);
            await base.SaveChangesAsync(cancellationToken);

            return saved;
        }
    }
}
