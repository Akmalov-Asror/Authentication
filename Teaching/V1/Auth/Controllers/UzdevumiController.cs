using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Teaching.Common.Entities.Dto_s;
using Teaching.V1.Auth.Services.Interfaces;

namespace Teaching.V1.Auth.Controllers;

[ApiController]
[ApiVersion("3.0")]
//[ApiVersion("3.0",Deprecated = true)]
[Route("api/v{version:apiVersion}/[controller]")]

public class UzdevumiController : ControllerBase
{
    private readonly IUzdevumi _uzdevumiService;

    public UzdevumiController(IUzdevumi uzdevumiService)
        => _uzdevumiService = uzdevumiService;
    // GET: api/uzdevumi/filter
    [MapToApiVersion("3.0")]
    [HttpGet("filter")]
    public async Task<ActionResult<List<UzdevumiDto>>> GetByFilter()
    {
        var result = await _uzdevumiService.GetByFilter();
        return Ok(result);
    }
    // POST: api/uzdevumi
    [MapToApiVersion("3.0")]
    [HttpPost]
    public async Task<ActionResult<CreateDto>> Post([FromBody] CreateDto dto)
    {
        var result = await _uzdevumiService.Create(dto);
        return CreatedAtAction(nameof(GetByFilter), result);
    }
    // PUT: api/uzdevumi
    [MapToApiVersion("3.0")]
    [HttpPut]
    public async Task<IActionResult> Put(Guid id ,[FromBody] UpdateUzdevumiDto dto)
    {
        var result = await _uzdevumiService.UpdateUzdevumi(id,dto);
        return NoContent();
    }
}
