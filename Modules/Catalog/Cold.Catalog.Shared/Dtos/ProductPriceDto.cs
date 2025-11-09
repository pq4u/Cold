namespace Cold.Catalog.Shared.Dtos;

public class ProductPriceDto
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public string ClassType { get; set; }
    public DateTimeOffset DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
}