namespace Cold.Deliveries.Shared.Dtos;

public class CreateDeliveryDto
{
    public string DeliveryNumber { get; set; }
    public Guid SupplierId { get; set; }
    public DateTimeOffset DeliveryDate { get; set; }
    public string? Notes { get; set; }
    public List<CreateDeliveryProductDto> Products { get; set; } = new();
}