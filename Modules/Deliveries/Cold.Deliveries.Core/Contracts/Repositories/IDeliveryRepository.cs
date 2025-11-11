using Cold.Deliveries.Core.Entities;

namespace Cold.Deliveries.Core.Contracts.Repositories;

internal interface IDeliveryRepository
{
    Task<Delivery> GetByIdAsync(Guid deliveryId);
    Task<Delivery> GetByDeliveryNumberAsync(string deliveryNumber);
    Task<IEnumerable<Delivery>> GetAllAsync();
    Task<IEnumerable<Delivery>> GetBySupplierIdAsync(Guid supplierId);
    Task<IEnumerable<Delivery>> GetUninvoicedAsync();
    Task<IEnumerable<Delivery>> GetUninvoicedBySupplierIdAsync(Guid supplierId);
    Task AddAsync(Delivery delivery);
    Task UpdateAsync(Delivery delivery);
    Task DeleteAsync(Delivery delivery);
}
