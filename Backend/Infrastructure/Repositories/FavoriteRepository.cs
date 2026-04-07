using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// Repositorio EF Core para favoritos.
/// </summary>
public class FavoriteRepository(ApplicationDbContext dbContext) : IFavoriteRepository
{
    /// <summary>
    /// Obtiene favoritos del usuario e incluye el medio y su categoría.
    /// </summary>
    public Task<List<Favorite>> GetByUserIdAsync(string userId, CancellationToken cancellationToken) =>
        dbContext.Favorites
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Include(x => x.MediaItem)
                .ThenInclude(m => m!.Category)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Obtiene un favorito por usuario y medio.
    /// </summary>
    public Task<Favorite?> GetByUserIdAndMediaIdAsync(string userId, Guid mediaId, CancellationToken cancellationToken) =>
        dbContext.Favorites.FirstOrDefaultAsync(x => x.UserId == userId && x.MediaItemId == mediaId, cancellationToken);

    /// <summary>
    /// Agrega un favorito y persiste cambios.
    /// </summary>
    public async Task AddAsync(Favorite favorite, CancellationToken cancellationToken)
    {
        dbContext.Favorites.Add(favorite);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Elimina un favorito y persiste cambios.
    /// </summary>
    public async Task DeleteAsync(Favorite favorite, CancellationToken cancellationToken)
    {
        dbContext.Favorites.Remove(favorite);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
