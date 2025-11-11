namespace Cold.Deliveries.Shared.Dtos;

public class CreateTransportRequestDto
{
    public Guid? DeliveryId { get; set; }
    public Guid SupplierId { get; set; }
    public DateTimeOffset RequestDate { get; set; }
    public DateTimeOffset? ScheduledPickupDate { get; set; }
    public string? Notes { get; set; }
}
