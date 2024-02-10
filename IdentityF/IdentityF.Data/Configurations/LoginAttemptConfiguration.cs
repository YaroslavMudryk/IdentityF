using IdentityF.Data.Entities;
using IdentityF.Data.Entities.Internal;
using IdentityF.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IdentityF.Data.Configurations
{
    public class LoginAttemptConfiguration : IEntityTypeConfiguration<LoginAttempt>
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public void Configure(EntityTypeBuilder<LoginAttempt> builder)
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
        }
    }
}
