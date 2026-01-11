namespace Cold.Packages.Shared.Dtos;

public class PackageRentalItemDto
{
    public Guid Id { get; set; }
    public Guid PackageId { get; set; }
    public string PackageName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}