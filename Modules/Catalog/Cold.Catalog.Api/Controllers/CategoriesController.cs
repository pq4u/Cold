using Cold.Catalog.Core.Services;
using Cold.Catalog.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Cold.Catalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    
    [HttpGet("get")]
    [SwaggerOperation("Get specific category")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDto>> GetAsync([FromQuery] Guid id)
        => Ok(await _categoryService.GetAsync(id));
    
    [HttpGet("get-all")]
    [SwaggerOperation("Get all categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllAsync()
        => Ok(await _categoryService.GetAllAsync());


    [HttpPost("add")]
    [SwaggerOperation("Add new category")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> AddAsync(CategoryDto category)
    {
        await _categoryService.AddAsync(category);
        return CreatedAtAction(nameof(GetAsync), new { categoryId = category.Id }, null);
    }
    
    [HttpPut("update")]
    [SwaggerOperation("Update existing category")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> UpdateAsync(CategoryDto category)
    {
        await _categoryService.UpdateAsync(category);
        return Ok();
    }
    
    [HttpDelete("remove")]
    [SwaggerOperation("Remove category")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> AddAsync([FromQuery] Guid id)
    {
        await _categoryService.RemoveAsync(id);
        return Ok();
    }
}