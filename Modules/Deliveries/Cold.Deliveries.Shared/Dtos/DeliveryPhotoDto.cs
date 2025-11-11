namespace Cold.Deliveries.Shared.Dtos;

public class DeliveryPhotoDto
{
    public Guid Id { get; set; }
    public Guid DeliveryId { get; set; }
    public string FilePath { get; set; }
    public string? Description { get; set; }
}
