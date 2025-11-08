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
            x.ProductId, x.Date
        });
        
        builder.HasOne(pp => pp.Product)
            .WithMany(p => p.Prices)
            .HasForeignKey(pp => pp.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Price)
            .IsRequired();
        
        builder.Property(x => x.Class);
        
        builder.Property(x => x.Date)
            .IsRequired()
            .HasDefaultValueSql("NOW()");
    }
}