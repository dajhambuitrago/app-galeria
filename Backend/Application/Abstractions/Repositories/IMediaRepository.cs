using Backend.Domain.Entities;
using Backend.Domain.Enums;

namespace Backend.Application.Abstractions.Repositories;

/// <summary>
/// Contrato para operaciones de persistencia de medios.
/// </summary>
public interface IMediaRepository
{
    /// <summary>
    /// Filtra medios por categoría y tipo.
    /// </summary>
    Task<List<MediaItem>> FilterAsync(string? categoryName, MediaType? type, CancellationToken cancellationToken);
    /// <summary>
    /// Obtiene medios por una lista de ids.
    /// </summary>
    Task<List<MediaItem>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    /// <summary>
    /// Obtiene un medio por id.
    /// </summary>
    Task<MediaItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Agrega un medio.
    /// </summary>
    Task AddAsync(MediaItem media, CancellationToken cancellationToken);
    /// <summary>
    /// Actualiza un medio.
    /// </summary>
    Task UpdateAsync(MediaItem media, CancellationToken cancellationToken);
    /// <summary>
    /// Elimina un medio.
    /// </summary>
    Task DeleteAsync(MediaItem media, CancellationToken cancellationToken);
}
