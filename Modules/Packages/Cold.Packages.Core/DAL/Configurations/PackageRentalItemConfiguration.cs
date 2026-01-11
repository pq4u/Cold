using Cold.Packages.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Packages.Core.DAL.Configurations;

public class PackageRentalItemConfiguration : IEntityTypeConfiguration<PackageRentalItem>
{
    public void Configure(EntityTypeBuilder<PackageRentalItem> builder)
    {
        builder.ToTable("PackageRentalItems");
        builder.HasKey(pri => pri.Id);

        builder.HasOne(pri => pri.Package)
            .WithMany()
            .HasForeignKey(pri => pri.PackageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}