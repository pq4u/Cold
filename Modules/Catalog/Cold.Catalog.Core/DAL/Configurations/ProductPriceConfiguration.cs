using Cold.Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Catalog.Core.DAL.Configurations;

internal class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder.ToTable("ProductsPrices");

        builder.HasKey(x => new 
        {
            x.ProductId, Date = x.DateFrom
        });
        
        builder.HasOne(pp => pp.Product)
            .WithMany(p => p.Prices)
            .HasForeignKey(pp => pp.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Price)
            .IsRequired();
        
        builder.Property(x => x.ClassType);
        
        builder.Property(x => x.DateFrom)
            .IsRequired()
            .HasDefaultValueSql("NOW()");
        
        builder.Property(x => x.DateTo)
            .IsRequired(false);
    }
}