using Backend.Domain.Entities;

namespace Backend.Application.Abstractions.Repositories;

/// <summary>
/// Contrato para operaciones de persistencia de favoritos.
/// </summary>
public interface IFavoriteRepository
{
    /// <summary>
    /// Obtiene los favoritos por usuario.
    /// </summary>
    Task<List<Favorite>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    /// <summary>
    /// Obtiene un favorito por usuario y medio.
    /// </summary>
    Task<Favorite?> GetByUserIdAndMediaIdAsync(string userId, Guid mediaId, CancellationToken cancellationToken);
    /// <summary>
    /// Agrega un favorito.
    /// </summary>
    Task AddAsync(Favorite favorite, CancellationToken cancellationToken);
    /// <summary>
    /// Elimina un favorito.
    /// </summary>
    Task DeleteAsync(Favorite favorite, CancellationToken cancellationToken);
}
