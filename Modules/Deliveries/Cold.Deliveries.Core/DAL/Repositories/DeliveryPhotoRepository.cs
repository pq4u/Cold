using Cold.Deliveries.Core.Contracts.Repositories;
using Cold.Deliveries.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Deliveries.Core.DAL.Repositories;

internal class DeliveryPhotoRepository : IDeliveryPhotoRepository
{
    private readonly DeliveriesDbContext _dbContext;
    private readonly DbSet<DeliveryPhoto> _photos;

    public DeliveryPhotoRepository(DeliveriesDbContext dbContext)
    {
        _dbContext = dbContext;
        _photos = _dbContext.DeliveryPhotos;
    }

    public async Task<DeliveryPhoto> GetByIdAsync(Guid photoId)
        => await _photos
            .SingleOrDefaultAsync(x => x.Id == photoId);

    public async Task<IEnumerable<DeliveryPhoto>> GetByDeliveryIdAsync(Guid deliveryId)
        => await _photos
            .Where(x => x.DeliveryId == deliveryId)
            .ToListAsync();

    public async Task AddAsync(DeliveryPhoto photo)
    {
        await _photos.AddAsync(photo);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(DeliveryPhoto photo)
    {
        _photos.Remove(photo);
        await _dbContext.SaveChangesAsync();
    }
}
