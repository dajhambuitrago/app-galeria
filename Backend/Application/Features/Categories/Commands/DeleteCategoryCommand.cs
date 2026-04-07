using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Common;
using MediatR;

namespace Backend.Application.Features.Categories.Commands;

/// <summary>
/// Comando para eliminar una categoría.
/// </summary>
public record DeleteCategoryCommand(Guid Id) : IRequest<Result>;

/// <summary>
/// Maneja la lógica de eliminación de categorías.
/// </summary>
public class DeleteCategoryCommandHandler(ICategoryRepository repository) : IRequestHandler<DeleteCategoryCommand, Result>
{
    /// <summary>
    /// Elimina la categoría si existe.
    /// </summary>
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            return Result.Failure("Categoría no encontrada.");
        }

        await repository.DeleteAsync(category, cancellationToken);
        return Result.Success();
    }
}
