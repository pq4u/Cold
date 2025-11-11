namespace Cold.Deliveries.Core.Entities;

internal sealed class DeliveryPhoto
{
    public Guid Id { get; private set; }
    public Guid DeliveryId { get; private set; }
    public Delivery Delivery { get; private set; }
    public string FilePath { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public DeliveryPhoto(Guid id, Guid deliveryId, string filePath, string? description)
    {
        Id = id;
        DeliveryId = deliveryId;
        FilePath = filePath;
        Description = description;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private DeliveryPhoto()
    {
    }
}
