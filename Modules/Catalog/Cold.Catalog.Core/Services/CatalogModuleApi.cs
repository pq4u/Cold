using Cold.Catalog.Shared;

namespace Cold.Catalog.Core.Services;

public class CatalogModuleApi : ICatalogModuleApi
{
    private readonly IProductService _productService;
    private readonly IProductPriceService _productPriceService;

    public CatalogModuleApi(IProductService productService, IProductPriceService productPriceService)
    {
        _productService = productService;
        _productPriceService = productPriceService;
    }

    public async Task<IReadOnlyList<string>> GetProductsNamesAsync(List<Guid> productIds)
        => await _productService.GetNamesAsync(productIds);

    public async Task<decimal?> GetProductPriceAsync(Guid productId, string classType, DateTimeOffset date)
        => await _productPriceService.GetPriceAsync(productId, classType, date);
}