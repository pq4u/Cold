using Cold.Packages.Core.Services;
using Cold.Packages.Shared;
using Cold.Packages.Shared.Dtos;

namespace Cold.Packages.Core.Services;

internal class PackagesModuleApi : IPackagesModuleApi
{
    private readonly IPackageService _packageService;

    public PackagesModuleApi(IPackageService packageService)
    {
        _packageService = packageService;
    }

    public Task<PackageDto?> GetPackageAsync(Guid packageId)
        => _packageService.GetAsync(packageId);
}