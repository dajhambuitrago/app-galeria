using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// Repositorio EF Core para categorías.
/// </summary>
public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{
    /// <summary>
    /// Obtiene todas las categorías ordenadas por nombre.
    /// </summary>
    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken) =>
        dbContext.Categories.OrderBy(x => x.Name).ToListAsync(cancellationToken);

    /// <summary>
    /// Obtiene una categoría por id.
    /// </summary>
    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    /// <summary>
    /// Obtiene una categoría por nombre.
    /// </summary>
    public Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken) =>
        dbContext.Categories.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);

    /// <summary>
    /// Agrega una categoría y persiste cambios.
    /// </summary>
    public async Task AddAsync(Category category, CancellationToken cancellationToken)
    {
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Actualiza una categoría y persiste cambios.
    /// </summary>
    public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Elimina una categoría y persiste cambios.
    /// </summary>
    public async Task DeleteAsync(Category category, CancellationToken cancellationToken)
    {
        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
