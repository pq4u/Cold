namespace Cold.Deliveries.Shared.Dtos;

public class DeliveryProductDto
{
    public Guid Id { get; set; }
    public Guid DeliveryId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ClassType { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalValue { get; set; }
}
