using Cold.Contracts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Contracts.Core.DAL.Configurations;

internal class ContractProductConfiguration : IEntityTypeConfiguration<ContractProduct>
{
    public void Configure(EntityTypeBuilder<ContractProduct> builder)
    {
        builder.ToTable("ContractProducts");

        builder.HasKey(x => new { x.ContractId, x.ProductId });

        builder.HasOne(cp => cp.Contract)
            .WithMany(c => c.ContractProducts)
            .HasForeignKey(cp => cp.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.ProductId)
            .IsRequired();
    }
}