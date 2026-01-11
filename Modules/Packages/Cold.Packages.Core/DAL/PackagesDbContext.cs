using Cold.Packages.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Packages.Core.DAL;

public class PackagesDbContext : DbContext
{
    public PackagesDbContext(DbContextOptions<PackagesDbContext> options) : base(options)
    {
    }

    public DbSet<Package> Packages { get; set; }
    public DbSet<PackageRental> PackageRentals { get; set; }
    public DbSet<PackageRentalItem> PackageRentalItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("packages");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}