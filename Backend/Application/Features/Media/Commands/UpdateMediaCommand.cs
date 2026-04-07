using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Common;
using Backend.Domain.Enums;
using FluentValidation;
using MediatR;

namespace Backend.Application.Features.Media.Commands;

/// <summary>
/// Comando para actualizar un medio existente.
/// </summary>
public record UpdateMediaCommand(Guid Id, string Title, string Url, string? Description, MediaType Type, Guid CategoryId) : IRequest<Result>;

/// <summary>
/// Valida el comando de actualización de medios.
/// </summary>
public class UpdateMediaCommandValidator : AbstractValidator<UpdateMediaCommand>
{
    public UpdateMediaCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Url).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}

/// <summary>
/// Maneja la lógica de actualización de medios.
/// </summary>
public class UpdateMediaCommandHandler(IMediaRepository mediaRepository, ICategoryRepository categoryRepository, IValidator<UpdateMediaCommand> validator) : IRequestHandler<UpdateMediaCommand, Result>
{
    /// <summary>
    /// Valida la solicitud y actualiza el medio.
    /// </summary>
    public async Task<Result> Handle(UpdateMediaCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Result.Failure(validation.Errors.First().ErrorMessage);
        }

        var media = await mediaRepository.GetByIdAsync(request.Id, cancellationToken);
        if (media is null)
        {
            return Result.Failure("Archivo multimedia no encontrado.");
        }

        var category = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
        {
            return Result.Failure("Categoría inválida.");
        }

        media.Title = request.Title.Trim();
        media.Url = request.Url.Trim();
        media.Description = request.Description?.Trim() ?? string.Empty;
        media.Type = request.Type;
        media.CategoryId = request.CategoryId;

        await mediaRepository.UpdateAsync(media, cancellationToken);
        return Result.Success();
    }
}
