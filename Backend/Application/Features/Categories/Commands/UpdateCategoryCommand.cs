using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Common;
using FluentValidation;
using MediatR;

namespace Backend.Application.Features.Categories.Commands;

/// <summary>
/// Comando para actualizar una categoría.
/// </summary>
public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Result>;

/// <summary>
/// Valida el comando de actualización de categorías.
/// </summary>
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}

/// <summary>
/// Maneja la lógica de actualización de categorías.
/// </summary>
public class UpdateCategoryCommandHandler(ICategoryRepository repository, IValidator<UpdateCategoryCommand> validator) : IRequestHandler<UpdateCategoryCommand, Result>
{
    /// <summary>
    /// Valida la solicitud y actualiza la categoría.
    /// </summary>
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Result.Failure(validation.Errors.First().ErrorMessage);
        }

        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            return Result.Failure("Categoría no encontrada.");
        }

        category.Name = request.Name.Trim();
        await repository.UpdateAsync(category, cancellationToken);
        return Result.Success();
    }
}
