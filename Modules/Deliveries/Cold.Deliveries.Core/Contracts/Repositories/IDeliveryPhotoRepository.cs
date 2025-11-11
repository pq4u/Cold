using Cold.Deliveries.Core.Entities;

namespace Cold.Deliveries.Core.Contracts.Repositories;

internal interface IDeliveryPhotoRepository
{
    Task<DeliveryPhoto> GetByIdAsync(Guid photoId);
    Task<IEnumerable<DeliveryPhoto>> GetByDeliveryIdAsync(Guid deliveryId);
    Task AddAsync(DeliveryPhoto photo);
    Task DeleteAsync(DeliveryPhoto photo);
}
