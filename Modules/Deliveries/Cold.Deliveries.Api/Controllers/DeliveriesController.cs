using Cold.Deliveries.Core.Services;
using Cold.Deliveries.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Cold.Deliveries.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DeliveriesController : ControllerBase
{
    private readonly IDeliveryService _deliveryService;

    public DeliveriesController(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation("Get specific delivery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryDto>> GetAsync(Guid id)
    {
        var delivery = await _deliveryService.GetAsync(id);
        return delivery is null ? NotFound() : Ok(delivery);
    }

    [HttpGet("by-number/{deliveryNumber}")]
    [SwaggerOperation("Get delivery by delivery number")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryDto>> GetByNumberAsync(string deliveryNumber)
    {
        var delivery = await _deliveryService.GetByDeliveryNumberAsync(deliveryNumber);
        return delivery is null ? NotFound() : Ok(delivery);
    }

    [HttpGet]
    [SwaggerOperation("Get all deliveries")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<DeliveryDto>>> GetAllAsync()
        => Ok(await _deliveryService.GetAllAsync());

    [HttpGet("by-supplier/{supplierId:guid}")]
    [SwaggerOperation("Get deliveries by supplier")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<DeliveryDto>>> GetBySupplierAsync(Guid supplierId)
        => Ok(await _deliveryService.GetBySupplierIdAsync(supplierId));

    [HttpGet("uninvoiced")]
    [SwaggerOperation("Get all uninvoiced deliveries")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<DeliveryDto>>> GetUninvoicedAsync()
        => Ok(await _deliveryService.GetUninvoicedAsync());

    [HttpGet("uninvoiced/by-supplier/{supplierId:guid}")]
    [SwaggerOperation("Get uninvoiced deliveries by supplier")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<DeliveryDto>>> GetUninvoicedBySupplierAsync(Guid supplierId)
        => Ok(await _deliveryService.GetUninvoicedBySupplierIdAsync(supplierId));

    [HttpPost]
    [SwaggerOperation("Create new delivery")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateAsync([FromBody] CreateDeliveryDto dto)
    {
        try
        {
            var deliveryId = await _deliveryService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAsync), new { id = deliveryId }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation("Update delivery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] CreateDeliveryDto dto)
    {
        try
        {
            await _deliveryService.UpdateAsync(id, dto);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{id:guid}/mark-invoiced")]
    [SwaggerOperation("Mark delivery as invoiced")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> MarkAsInvoicedAsync(Guid id)
    {
        try
        {
            await _deliveryService.MarkAsInvoicedAsync(id);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
