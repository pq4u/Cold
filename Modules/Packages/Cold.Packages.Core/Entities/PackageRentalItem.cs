namespace Cold.Packages.Core.Entities;

public class PackageRentalItem
{
    public Guid Id { get; set; }
    public Guid PackageRentalId { get; set; }
    public PackageRental PackageRental { get; set; } = null!;
    public Guid PackageId { get; set; }
    public Package Package { get; set; } = null!;
    public int Quantity { get; set; }
}