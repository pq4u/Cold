using Cold.Deliveries.Core.Contracts.Repositories;
using Cold.Deliveries.Core.Entities;
using Cold.Deliveries.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Cold.Deliveries.Core.DAL;

namespace Cold.Deliveries.Core.Services;

internal class TransportRequestService : ITransportRequestService
{
    private readonly ITransportRequestRepository _transportRequestRepository;
    private readonly DeliveriesDbContext _dbContext;

    public TransportRequestService(ITransportRequestRepository transportRequestRepository, DeliveriesDbContext dbContext)
    {
        _transportRequestRepository = transportRequestRepository;
        _dbContext = dbContext;
    }

    public async Task<TransportRequestDto> GetAsync(Guid transportRequestId)
    {
        var request = await _transportRequestRepository.GetByIdAsync(transportRequestId);
        return request is null ? null : MapToDto(request);
    }

    public async Task<IReadOnlyList<TransportRequestDto>> GetAllAsync()
    {
        var requests = await _transportRequestRepository.GetAllAsync();
        return requests.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<TransportRequestDto>> GetBySupplierIdAsync(Guid supplierId)
    {
        var requests = await _transportRequestRepository.GetBySupplierIdAsync(supplierId);
        return requests.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<TransportRequestDto>> GetByStatusAsync(short statusId)
    {
        var requests = await _transportRequestRepository.GetByStatusAsync(statusId);
        return requests.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<TransportStatusDto>> GetAllStatusesAsync()
    {
        var statuses = await _dbContext.TransportStatuses.ToListAsync();
        return statuses.Select(s => new TransportStatusDto
        {
            Id = s.Id,
            Name = s.Name,
            DisplayName = s.DisplayName
        }).ToList();
    }

    public async Task<Guid> CreateAsync(CreateTransportRequestDto dto)
    {
        var requestId = Guid.NewGuid();
        var request = new TransportRequest(
            requestId, dto.DeliveryId, dto.SupplierId, dto.RequestDate, dto.ScheduledPickupDate, dto.Notes);

        await _transportRequestRepository.AddAsync(request);
        return requestId;
    }

    public async Task UpdateStatusAsync(Guid transportRequestId, UpdateTransportStatusDto dto)
    {
        var request = await _transportRequestRepository.GetByIdAsync(transportRequestId);
        if (request is null)
        {
            throw new ArgumentException("Transport request not found");
        }

        request.UpdateStatus(dto.TransportStatusId, dto.ActualPickupDate, dto.ActualDeliveryDate);
        await _transportRequestRepository.UpdateAsync(request);
    }

    public async Task UpdateAsync(Guid transportRequestId, DateTimeOffset? scheduledPickupDate, string? notes)
    {
        var request = await _transportRequestRepository.GetByIdAsync(transportRequestId);
        if (request is null)
        {
            throw new ArgumentException("Transport request not found");
        }

        request.Update(scheduledPickupDate, notes);
        await _transportRequestRepository.UpdateAsync(request);
    }

    public async Task LinkToDeliveryAsync(Guid transportRequestId, Guid deliveryId)
    {
        var request = await _transportRequestRepository.GetByIdAsync(transportRequestId);
        if (request is null)
        {
            throw new ArgumentException("Transport request not found");
        }

        request.LinkToDelivery(deliveryId);
        await _transportRequestRepository.UpdateAsync(request);
    }

    private static TransportRequestDto MapToDto(TransportRequest request)
    {
        return new TransportRequestDto
        {
            Id = request.Id,
            DeliveryId = request.DeliveryId,
            DeliveryNumber = request.Delivery?.DeliveryNumber ?? string.Empty,
            SupplierId = request.SupplierId,
            TransportStatusId = request.TransportStatusId,
            TransportStatusName = request.TransportStatus?.DisplayName ?? string.Empty,
            RequestDate = request.RequestDate,
            ScheduledPickupDate = request.ScheduledPickupDate,
            ActualPickupDate = request.ActualPickupDate,
            ActualDeliveryDate = request.ActualDeliveryDate,
            Notes = request.Notes
        };
    }
}
