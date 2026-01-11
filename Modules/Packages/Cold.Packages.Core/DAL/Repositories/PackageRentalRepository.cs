using System.Linq.Expressions;
using Cold.Packages.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cold.Packages.Core.DAL.Repositories;

public class PackageRentalRepository : IPackageRentalRepository
{
    private readonly PackagesDbContext _context;

    public PackageRentalRepository(PackagesDbContext context)
    {
        _context = context;
    }

    public async Task<PackageRental?> GetAsync(Guid id)
        => await _context.PackageRentals
            .Include(pr => pr.Items)
            .ThenInclude(i => i.Package)
            .SingleOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<PackageRental>> GetAllAsync(Expression<Func<PackageRental, bool>>? filter = null)
    {
        var query = _context.PackageRentals
            .Include(pr => pr.Items)
            .ThenInclude(i => i.Package)
            .AsQueryable();

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }


    public async Task AddAsync(PackageRental packageRental)
    {
        await _context.PackageRentals.AddAsync(packageRental);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PackageRental packageRental)
    {
        _context.PackageRentals.Update(packageRental);
        await _context.SaveChangesAsync();
    }
}