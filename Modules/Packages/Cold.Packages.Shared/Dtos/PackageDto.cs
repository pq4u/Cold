namespace Cold.Packages.Shared.Dtos;

public class PackageDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
}