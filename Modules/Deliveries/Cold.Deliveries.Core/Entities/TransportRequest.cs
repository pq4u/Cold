namespace Cold.Deliveries.Core.Entities;

internal sealed class TransportRequest
{
    public Guid Id { get; private set; }
    public Guid? DeliveryId { get; private set; }
    public Delivery? Delivery { get; private set; }
    public Guid SupplierId { get; private set; }
    public short TransportStatusId { get; private set; }
    public TransportStatus TransportStatus { get; private set; }
    public DateTimeOffset RequestDate { get; private set; }
    public DateTimeOffset? ScheduledPickupDate { get; private set; }
    public DateTimeOffset? ActualPickupDate { get; private set; }
    public DateTimeOffset? ActualDeliveryDate { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public TransportRequest(Guid id, Guid? deliveryId, Guid supplierId, DateTimeOffset requestDate,
                           DateTimeOffset? scheduledPickupDate, string? notes)
    {
        Id = id;
        DeliveryId = deliveryId;
        SupplierId = supplierId;
        RequestDate = requestDate;
        ScheduledPickupDate = scheduledPickupDate;
        Notes = notes;
        TransportStatusId = TransportStatus.Statuses.ToRealize;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private TransportRequest()
    {
    }

    public void UpdateStatus(short transportStatusId, DateTimeOffset? actualPickupDate = null,
                            DateTimeOffset? actualDeliveryDate = null)
    {
        TransportStatusId = transportStatusId;

        if (actualPickupDate.HasValue)
            ActualPickupDate = actualPickupDate;

        if (actualDeliveryDate.HasValue)
            ActualDeliveryDate = actualDeliveryDate;

        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Update(DateTimeOffset? scheduledPickupDate, string? notes)
    {
        ScheduledPickupDate = scheduledPickupDate;
        Notes = notes;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void LinkToDelivery(Guid deliveryId)
    {
        DeliveryId = deliveryId;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
