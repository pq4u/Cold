using Cold.Catalog.Core.Services;
using Cold.Catalog.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Cold.Catalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductPricesController : ControllerBase
{
    private readonly IProductPriceService _productPriceService;

    public ProductPricesController(IProductPriceService productPriceService)
    {
        _productPriceService = productPriceService;
    }
    
    [HttpGet("get")]
    [SwaggerOperation("Get specific product prices")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductPriceDto>>> GetAsync([FromQuery] Guid productId)
        => Ok(await _productPriceService.GetByProductIdAsync(productId));
    
    [HttpGet("get-all")]
    [SwaggerOperation("Get all products prices")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductPriceDto>>> GetAllAsync()
        => Ok(await _productPriceService.GetAllAsync());

    [HttpPost("add")]
    [SwaggerOperation("Add new product price")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddAsync(ProductPriceDto productPrice)
    {
        await _productPriceService.AddAsync(productPrice);
        return CreatedAtAction(nameof(GetAsync), new { productId = productPrice.ProductId });
    }

    [HttpPut("update")]
    [SwaggerOperation("Update existing product price")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateAsync(ProductPriceDto productPrice)
    {
        await _productPriceService.UpdateAsync(productPrice);
        return Ok();
    }
}