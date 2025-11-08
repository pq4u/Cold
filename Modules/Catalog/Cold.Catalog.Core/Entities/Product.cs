namespace Cold.Catalog.Core.Entities;

internal sealed class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Image { get; private set; }
    public Category Category { get; private set; }
    public Guid CategoryId { get; private set; }
    
    public ICollection<ProductPrice> Prices { get; private set; }
}