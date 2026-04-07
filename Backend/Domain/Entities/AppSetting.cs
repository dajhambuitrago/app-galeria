namespace Backend.Domain.Entities;

/// <summary>
/// Entidad que almacena configuraciones de la aplicación.
/// </summary>
public class AppSetting
{
    /// <summary>
    /// Identificador único de la configuración.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Duración del slideshow en segundos.
    /// </summary>
    public int SlideshowDurationSeconds { get; set; }
}
