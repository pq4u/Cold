namespace Cold.Catalog.Core.Entities;

internal sealed class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Image { get; private set; }

    public IEnumerable<Product>? Products { get; private set; }
    

    public Category(Guid id, string name, string image)
    {
        Id = id;
        Name = name;
        Image = image;
    }
}