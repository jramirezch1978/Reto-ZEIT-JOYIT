using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.API.Filters;
using System.Security.Claims;

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
    public async Task<IActionResult> Create([FromBody] CreatePartnerRequest request)
    {
        try
        {
            // Obtener el ID del usuario del token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new { codigo = 401, mensaje = "Token inválido: no contiene ID de usuario" });
            }

            // Crear el objeto Partner con los datos del request
            var partner = new Partner
            {
                RazonSocial = request.RazonSocial,
                TaxId = request.TaxId,
                Type = request.Type,
                ContactName = request.ContactName,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Country = request.Country,
                PostalCode = request.PostalCode,
                IsActive = request.IsActive,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            var id = await _partnerService.CreateAsync(partner);
            partner.Id = id;
            return CreatedAtAction(nameof(GetById), new { id }, new { partner.Id, partner.RazonSocial, partner.TaxId, partner.Type, partner.ContactName, partner.ContactEmail, partner.ContactPhone, partner.Address, partner.City, partner.State, partner.Country, partner.PostalCode, partner.IsActive, partner.CreatedById, partner.CreatedAt, partner.UpdatedAt });
        }
        catch (Exception ex)
        {
            var errorMessage = DatabaseErrorHandler.FormatErrorMessage(ex.Message);
            return StatusCode(500, new { codigo = 500, mensaje = errorMessage });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Partner partner)
    {
        if (id != partner.Id) return BadRequest(new { codigo = 400, mensaje = "El id de la URL no coincide con el del proveedor" });
        try
        {
            // Obtener el ID del usuario del token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new { codigo = 401, mensaje = "Token inválido: no contiene ID de usuario" });
            }

            // Obtener el proveedor existente para mantener el CreatedById original
            var existingPartner = await _partnerService.GetByIdAsync(id);
            if (existingPartner == null)
            {
                return NotFound(new { codigo = 404, mensaje = "Proveedor no encontrado" });
            }

            // Mantener el CreatedById original y actualizar UpdatedAt
            partner.CreatedById = existingPartner.CreatedById;
            partner.CreatedAt = existingPartner.CreatedAt;
            partner.UpdatedAt = DateTime.UtcNow;

            await _partnerService.UpdateAsync(partner);
            return Ok(new { codigo = 0, mensaje = "ok" });
        }
        catch (Exception ex)
        {
            var errorMessage = DatabaseErrorHandler.FormatErrorMessage(ex.Message);
            return StatusCode(500, new { codigo = 500, mensaje = errorMessage });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _partnerService.DeleteAsync(id);
            return Ok(new { codigo = 0, mensaje = "ok" });
        }
        catch (Exception ex)
        {
            var errorMessage = DatabaseErrorHandler.FormatErrorMessage(ex.Message);
            return StatusCode(500, new { codigo = 500, mensaje = errorMessage });
        }
    }
} 