using Cold.Packages.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Packages.Core.DAL.Configurations;

public class PackageRentalConfiguration : IEntityTypeConfiguration<PackageRental>
{
    public void Configure(EntityTypeBuilder<PackageRental> builder)
    {
        builder.ToTable("PackageRentals");
        builder.HasKey(pr => pr.Id);

        builder.Property(pr => pr.Status)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasMany(pr => pr.Items)
            .WithOne(item => item.PackageRental)
            .HasForeignKey(item => item.PackageRentalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}