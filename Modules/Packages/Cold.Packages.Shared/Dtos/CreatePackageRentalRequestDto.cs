namespace Cold.Packages.Shared.Dtos;

public class CreatePackageRentalRequestDto
{
    public Guid SupplierId { get; set; }
    public ICollection<PackageRentalRequestItemDto> Items { get; set; } = new List<PackageRentalRequestItemDto>();
}