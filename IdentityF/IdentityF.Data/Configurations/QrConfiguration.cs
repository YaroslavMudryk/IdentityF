using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityF.Data.Configurations
{
    public class QrConfiguration : IEntityTypeConfiguration<Qr>
    {
        public void Configure(EntityTypeBuilder<Qr> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(s => s.QrCodeId);
        }
    }
}
