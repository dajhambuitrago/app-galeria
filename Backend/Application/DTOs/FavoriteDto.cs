namespace Backend.Application.DTOs;

/// <summary>
/// Representa un favorito del usuario.
/// </summary>
public record FavoriteDto(Guid MediaItemId, string Title, string Url);
