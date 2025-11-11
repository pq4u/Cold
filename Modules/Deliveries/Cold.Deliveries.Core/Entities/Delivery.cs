namespace Cold.Deliveries.Core.Entities;

internal sealed class Delivery
{
    public Guid Id { get; private set; }
    public string DeliveryNumber { get; private set; }
    public Guid SupplierId { get; private set; }
    public DateTimeOffset DeliveryDate { get; private set; }
    public decimal TotalValue { get; private set; }
    public string? Notes { get; private set; }
    public bool IsInvoiced { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public ICollection<DeliveryProduct> DeliveryProducts { get; private set; }
    public ICollection<DeliveryPhoto> Photos { get; private set; }
    public ICollection<TransportRequest> TransportRequests { get; private set; }

    public Delivery(Guid id, string deliveryNumber, Guid supplierId, DateTimeOffset deliveryDate,
                   decimal totalValue, string? notes)
    {
        Id = id;
        DeliveryNumber = deliveryNumber;
        SupplierId = supplierId;
        DeliveryDate = deliveryDate;
        TotalValue = totalValue;
        Notes = notes;
        IsInvoiced = false;
        CreatedAt = DateTimeOffset.UtcNow;
        DeliveryProducts = new HashSet<DeliveryProduct>();
        Photos = new HashSet<DeliveryPhoto>();
        TransportRequests = new HashSet<TransportRequest>();
    }

    private Delivery()
    {
    }

    public void MarkAsInvoiced()
    {
        IsInvoiced = true;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateTotalValue(decimal totalValue)
    {
        TotalValue = totalValue;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Update(DateTimeOffset deliveryDate, string? notes)
    {
        DeliveryDate = deliveryDate;
        Notes = notes;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
