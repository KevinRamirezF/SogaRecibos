using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SogaRecibos.Domain.Receipts;

namespace SogaRecibos.Infrastructure.Persistence.Configs;
public sealed class ReceiptConfig : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> e)
    {
        e.ToTable("Receipts");
        e.HasKey(x => x.Id);
        e.Property(x => x.Service).IsRequired();
        e.Property(x => x.Alias).HasMaxLength(64);
        e.Property(x => x.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
        e.OwnsOne(x => x.Identifier, oi =>
        {
            oi.Property(p => p.Value).HasColumnName("Identifier").HasMaxLength(64).IsRequired();
            oi.HasIndex(p => p.Value);
        });
        e.HasIndex(x => new { x.OwnerUserId, x.Service }).HasDatabaseName("IX_Receipts_Owner_Service");
        e.HasIndex(x => new { x.OwnerUserId, x.Service, Identifier = x.Identifier.Value })
         .IsUnique()
         .HasDatabaseName("UX_Receipts_Owner_Service_Identifier");
    }
}