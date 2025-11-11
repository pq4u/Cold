namespace Cold.Deliveries.Core.Entities;

internal sealed class DeliveryProduct
{
    public Guid Id { get; private set; }
    public Guid DeliveryId { get; private set; }
    public Delivery Delivery { get; private set; }
    public Guid ProductId { get; private set; }
    public string ClassType { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalValue { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public DeliveryProduct(Guid id, Guid deliveryId, Guid productId, string classType, decimal quantity, decimal unitPrice)
    {
        Id = id;
        DeliveryId = deliveryId;
        ProductId = productId;
        ClassType = classType;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalValue = quantity * unitPrice;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private DeliveryProduct()
    {
    }

    public void UpdateQuantityAndPrice(decimal quantity, decimal unitPrice)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalValue = quantity * unitPrice;
    }
}
