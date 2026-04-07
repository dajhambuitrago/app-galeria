namespace Backend.Domain.Entities;

/// <summary>
/// Entidad de categoría para clasificar medios.
/// </summary>
public class Category
{
    /// <summary>
    /// Identificador único de la categoría.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Nombre visible de la categoría.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Medios asociados a la categoría.
    /// </summary>
    public ICollection<MediaItem> MediaItems { get; set; } = new List<MediaItem>();
}
