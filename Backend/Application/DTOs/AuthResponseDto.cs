namespace Backend.Application.DTOs;

/// <summary>
/// Información de autenticación devuelta al iniciar sesión o registrarse.
/// </summary>
public record AuthResponseDto(string Token, string Email, IEnumerable<string> Roles);
