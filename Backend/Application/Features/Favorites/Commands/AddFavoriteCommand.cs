using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Common;
using Backend.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Backend.Application.Features.Favorites.Commands;

/// <summary>
/// Comando para agregar un medio a favoritos.
/// </summary>
public record AddFavoriteCommand(string UserId, Guid MediaId) : IRequest<Result>;

/// <summary>
/// Valida el comando de agregar favorito.
/// </summary>
public class AddFavoriteCommandValidator : AbstractValidator<AddFavoriteCommand>
{
    public AddFavoriteCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.MediaId).NotEmpty();
    }
}

/// <summary>
/// Maneja la lógica para agregar un medio a favoritos.
/// </summary>
public class AddFavoriteCommandHandler(IFavoriteRepository favoriteRepository, IMediaRepository mediaRepository, IValidator<AddFavoriteCommand> validator) : IRequestHandler<AddFavoriteCommand, Result>
{
    /// <summary>
    /// Ejecuta la validación y registra el favorito si corresponde.
    /// </summary>
    public async Task<Result> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Result.Failure(validation.Errors.First().ErrorMessage);
        }

        var media = await mediaRepository.GetByIdAsync(request.MediaId, cancellationToken);
        if (media is null)
        {
            return Result.Failure("Archivo multimedia no encontrado.");
        }

        var existing = await favoriteRepository.GetByUserIdAndMediaIdAsync(request.UserId, request.MediaId, cancellationToken);
        if (existing is not null)
        {
            return Result.Success();
        }

        await favoriteRepository.AddAsync(new Favorite
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            MediaItemId = request.MediaId
        }, cancellationToken);

        return Result.Success();
    }
}
