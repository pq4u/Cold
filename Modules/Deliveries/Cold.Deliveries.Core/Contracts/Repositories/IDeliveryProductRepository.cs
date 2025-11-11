using Cold.Deliveries.Core.Entities;

namespace Cold.Deliveries.Core.Contracts.Repositories;

internal interface IDeliveryProductRepository
{
    Task<DeliveryProduct> GetByIdAsync(Guid deliveryProductId);
    Task<IEnumerable<DeliveryProduct>> GetByDeliveryIdAsync(Guid deliveryId);
    Task AddAsync(DeliveryProduct deliveryProduct);
    Task UpdateAsync(DeliveryProduct deliveryProduct);
    Task DeleteAsync(DeliveryProduct deliveryProduct);
}
