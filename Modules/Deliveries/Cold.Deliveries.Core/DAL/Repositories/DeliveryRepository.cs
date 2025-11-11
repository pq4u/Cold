using Cold.Deliveries.Core.Contracts.Repositories;
using Cold.Deliveries.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Deliveries.Core.DAL.Repositories;

internal class DeliveryRepository : IDeliveryRepository
{
    private readonly DeliveriesDbContext _dbContext;
    private readonly DbSet<Delivery> _deliveries;

    public DeliveryRepository(DeliveriesDbContext dbContext)
    {
        _dbContext = dbContext;
        _deliveries = _dbContext.Deliveries;
    }

    public async Task<Delivery> GetByIdAsync(Guid deliveryId)
        => await _deliveries
            .Include(d => d.DeliveryProducts)
            .Include(d => d.Photos)
            .Include(d => d.TransportRequests)
            .SingleOrDefaultAsync(x => x.Id == deliveryId);

    public async Task<Delivery> GetByDeliveryNumberAsync(string deliveryNumber)
        => await _deliveries
            .Include(d => d.DeliveryProducts)
            .Include(d => d.Photos)
            .Include(d => d.TransportRequests)
            .SingleOrDefaultAsync(x => x.DeliveryNumber == deliveryNumber);

    public async Task<IEnumerable<Delivery>> GetAllAsync()
        => await _deliveries
            .Include(d => d.DeliveryProducts)
            .Include(d => d.Photos)
            .ToListAsync();

    public async Task<IEnumerable<Delivery>> GetBySupplierIdAsync(Guid supplierId)
        => await _deliveries
            .Include(d => d.DeliveryProducts)
            .Include(d => d.Photos)
            .Where(x => x.SupplierId == supplierId)
            .ToListAsync();

    public async Task<IEnumerable<Delivery>> GetUninvoicedAsync()
        => await _deliveries
            .Include(d => d.DeliveryProducts)
            .Where(x => !x.IsInvoiced)
            .ToListAsync();

    public async Task<IEnumerable<Delivery>> GetUninvoicedBySupplierIdAsync(Guid supplierId)
        => await _deliveries
            .Include(d => d.DeliveryProducts)
            .Where(x => x.SupplierId == supplierId && !x.IsInvoiced)
            .ToListAsync();

    public async Task AddAsync(Delivery delivery)
    {
        await _deliveries.AddAsync(delivery);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Delivery delivery)
    {
        _deliveries.Update(delivery);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Delivery delivery)
    {
        _deliveries.Remove(delivery);
        await _dbContext.SaveChangesAsync();
    }
}
