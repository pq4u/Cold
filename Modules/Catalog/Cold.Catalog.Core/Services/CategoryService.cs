using Cold.Catalog.Core.Categories.Repositories;
using Cold.Catalog.Core.Entities;
using Cold.Catalog.Shared.Dtos;

namespace Cold.Catalog.Core.Services;

internal class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> GetAsync(Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        
        return category is null ? null : MapToDto(category);
    }

    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        
        return categories.Select(MapToDto).ToList();
    }

    public async Task AddAsync(CategoryDto dto)
    {
        if (await _categoryRepository.GetByNameAsync(dto.Name) is not null)
        {
            throw new ArgumentException("Category already exists");
        }
        
        var category = new Category(dto.Id, dto.Name, dto.Image);
        await _categoryRepository.AddAsync(category);
    }
    
    public async Task UpdateAsync(CategoryDto dto)
    {
        var category = new Category(dto.Id, dto.Name, dto.Image);
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task RemoveAsync(Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null)
        {
            throw new ArgumentException("Category does not exist");
        }
        
        
        await _categoryRepository.DeleteAsync(category);
    }

    private static CategoryDto MapToDto(Category category)
        => Map<CategoryDto>(category);

    private static T Map<T>(Category category) where T : CategoryDto, new()
        => new()
        {
            Id = category.Id,
            Name = category.Name,
            Image = category.Image
        };
}