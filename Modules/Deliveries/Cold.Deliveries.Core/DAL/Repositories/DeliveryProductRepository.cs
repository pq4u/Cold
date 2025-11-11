using Cold.Deliveries.Core.Contracts.Repositories;
using Cold.Deliveries.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Deliveries.Core.DAL.Repositories;

internal class DeliveryProductRepository : IDeliveryProductRepository
{
    private readonly DeliveriesDbContext _dbContext;
    private readonly DbSet<DeliveryProduct> _deliveryProducts;

    public DeliveryProductRepository(DeliveriesDbContext dbContext)
    {
        _dbContext = dbContext;
        _deliveryProducts = _dbContext.DeliveryProducts;
    }

    public async Task<DeliveryProduct> GetByIdAsync(Guid deliveryProductId)
        => await _deliveryProducts
            .SingleOrDefaultAsync(x => x.Id == deliveryProductId);

    public async Task<IEnumerable<DeliveryProduct>> GetByDeliveryIdAsync(Guid deliveryId)
        => await _deliveryProducts
            .Where(x => x.DeliveryId == deliveryId)
            .ToListAsync();

    public async Task AddAsync(DeliveryProduct deliveryProduct)
    {
        await _deliveryProducts.AddAsync(deliveryProduct);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(DeliveryProduct deliveryProduct)
    {
        _deliveryProducts.Update(deliveryProduct);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(DeliveryProduct deliveryProduct)
    {
        _deliveryProducts.Remove(deliveryProduct);
        await _dbContext.SaveChangesAsync();
    }
}
