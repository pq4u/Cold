using Cold.Catalog.Core.Categories.Repositories;
using Cold.Catalog.Core.Entities;
using Cold.Catalog.Shared.Dtos;

namespace Cold.Catalog.Core.Services;

internal class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> GetAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        
        return product is null ? null : MapToDto(product);
    }
    public async Task<IReadOnlyList<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        
        return products.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<string>> GetNamesAsync(List<Guid> productIds)
        => (await _productRepository.GetByIdsAsync(productIds)).ToList();

    public async Task AddAsync(ProductDto dto)
    {
        if (await _productRepository.GetByNameAsync(dto.Name) is not null)
        {
            throw new ArgumentException("Product already exists");
        }
        
        var product = new Product(dto.Id, dto.Name, dto.Image, dto.CategoryId);
        await _productRepository.AddAsync(product);
    }
    
    public async Task UpdateAsync(ProductDto dto)
    {
        var product = new Product(dto.Id, dto.Name, dto.Image, dto.CategoryId);
        
        await _productRepository.UpdateAsync(product);
    }

    public async Task RemoveAsync(Guid productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null)
        {
            throw new ArgumentException("Product does not exist");
        }
        
        await _productRepository.DeleteAsync(product);
    }

    private static ProductDto MapToDto(Product product)
        => Map<ProductDto>(product);

    private static T Map<T>(Product product) where T : ProductDto, new()
        => new()
        {
            Id = product.Id,
            Name = product.Name,
            Image = product.Image,
            CategoryId = product.CategoryId
        };
}