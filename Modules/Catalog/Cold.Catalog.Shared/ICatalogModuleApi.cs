namespace Cold.Catalog.Shared;

public interface ICatalogModuleApi
{
    Task<IReadOnlyList<string>> GetProductsNamesAsync(List<Guid> productIds);
}