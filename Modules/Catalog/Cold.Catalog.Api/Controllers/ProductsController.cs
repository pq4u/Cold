using Cold.Catalog.Core.Services;
using Cold.Catalog.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Cold.Catalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet("get")]
    [SwaggerOperation("Get specific product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> GetAsync([FromQuery] Guid id)
        => Ok(await _productService.GetAsync(id));
    
    [HttpGet("get-all")]
    [SwaggerOperation("Get all products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync()
        => Ok(await _productService.GetAllAsync());


    [HttpPost("add")]
    [SwaggerOperation("Add new product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> AddAsync(ProductDto product)
    {
        await _productService.AddAsync(product);
        return CreatedAtAction(nameof(GetAsync), new { productId = product.Id }, null);
    }
    
    [HttpPut("update")]
    [SwaggerOperation("Update existing product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> UpdateAsync(ProductDto product)
    {
        await _productService.UpdateAsync(product);
        return Ok();
    }
    
    [HttpDelete("remove")]
    [SwaggerOperation("Remove product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> AddAsync([FromQuery] Guid id)
    {
        await _productService.RemoveAsync(id);
        return Ok();
    }
}