using Cold.Packages.Core.Services;
using Cold.Packages.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Cold.Packages.Api.Controllers;

[ApiController]
[Route("api/packages")]
public class PackagesController : ControllerBase
{
    private readonly IPackageService _packageService;

    public PackagesController(IPackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PackageDto>> Get(Guid id)
    {
        var package = await _packageService.GetAsync(id);
        return package is null ? NotFound() : Ok(package);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PackageDto>>> GetAll()
    {
        return Ok(await _packageService.GetAllAsync());
    }

    [HttpPost]
    public async Task<ActionResult> Post(PackageDto dto)
    {
        var id = await _packageService.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, PackageDto dto)
    {
        dto.Id = id;
        await _packageService.UpdateAsync(dto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _packageService.DeleteAsync(id);
        return NoContent();
    }
}