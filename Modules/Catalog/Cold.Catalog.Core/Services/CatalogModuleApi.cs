using Cold.Catalog.Shared;

namespace Cold.Catalog.Core.Services;

public class CatalogModuleApi : ICatalogModuleApi
{
    private readonly IProductService _productService;

    public CatalogModuleApi(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IReadOnlyList<string>> GetProductsNamesAsync(List<Guid> productIds)
        => await _productService.GetNamesAsync(productIds);
}