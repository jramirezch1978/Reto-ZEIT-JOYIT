using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.API.Filters;
using BCrypt.Net;

namespace CleanArchitecture.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[ServiceFilter(typeof(ActiveUserRequirementFilter))]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        try
        {
            // Validar duplicidad
            if (await _userService.ExistsByUsernameOrEmailAsync(request.Username, request.Email))
            {
                return Conflict(new { codigo = 409, mensaje = "El usuario o email ya existe" });
            }
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };
            var id = await _userService.CreateAsync(user);
            user.Id = id;
            return CreatedAtAction(nameof(GetById), new { id }, new { user.Id, user.Username, user.Email, user.FirstName, user.LastName, user.Role, user.IsActive });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { codigo = 500, mensaje = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] User user)
    {
        if (id != user.Id) return BadRequest(new { codigo = 400, mensaje = "El id de la URL no coincide con el del usuario" });
        try
        {
            await _userService.UpdateAsync(user);
            return Ok(new { codigo = 0, mensaje = "ok" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { codigo = 500, mensaje = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _userService.DeleteAsync(id);
            return Ok(new { codigo = 0, mensaje = "ok" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { codigo = 500, mensaje = ex.Message });
        }
    }
} 