using Cold.Contracts.Core.Entities;
using Cold.Contracts.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cold.Contracts.Core.DAL.Configurations;

internal class ContractStatusConfiguration : IEntityTypeConfiguration<ContractStatus>
{
    public void Configure(EntityTypeBuilder<ContractStatus> builder)
    {
        builder.ToTable("ContractStatuses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(cs => cs.Contracts)
            .WithOne(c => c.ContractStatus)
            .HasForeignKey(c => c.ContractStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new { Id = (short)ContractStatusEnum.Draft, Name = "Draft", Description = "Contract is in draft state" },
            new { Id = (short)ContractStatusEnum.PendingAcceptance, Name = "PendingAcceptance", Description = "Contract is pending farmer acceptance" },
            new { Id = (short)ContractStatusEnum.Active, Name = "Active", Description = "Contract is active and accepted" },
            new { Id = (short)ContractStatusEnum.Rejected, Name = "Rejected", Description = "Contract was rejected by farmer" },
            new { Id = (short)ContractStatusEnum.Expired, Name = "Expired", Description = "Contract has expired" },
            new { Id = (short)ContractStatusEnum.Completed, Name = "Completed", Description = "Contract has been completed" },
            new { Id = (short)ContractStatusEnum.Terminated, Name = "Terminated", Description = "Contract was terminated early" }
        );
    }
}