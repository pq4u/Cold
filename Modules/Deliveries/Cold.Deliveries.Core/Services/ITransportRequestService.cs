using Cold.Deliveries.Shared.Dtos;

namespace Cold.Deliveries.Core.Services;

public interface ITransportRequestService
{
    Task<TransportRequestDto> GetAsync(Guid transportRequestId);
    Task<IReadOnlyList<TransportRequestDto>> GetAllAsync();
    Task<IReadOnlyList<TransportRequestDto>> GetBySupplierIdAsync(Guid supplierId);
    Task<IReadOnlyList<TransportRequestDto>> GetByStatusAsync(short statusId);
    Task<IReadOnlyList<TransportStatusDto>> GetAllStatusesAsync();
    Task<Guid> CreateAsync(CreateTransportRequestDto dto);
    Task UpdateStatusAsync(Guid transportRequestId, UpdateTransportStatusDto dto);
    Task UpdateAsync(Guid transportRequestId, DateTimeOffset? scheduledPickupDate, string? notes);
    Task LinkToDeliveryAsync(Guid transportRequestId, Guid deliveryId);
}
