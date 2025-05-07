using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.API.Filters;

namespace CleanArchitecture.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[ServiceFilter(typeof(ActiveUserRequirementFilter))]
public class PartnerController : ControllerBase
{
    private readonly PartnerService _partnerService;

    public PartnerController(PartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var partners = await _partnerService.GetAllAsync();
        return Ok(partners);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var partner = await _partnerService.GetByIdAsync(id);
        if (partner == null) return NotFound();
        return Ok(partner);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Partner partner)
    {
        var id = await _partnerService.CreateAsync(partner);
        return CreatedAtAction(nameof(GetById), new { id }, partner);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Partner partner)
    {
        if (id != partner.Id) return BadRequest();
        await _partnerService.UpdateAsync(partner);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _partnerService.DeleteAsync(id);
        return NoContent();
    }
} 