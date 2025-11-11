using Cold.Deliveries.Shared.Dtos;

namespace Cold.Deliveries.Core.Services;

public interface IDeliveryService
{
    Task<DeliveryDto> GetAsync(Guid deliveryId);
    Task<DeliveryDto> GetByDeliveryNumberAsync(string deliveryNumber);
    Task<IReadOnlyList<DeliveryDto>> GetAllAsync();
    Task<IReadOnlyList<DeliveryDto>> GetBySupplierIdAsync(Guid supplierId);
    Task<IReadOnlyList<DeliveryDto>> GetUninvoicedAsync();
    Task<IReadOnlyList<DeliveryDto>> GetUninvoicedBySupplierIdAsync(Guid supplierId);
    Task<Guid> AddAsync(CreateDeliveryDto dto);
    Task UpdateAsync(Guid deliveryId, CreateDeliveryDto dto);
    Task DeleteAsync(Guid deliveryId);
    Task MarkAsInvoicedAsync(Guid deliveryId);
}
