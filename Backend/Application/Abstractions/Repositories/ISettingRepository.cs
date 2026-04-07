using Backend.Domain.Entities;

namespace Backend.Application.Abstractions.Repositories;

/// <summary>
/// Contrato para operaciones de configuración.
/// </summary>
public interface ISettingRepository
{
    /// <summary>
    /// Obtiene la configuración actual.
    /// </summary>
    Task<AppSetting?> GetAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Inserta o actualiza la duración del slideshow.
    /// </summary>
    Task<AppSetting> UpsertSlideshowDurationAsync(int slideshowDurationSeconds, CancellationToken cancellationToken);
}
