using Cold.Deliveries.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Deliveries.Core.DAL.Configurations;

internal class DeliveryPhotoConfiguration : IEntityTypeConfiguration<DeliveryPhoto>
{
    public void Configure(EntityTypeBuilder<DeliveryPhoto> builder)
    {
        builder.ToTable("DeliveryPhotos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DeliveryId)
            .IsRequired();

        builder.Property(x => x.FilePath)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.HasOne(p => p.Delivery)
            .WithMany(d => d.Photos)
            .HasForeignKey(p => p.DeliveryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
