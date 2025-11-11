namespace Cold.Deliveries.Shared.Dtos;

public class CreateDeliveryProductDto
{
    public Guid ProductId { get; set; }
    public string ClassType { get; set; }
    public decimal Quantity { get; set; }
}