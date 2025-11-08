namespace Cold.Catalog.Core.Entities;

internal sealed class ProductPrice
{
    public Product Product { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Price { get; private set; }
    public string Class { get; private set; }
    public DateTimeOffset Date { get; private set; }
}