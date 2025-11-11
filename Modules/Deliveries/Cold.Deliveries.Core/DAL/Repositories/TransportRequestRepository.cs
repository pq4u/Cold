using Cold.Deliveries.Core.Contracts.Repositories;
using Cold.Deliveries.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Deliveries.Core.DAL.Repositories;

internal class TransportRequestRepository : ITransportRequestRepository
{
    private readonly DeliveriesDbContext _dbContext;
    private readonly DbSet<TransportRequest> _transportRequests;

    public TransportRequestRepository(DeliveriesDbContext dbContext)
    {
        _dbContext = dbContext;
        _transportRequests = _dbContext.TransportRequests;
    }

    public async Task<TransportRequest> GetByIdAsync(Guid transportRequestId)
        => await _transportRequests
            .Include(tr => tr.TransportStatus)
            .Include(tr => tr.Delivery)
            .SingleOrDefaultAsync(x => x.Id == transportRequestId);

    public async Task<IEnumerable<TransportRequest>> GetAllAsync()
        => await _transportRequests
            .Include(tr => tr.TransportStatus)
            .Include(tr => tr.Delivery)
            .ToListAsync();

    public async Task<IEnumerable<TransportRequest>> GetBySupplierIdAsync(Guid supplierId)
        => await _transportRequests
            .Include(tr => tr.TransportStatus)
            .Include(tr => tr.Delivery)
            .Where(x => x.SupplierId == supplierId)
            .ToListAsync();

    public async Task<IEnumerable<TransportRequest>> GetByStatusAsync(short statusId)
        => await _transportRequests
            .Include(tr => tr.TransportStatus)
            .Include(tr => tr.Delivery)
            .Where(x => x.TransportStatusId == statusId)
            .ToListAsync();

    public async Task AddAsync(TransportRequest transportRequest)
    {
        await _transportRequests.AddAsync(transportRequest);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TransportRequest transportRequest)
    {
        _transportRequests.Update(transportRequest);
        await _dbContext.SaveChangesAsync();
    }
}
