using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Common;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
using FluentValidation;
using MediatR;

namespace Backend.Application.Features.Media.Commands;

/// <summary>
/// Comando para crear un nuevo medio.
/// </summary>
public record CreateMediaCommand(string Title, string Url, string? Description, MediaType Type, Guid CategoryId) : IRequest<Result<Guid>>;

/// <summary>
/// Valida el comando de creación de medios.
/// </summary>
public class CreateMediaCommandValidator : AbstractValidator<CreateMediaCommand>
{
    public CreateMediaCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Url).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}

/// <summary>
/// Maneja la lógica de creación de medios.
/// </summary>
public class CreateMediaCommandHandler(IMediaRepository mediaRepository, ICategoryRepository categoryRepository, IValidator<CreateMediaCommand> validator) : IRequestHandler<CreateMediaCommand, Result<Guid>>
{
    /// <summary>
    /// Valida la solicitud y persiste el medio.
    /// </summary>
    public async Task<Result<Guid>> Handle(CreateMediaCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<Guid>.Failure(validation.Errors.First().ErrorMessage);
        }

        var category = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
        {
            return Result<Guid>.Failure("Categoría inválida.");
        }

        var media = new MediaItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title.Trim(),
            Url = request.Url.Trim(),
            Description = request.Description?.Trim() ?? string.Empty,
            Type = request.Type,
            CategoryId = request.CategoryId
        };

        await mediaRepository.AddAsync(media, cancellationToken);
        return Result<Guid>.Success(media.Id);
    }
}
