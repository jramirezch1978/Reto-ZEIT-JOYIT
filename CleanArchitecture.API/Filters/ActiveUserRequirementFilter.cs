using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using CleanArchitecture.Application.Services;

namespace CleanArchitecture.API.Filters;

public class ActiveUserRequirementFilter : IAsyncActionFilter
{
    private readonly UserService _userService;
    public ActiveUserRequirementFilter(UserService userService)
    {
        _userService = userService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Token inválido: no contiene ID de usuario" });
            return;
        }
        if (!int.TryParse(userIdClaim.Value, out int userId))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Token inválido: ID de usuario no es válido" });
            return;
        }
        var user = await _userService.GetByIdAsync(userId);
        if (user == null || !user.IsActive)
        {
            context.Result = new UnauthorizedObjectResult(new { message = "El usuario no está activo o no existe" });
            return;
        }
        await next();
    }
} 