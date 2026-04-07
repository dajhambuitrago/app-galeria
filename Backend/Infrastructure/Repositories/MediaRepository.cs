using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// Repositorio EF Core para medios.
/// </summary>
public class MediaRepository(ApplicationDbContext dbContext) : IMediaRepository
{
    /// <summary>
    /// Filtra medios por categoría y tipo.
    /// </summary>
    public async Task<List<MediaItem>> FilterAsync(string? categoryName, MediaType? type, CancellationToken cancellationToken)
    {
        var query = dbContext.MediaItems
            .AsNoTracking()
            .Include(m => m.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            var normalized = categoryName.Trim();
            query = query.Where(x => x.Category != null && EF.Functions.ILike(x.Category.Name, normalized));
        }

        if (type.HasValue)
        {
            query = query.Where(x => x.Type == type.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtiene medios por ids únicos.
    /// </summary>
    public Task<List<MediaItem>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        var idList = ids.Distinct().ToList();
        if (idList.Count == 0)
        {
            return Task.FromResult(new List<MediaItem>());
        }

        return dbContext.MediaItems
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => idList.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtiene un medio por id.
    /// </summary>
    public Task<MediaItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        dbContext.MediaItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    /// <summary>
    /// Agrega un medio y persiste cambios.
    /// </summary>
    public async Task AddAsync(MediaItem media, CancellationToken cancellationToken)
    {
        dbContext.MediaItems.Add(media);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Actualiza un medio y persiste cambios.
    /// </summary>
    public async Task UpdateAsync(MediaItem media, CancellationToken cancellationToken)
    {
        dbContext.MediaItems.Update(media);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Elimina un medio y persiste cambios.
    /// </summary>
    public async Task DeleteAsync(MediaItem media, CancellationToken cancellationToken)
    {
        dbContext.MediaItems.Remove(media);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
