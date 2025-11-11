using Cold.Deliveries.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Deliveries.Core.DAL.Configurations;

internal class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("Deliveries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DeliveryNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.DeliveryNumber)
            .IsUnique();

        builder.Property(x => x.SupplierId)
            .IsRequired();

        builder.Property(x => x.DeliveryDate)
            .IsRequired();

        builder.Property(x => x.TotalValue)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.IsInvoiced)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasMany(d => d.DeliveryProducts)
            .WithOne(dp => dp.Delivery)
            .HasForeignKey(dp => dp.DeliveryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Photos)
            .WithOne(p => p.Delivery)
            .HasForeignKey(p => p.DeliveryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.TransportRequests)
            .WithOne(tr => tr.Delivery)
            .HasForeignKey(tr => tr.DeliveryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
