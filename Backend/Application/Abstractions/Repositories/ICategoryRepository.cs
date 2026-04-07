using Backend.Domain.Entities;

namespace Backend.Application.Abstractions.Repositories;

/// <summary>
/// Contrato para operaciones de persistencia de categorías.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Obtiene todas las categorías.
    /// </summary>
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Obtiene una categoría por id.
    /// </summary>
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Obtiene una categoría por nombre.
    /// </summary>
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken);
    /// <summary>
    /// Agrega una categoría.
    /// </summary>
    Task AddAsync(Category category, CancellationToken cancellationToken);
    /// <summary>
    /// Actualiza una categoría.
    /// </summary>
    Task UpdateAsync(Category category, CancellationToken cancellationToken);
    /// <summary>
    /// Elimina una categoría.
    /// </summary>
    Task DeleteAsync(Category category, CancellationToken cancellationToken);
}
