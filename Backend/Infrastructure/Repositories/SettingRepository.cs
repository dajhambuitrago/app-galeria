using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// Repositorio EF Core para configuración de la aplicación.
/// </summary>
public class SettingRepository(ApplicationDbContext dbContext) : ISettingRepository
{
    /// <summary>
    /// Obtiene la configuración actual.
    /// </summary>
    public Task<AppSetting?> GetAsync(CancellationToken cancellationToken) =>
        dbContext.AppSettings.AsNoTracking().FirstOrDefaultAsync(cancellationToken);

    /// <summary>
    /// Inserta o actualiza la duración del slideshow.
    /// </summary>
    public async Task<AppSetting> UpsertSlideshowDurationAsync(int slideshowDurationSeconds, CancellationToken cancellationToken)
    {
        var setting = await dbContext.AppSettings.FirstOrDefaultAsync(cancellationToken);
        if (setting is null)
        {
            setting = new AppSetting { SlideshowDurationSeconds = slideshowDurationSeconds };
            dbContext.AppSettings.Add(setting);
        }
        else
        {
            setting.SlideshowDurationSeconds = slideshowDurationSeconds;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return setting;
    }
}
