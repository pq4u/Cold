using Cold.Contracts.Core.Services;
using Cold.Contracts.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Cold.Contracts.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpGet("get")]
    [SwaggerOperation("Get specific contract")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContractDto>> GetAsync([FromQuery] Guid id)
        => Ok(await _contractService.GetAsync(id));

    [HttpGet("get-all")]
    [SwaggerOperation("Get all contracts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ContractDto>>> GetAllAsync()
        => Ok(await _contractService.GetAllAsync());

    [HttpPost("add")]
    [SwaggerOperation("Add new contract")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddAsync(ContractDto contract)
    {
        await _contractService.AddAsync(contract);
        return CreatedAtAction(nameof(GetAsync), new { id = contract.Id }, contract);
    }

    [HttpPut("update")]
    [SwaggerOperation("Update existing contract")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateAsync(ContractDto contract)
    {
        await _contractService.UpdateAsync(contract);
        return Ok();
    }
}