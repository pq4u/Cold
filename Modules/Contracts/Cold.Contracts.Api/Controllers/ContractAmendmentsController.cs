using Cold.Contracts.Core.Services;
using Cold.Contracts.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Cold.Contracts.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContractAmendmentsController : ControllerBase
{
    private readonly IContractAmendmentService _amendmentService;

    public ContractAmendmentsController(IContractAmendmentService amendmentService)
    {
        _amendmentService = amendmentService;
    }

    [HttpGet("get")]
    [SwaggerOperation("Get specific amendment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContractAmendmentDto>> GetAsync([FromQuery] Guid id)
        => Ok(await _amendmentService.GetAsync(id));

    [HttpGet("get-by-contract")]
    [SwaggerOperation("Get all amendments for a contract")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ContractAmendmentDto>>> GetByContractIdAsync([FromQuery] Guid contractId)
        => Ok(await _amendmentService.GetByContractIdAsync(contractId));

    [HttpGet("get-all")]
    [SwaggerOperation("Get all amendments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ContractAmendmentDto>>> GetAllAsync()
        => Ok(await _amendmentService.GetAllAsync());

    [HttpPost("add")]
    [SwaggerOperation("Add new amendment")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddAsync(ContractAmendmentDto amendment)
    {
        await _amendmentService.AddAsync(amendment);
        return CreatedAtAction(nameof(GetAsync), new { id = amendment.Id }, amendment);
    }
}