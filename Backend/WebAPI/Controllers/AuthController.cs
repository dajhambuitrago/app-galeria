using Backend.Application.Abstractions.Security;
using Backend.Application.DTOs;
using Backend.Infrastructure.Identity;
using Backend.WebAPI.Contracts.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtTokenService jwtTokenService) : ControllerBase
{
    /// <summary>
    /// Registra un usuario nuevo y devuelve el token JWT.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var userExists = await userManager.FindByEmailAsync(request.Email);
        if (userExists is not null)
        {
            return BadRequest(new { error = "El usuario ya existe." });
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            return BadRequest(new { error = createResult.Errors.First().Description });
        }

        await userManager.AddToRoleAsync(user, "User");
        var roles = await userManager.GetRolesAsync(user);
        var token = await jwtTokenService.GenerateTokenAsync(user.Id, user.Email!, roles);

        return Ok(new AuthResponseDto(token, user.Email!, roles));
    }

    /// <summary>
    /// Valida credenciales y devuelve el token JWT.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Unauthorized(new { error = "Credenciales inválidas." });
        }

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!signInResult.Succeeded)
        {
            return Unauthorized(new { error = "Credenciales inválidas." });
        }

        var roles = await userManager.GetRolesAsync(user);
        var token = await jwtTokenService.GenerateTokenAsync(user.Id, user.Email!, roles);

        return Ok(new AuthResponseDto(token, user.Email!, roles));
    }
}
