using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AppCore.Dto;
using AppCore.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GatesController(IParkingGateService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllGates([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        return Ok(await service.GetAllPaged(page, size));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await service.GetById(id);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await service.GetByName(name);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGateDto dto)
    {
        var result = await service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeOperationalStatus(Guid id, [FromQuery] bool status)
    {
        var result = await service.ChangeOperationalStatus(id, status);
        return result is not null ? Ok(result) : NotFound();
    }
}

