using Cold.Deliveries.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Deliveries.Core.DAL.Configurations;

internal class TransportStatusConfiguration : IEntityTypeConfiguration<TransportStatus>
{
    public void Configure(EntityTypeBuilder<TransportStatus> builder)
    {
        builder.ToTable("TransportStatuses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.DisplayName)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(ts => ts.TransportRequests)
            .WithOne(tr => tr.TransportStatus)
            .HasForeignKey(tr => tr.TransportStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new { Id = TransportStatus.Statuses.ToRealize, Name = "ToRealize", DisplayName = "Do realizacji" },
            new { Id = TransportStatus.Statuses.OnWayToSupplier, Name = "OnWayToSupplier", DisplayName = "W drodze do dostawcy" },
            new { Id = TransportStatus.Statuses.AtSupplier, Name = "AtSupplier", DisplayName = "U dostawcy" },
            new { Id = TransportStatus.Statuses.OnWayToColdStorage, Name = "OnWayToColdStorage", DisplayName = "W drodze do chłodni" },
            new { Id = TransportStatus.Statuses.InColdStorage, Name = "InColdStorage", DisplayName = "W chłodni" },
            new { Id = TransportStatus.Statuses.Cancelled, Name = "Cancelled", DisplayName = "Anulowano" }
        );
    }
}
