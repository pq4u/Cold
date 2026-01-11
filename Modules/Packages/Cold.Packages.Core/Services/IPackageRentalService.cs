using Cold.Packages.Shared.Dtos;

namespace Cold.Packages.Core.Services;

public interface IPackageRentalService
{
    Task<Guid> RequestRentalAsync(CreatePackageRentalRequestDto dto);
    Task<PackageRentalDto?> GetAsync(Guid id);
    Task<IEnumerable<PackageRentalDto>> GetRequestedRentalsAsync();
    Task<IEnumerable<PackageRentalDto>> GetActiveRentalsAsync();
    Task ApproveRentalAsync(Guid rentalId);
    Task RejectRentalAsync(Guid rentalId);
    Task ReturnRentalAsync(Guid rentalId);
}