using Cold.Deliveries.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Deliveries.Core.DAL;

internal class DeliveriesDbContext : DbContext
{
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<DeliveryProduct> DeliveryProducts { get; set; }
    public DbSet<DeliveryPhoto> DeliveryPhotos { get; set; }
    public DbSet<TransportRequest> TransportRequests { get; set; }
    public DbSet<TransportStatus> TransportStatuses { get; set; }

    public DeliveriesDbContext(DbContextOptions<DeliveriesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("deliveries");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
