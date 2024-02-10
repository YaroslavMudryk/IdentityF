using IdentityF.Data.Entities;
using IdentityF.Data.Entities.Internal;
using IdentityF.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IdentityF.Data.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(s => s.Client)
                .HasConversion(
                c => c.ToJson(),
                c => c.FromJson<ClientInfo>());

            builder.Property(s => s.Location)
                .HasConversion(
                c => c.ToJson(),
                c => c.FromJson<LocationInfo>());

            builder.Property(s => s.App)
                                .HasConversion(
                c => c.ToJson(),
                c => c.FromJson<AppInfo>());
        }
    }
}
