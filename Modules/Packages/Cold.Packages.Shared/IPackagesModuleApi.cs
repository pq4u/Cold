using Cold.Packages.Shared.Dtos;

namespace Cold.Packages.Shared;

public interface IPackagesModuleApi
{
    Task<PackageDto?> GetPackageAsync(Guid packageId);
}