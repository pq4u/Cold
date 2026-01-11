using System.Linq.Expressions;
using Cold.Packages.Core.Entities;

namespace Cold.Packages.Core.DAL.Repositories;

public interface IPackageRentalRepository
{
    Task<PackageRental?> GetAsync(Guid id);
    Task<IEnumerable<PackageRental>> GetAllAsync(Expression<Func<PackageRental, bool>>? filter = null);
    Task AddAsync(PackageRental packageRental);
    Task UpdateAsync(PackageRental packageRental);
}