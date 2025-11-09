using Cold.Catalog.Core.Categories.Repositories;
using Cold.Catalog.Core.Entities;
using Cold.Catalog.Shared.Dtos;

namespace Cold.Catalog.Core.Services;

internal class ProductPriceService : IProductPriceService
{
    private readonly IProductPriceRepository _productPriceRepository;

    public ProductPriceService(IProductPriceRepository productPriceRepository)
    {
        _productPriceRepository = productPriceRepository;
    }

    public async Task<IReadOnlyList<ProductPriceDto>> GetByProductIdAsync(Guid productId)
    {
        var productPrices = await _productPriceRepository.GetByProductIdAsync(productId);
        var result = productPrices.Select(x => MapToDto(x)).ToList();
        return productPrices.Any() ? result : null;
    }

    public async Task<IReadOnlyList<ProductPriceDto>> GetAllAsync()
    {
        var products = await _productPriceRepository.GetAllAsync();
        
        return products.Select(MapToDto).ToList();
    }

    public async Task AddAsync(ProductPriceDto dto)
    {
        if (await _productPriceRepository.GetByProductIdAsync(dto.ProductId) is null)
        {
            throw new ArgumentException("Product does not exist");
        }
        
        var productPrice = new ProductPrice(dto.ProductId, dto.Price, dto.ClassType, dto.DateFrom, dto.DateTo);
        await _productPriceRepository.AddAsync(productPrice);
    }
    
    public async Task UpdateAsync(ProductPriceDto dto)
    {
        var productPrice = new ProductPrice(dto.ProductId, dto.Price, dto.ClassType, dto.DateFrom, dto.DateTo);
        await _productPriceRepository.UpdateAsync(productPrice);
    }

    private static ProductPriceDto MapToDto(ProductPrice productPrice)
        => Map<ProductPriceDto>(productPrice);

    private static T Map<T>(ProductPrice productPrice) where T : ProductPriceDto, new()
        => new()
        {
            ProductId = productPrice.ProductId,
            Price = productPrice.Price,
            ClassType = productPrice.ClassType,
            DateFrom = productPrice.DateFrom,
            DateTo = productPrice.DateTo
        };
}