namespace Cold.Catalog.Shared.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public Guid CategoryId { get; set; }
}