using Cold.Deliveries.Core.Entities;

namespace Cold.Deliveries.Core.Contracts.Repositories;

internal interface ITransportRequestRepository
{
    Task<TransportRequest> GetByIdAsync(Guid transportRequestId);
    Task<IEnumerable<TransportRequest>> GetAllAsync();
    Task<IEnumerable<TransportRequest>> GetBySupplierIdAsync(Guid supplierId);
    Task<IEnumerable<TransportRequest>> GetByStatusAsync(short statusId);
    Task AddAsync(TransportRequest transportRequest);
    Task UpdateAsync(TransportRequest transportRequest);
}
