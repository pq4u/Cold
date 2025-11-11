using Cold.Deliveries.Core.Services;
using Cold.Deliveries.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Cold.Deliveries.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TransportRequestsController : ControllerBase
{
    private readonly ITransportRequestService _transportRequestService;

    public TransportRequestsController(ITransportRequestService transportRequestService)
    {
        _transportRequestService = transportRequestService;
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation("Get specific transport request")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransportRequestDto>> GetAsync(Guid id)
    {
        var request = await _transportRequestService.GetAsync(id);
        return request is null ? NotFound() : Ok(request);
    }

    [HttpGet]
    [SwaggerOperation("Get all transport requests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TransportRequestDto>>> GetAllAsync()
        => Ok(await _transportRequestService.GetAllAsync());

    [HttpGet("by-supplier/{supplierId:guid}")]
    [SwaggerOperation("Get transport requests by supplier")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TransportRequestDto>>> GetBySupplierAsync(Guid supplierId)
        => Ok(await _transportRequestService.GetBySupplierIdAsync(supplierId));

    [HttpGet("by-status/{statusId}")]
    [SwaggerOperation("Get transport requests by status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TransportRequestDto>>> GetByStatusAsync(short statusId)
        => Ok(await _transportRequestService.GetByStatusAsync(statusId));

    [HttpGet("statuses")]
    [SwaggerOperation("Get all transport statuses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TransportStatusDto>>> GetAllStatusesAsync()
        => Ok(await _transportRequestService.GetAllStatusesAsync());

    [HttpPost]
    [SwaggerOperation("Create new transport request")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateAsync([FromBody] CreateTransportRequestDto dto)
    {
        var requestId = await _transportRequestService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAsync), new { id = requestId }, null);
    }

    [HttpPatch("{id:guid}/status")]
    [SwaggerOperation("Update transport request status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateStatusAsync(Guid id, [FromBody] UpdateTransportStatusDto dto)
    {
        try
        {
            await _transportRequestService.UpdateStatusAsync(id, dto);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("{id:guid}")]
    [SwaggerOperation("Update transport request details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromQuery] DateTimeOffset? scheduledPickupDate, [FromQuery] string? notes)
    {
        try
        {
            await _transportRequestService.UpdateAsync(id, scheduledPickupDate, notes);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("{id:guid}/link-delivery")]
    [SwaggerOperation("Link transport request to delivery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> LinkToDeliveryAsync(Guid id, [FromBody] LinkTransportRequestToDeliveryDto dto)
    {
        try
        {
            await _transportRequestService.LinkToDeliveryAsync(id, dto.DeliveryId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
