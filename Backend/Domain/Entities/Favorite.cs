namespace Backend.Domain.Entities;

/// <summary>
/// Entidad que representa un favorito de usuario.
/// </summary>
public class Favorite
{
    /// <summary>
    /// Identificador único del favorito.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Identificador del usuario propietario.
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    /// Identificador del medio favorito.
    /// </summary>
    public Guid MediaItemId { get; set; }
    /// <summary>
    /// Navegación al medio asociado.
    /// </summary>
    public MediaItem? MediaItem { get; set; }
}
