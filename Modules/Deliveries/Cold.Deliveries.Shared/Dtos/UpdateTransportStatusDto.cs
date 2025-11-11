namespace Cold.Deliveries.Shared.Dtos;

public class UpdateTransportStatusDto
{
    public short TransportStatusId { get; set; }
    public DateTimeOffset? ActualPickupDate { get; set; }
    public DateTimeOffset? ActualDeliveryDate { get; set; }
}
