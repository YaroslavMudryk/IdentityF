using IdentityF.Data.Entities;
using IdentityF.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IdentityF.Data.Configurations
{
    public class MFAConfiguration : IEntityTypeConfiguration<Mfa>
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public void Configure(EntityTypeBuilder<Mfa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RestoreCodes).HasConversion(
                c => c.ToJson(),
                c => c.FromJson<string[]>());
        }
    }
}
