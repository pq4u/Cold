using Cold.Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Catalog.Core.DAL.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.Property(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(30)
            .IsRequired();
        
        builder.Property(x => x.Image);
    }
}