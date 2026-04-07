namespace Backend.Application.Abstractions.Security;

/// <summary>
/// Contrato para generación de tokens JWT.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Genera un token firmado con los datos y roles del usuario.
    /// </summary>
    Task<string> GenerateTokenAsync(string userId, string email, IEnumerable<string> roles);
}
