using Cold.Deliveries.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Deliveries.Core.DAL.Configurations;

internal class DeliveryProductConfiguration : IEntityTypeConfiguration<DeliveryProduct>
{
    public void Configure(EntityTypeBuilder<DeliveryProduct> builder)
    {
        builder.ToTable("DeliveryProducts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DeliveryId)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.ClassType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.TotalValue)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.HasOne(dp => dp.Delivery)
            .WithMany(d => d.DeliveryProducts)
            .HasForeignKey(dp => dp.DeliveryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
