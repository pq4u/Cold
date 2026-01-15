using Cold.Packages.Core.Services;
using Cold.Packages.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Cold.Packages.Api.Controllers;

[ApiController]
[Route("api/package-rentals")]
public class PackageRentalsController : ControllerBase
{
    private readonly IPackageRentalService _rentalService;

    public PackageRentalsController(IPackageRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PackageRentalDto>> Get(Guid id)
    {
        var rental = await _rentalService.GetAsync(id);
        return rental is null ? NotFound() : Ok(rental);
    }

    [HttpGet("requested")]
    public async Task<ActionResult<IEnumerable<PackageRentalDto>>> GetRequested()
    {
        return Ok(await _rentalService.GetRequestedRentalsAsync());
    }
    
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<PackageRentalDto>>> GetActive()
    {
        return Ok(await _rentalService.GetActiveRentalsAsync());
    }

    [HttpPost("request")]
    public async Task<ActionResult> RequestRental(CreatePackageRentalRequestDto dto)
    {
        var id = await _rentalService.RequestRentalAsync(dto);
        return Ok();
        return CreatedAtAction(nameof(Get), new { id }, null);
    }
    
    [HttpPost("{id:guid}/approve")]
    public async Task<ActionResult> Approve(Guid id)
    {
        await _rentalService.ApproveRentalAsync(id);
        return NoContent();
    }
    
    [HttpPost("{id:guid}/reject")]
    public async Task<ActionResult> Reject(Guid id)
    {
        await _rentalService.RejectRentalAsync(id);
        return NoContent();
    }
    
    [HttpPost("{id:guid}/return")]
    public async Task<ActionResult> Return(Guid id)
    {
        await _rentalService.ReturnRentalAsync(id);
        return NoContent();
    }
}