using Cold.Contracts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Contracts.Core.DAL.Configurations;

internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contracts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ContractNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.ContractNumber)
            .IsUnique();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.ContractStatusId)
            .HasColumnType("smallint")
            .IsRequired();

        builder.HasOne(c => c.ContractStatus)
            .WithMany(cs => cs.Contracts)
            .HasForeignKey(c => c.ContractStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.IsAccepted)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired(false);

        builder.Property(x => x.SignedDate)
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasMany(c => c.ContractProducts)
            .WithOne(cp => cp.Contract)
            .HasForeignKey(cp => cp.ContractId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}