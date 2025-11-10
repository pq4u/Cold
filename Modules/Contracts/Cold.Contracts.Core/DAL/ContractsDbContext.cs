using Cold.Contracts.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Contracts.Core.DAL;

internal class ContractsDbContext : DbContext
{
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<ContractProduct> ContractProducts { get; set; }
    public DbSet<ContractStatus> ContractStatuses { get; set; }
    public DbSet<ContractAmendment> ContractAmendments { get; set; }

    public ContractsDbContext(DbContextOptions<ContractsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("contracts");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}