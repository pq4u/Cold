namespace Cold.Deliveries.Shared.Dtos;

public class TransportRequestDto
{
    public Guid Id { get; set; }
    public Guid? DeliveryId { get; set; }
    public string? DeliveryNumber { get; set; }
    public Guid SupplierId { get; set; }
    public short TransportStatusId { get; set; }
    public string TransportStatusName { get; set; }
    public DateTimeOffset RequestDate { get; set; }
    public DateTimeOffset? ScheduledPickupDate { get; set; }
    public DateTimeOffset? ActualPickupDate { get; set; }
    public DateTimeOffset? ActualDeliveryDate { get; set; }
    public string? Notes { get; set; }
}
