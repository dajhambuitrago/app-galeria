namespace Backend.WebAPI.Contracts.Auth;

/// <summary>
/// Datos requeridos para registrar un usuario.
/// </summary>
public record RegisterRequest(string Email, string Password);
