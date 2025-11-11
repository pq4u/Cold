namespace Cold.Deliveries.Shared.Dtos;

public class DeliveryDto
{
    public Guid Id { get; set; }
    public string DeliveryNumber { get; set; }
    public Guid SupplierId { get; set; }
    public DateTimeOffset DeliveryDate { get; set; }
    public decimal TotalValue { get; set; }
    public string? Notes { get; set; }
    public bool IsInvoiced { get; set; }
    public List<DeliveryProductDto> Products { get; set; } = new();
    public List<DeliveryPhotoDto>? Photos { get; set; } = new();
}
