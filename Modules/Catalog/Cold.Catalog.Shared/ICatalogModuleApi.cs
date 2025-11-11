namespace Cold.Catalog.Shared;

public interface ICatalogModuleApi
{
    Task<IReadOnlyList<string>> GetProductsNamesAsync(List<Guid> productIds);
    Task<decimal?> GetProductPriceAsync(Guid productId, string classType, DateTimeOffset date);
}