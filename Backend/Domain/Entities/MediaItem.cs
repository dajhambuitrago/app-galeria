using Backend.Domain.Enums;

namespace Backend.Domain.Entities;

/// <summary>
/// Entidad que representa un medio de la galería.
/// </summary>
public class MediaItem
{
    /// <summary>
    /// Identificador único del medio.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Título visible del medio.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// URL pública del medio.
    /// </summary>
    public string Url { get; set; } = string.Empty;
    /// <summary>
    /// Descripción del medio.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Tipo de medio (imagen/video).
    /// </summary>
    public MediaType Type { get; set; }
    /// <summary>
    /// Identificador de la categoría asociada.
    /// </summary>
    public Guid CategoryId { get; set; }
    /// <summary>
    /// Navegación a la categoría asociada.
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// Favoritos vinculados al medio.
    /// </summary>
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
