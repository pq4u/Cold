namespace Cold.Catalog.Core.Entities;

internal sealed class ProductPrice
{
    public Product Product { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Price { get; private set; }
    public string ClassType { get; private set; }
    public DateTimeOffset DateFrom { get; private set; }
    public DateTimeOffset? DateTo { get; private set; }
    
    public ProductPrice(Guid productId, decimal price, string classType, DateTimeOffset dateFrom, DateTimeOffset? dateTo)
    {
        ProductId = productId;
        Price = price;
        ClassType = classType;
        DateFrom = dateFrom;
        DateTo = dateTo;
    }
}