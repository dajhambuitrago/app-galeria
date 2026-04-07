using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Common;
using Backend.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Backend.Application.Features.Categories.Commands;

/// <summary>
/// Comando para crear una categoría.
/// </summary>
public record CreateCategoryCommand(string Name) : IRequest<Result<Guid>>;

/// <summary>
/// Valida el comando de creación de categorías.
/// </summary>
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}

/// <summary>
/// Maneja la lógica de creación de categorías.
/// </summary>
public class CreateCategoryCommandHandler(ICategoryRepository repository, IValidator<CreateCategoryCommand> validator) : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    /// <summary>
    /// Valida la solicitud y guarda la categoría.
    /// </summary>
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<Guid>.Failure(validation.Errors.First().ErrorMessage);
        }

        var existing = await repository.GetByNameAsync(request.Name, cancellationToken);
        if (existing is not null)
        {
            return Result<Guid>.Failure("La categoría ya existe.");
        }

        var category = new Category { Id = Guid.NewGuid(), Name = request.Name.Trim() };
        await repository.AddAsync(category, cancellationToken);
        return Result<Guid>.Success(category.Id);
    }
}
