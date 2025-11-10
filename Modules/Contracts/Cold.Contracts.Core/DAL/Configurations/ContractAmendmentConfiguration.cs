using Cold.Contracts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Contracts.Core.DAL.Configurations;

internal class ContractAmendmentConfiguration : IEntityTypeConfiguration<ContractAmendment>
{
    public void Configure(EntityTypeBuilder<ContractAmendment> builder)
    {
        builder.ToTable("ContractAmendments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AmendmentNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.AmendmentNumber)
            .IsUnique();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.HasOne(a => a.Contract)
            .WithMany(c => c.Amendments)
            .HasForeignKey(a => a.ContractId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}