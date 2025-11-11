using Cold.Deliveries.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Deliveries.Core.DAL.Configurations;

internal class TransportRequestConfiguration : IEntityTypeConfiguration<TransportRequest>
{
    public void Configure(EntityTypeBuilder<TransportRequest> builder)
    {
        builder.ToTable("TransportRequests");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DeliveryId)
            .IsRequired(false);

        builder.Property(x => x.SupplierId)
            .IsRequired();

        builder.Property(x => x.TransportStatusId)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.RequestDate)
            .IsRequired();

        builder.Property(x => x.ScheduledPickupDate)
            .IsRequired(false);

        builder.Property(x => x.ActualPickupDate)
            .IsRequired(false);

        builder.Property(x => x.ActualDeliveryDate)
            .IsRequired(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasOne(tr => tr.Delivery)
            .WithMany(d => d.TransportRequests)
            .HasForeignKey(tr => tr.DeliveryId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasOne(tr => tr.TransportStatus)
            .WithMany(ts => ts.TransportRequests)
            .HasForeignKey(tr => tr.TransportStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
