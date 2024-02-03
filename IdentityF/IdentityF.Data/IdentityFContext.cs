using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityF.Data
{
    public class IdentityFContext : DbContext
    {
        public IdentityFContext(DbContextOptions<IdentityFContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Password> Passwords { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Mfa> Mfas { get; set; }
        public DbSet<Confirm> Confirms { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ExternalLogin> ExternalLogins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<AppClaim> AppClaims { get; set; }
        public DbSet<App> Apps { get; set; }
    }
}
